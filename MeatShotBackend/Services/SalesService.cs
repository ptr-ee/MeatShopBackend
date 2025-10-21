using MeatShotBackend.Data;
using MeatShotBackend.DTOs;
using MeatShotBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MeatShotBackend.Services
{
    public interface ISalesService
    {
        Task<Sale> CreateSaleAsync(CreateSaleDto dto);
        Task<IEnumerable<object>> GetSalesSummaryAsync(int shopId, DateTime? date = null);
    }


    public class SalesService : ISalesService
    {
        private readonly AppDbContext _db;
        public SalesService(AppDbContext db) { _db = db; }


        public async Task<Sale> CreateSaleAsync(CreateSaleDto dto)
        {
            // Basic validation & processing
            var sale = new Sale
            {
                ShopId = dto.ShopId,
                CashierId = dto.CashierId,
                CreatedAt = DateTime.UtcNow
            };


            decimal total = 0m;


            foreach (var item in dto.Items)
            {
                var shopMeat = await _db.ShopMeats.FirstOrDefaultAsync(sm => sm.ShopId == dto.ShopId && sm.MeatId == item.MeatId);
                if (shopMeat == null) throw new Exception($"Meat {item.MeatId} not available in shop {dto.ShopId}");
                if (shopMeat.StockQty < item.QuantityKg) throw new Exception("Insufficient stock");


                var subtotal = item.QuantityKg * shopMeat.PricePerKg;
                total += subtotal;


                var saleItem = new SaleItem
                {
                    MeatId = item.MeatId,
                    QuantityKg = item.QuantityKg,
                    PricePerKg = shopMeat.PricePerKg,
                    Subtotal = subtotal
                };
                sale.Items.Add(saleItem);


                shopMeat.StockQty -= item.QuantityKg;
                shopMeat.LastUpdated = DateTime.UtcNow;
            }


            sale.TotalAmount = total;
            _db.Sales.Add(sale);
            await _db.SaveChangesAsync();


            return sale;
        }


        public async Task<IEnumerable<object>> GetSalesSummaryAsync(int shopId, DateTime? date = null)
        {
            var dt = date ?? DateTime.UtcNow.Date;
            var next = dt.Date.AddDays(1);


            var q = await _db.Sales
            .Where(s => s.ShopId == shopId && s.CreatedAt >= dt && s.CreatedAt < next)
            .SelectMany(s => s.Items)
            .GroupBy(i => i.MeatId)
            .Select(g => new {
                MeatId = g.Key,
                SoldKg = g.Sum(x => x.QuantityKg),
                Sales = g.Sum(x => x.Subtotal)
            }).ToListAsync();


            return q;
        }
    }
}
