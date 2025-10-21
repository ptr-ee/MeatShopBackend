using MeatShotBackend.Data;
using MeatShotBackend.DTOs;
using MeatShotBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MeatShotBackend.Services
{
    public interface IInventoryService
    {
        Task<List<ShopMeat>> GetByShopAsync(int shopId);
        Task<ShopMeat> AssignAsync(int shopId, AssignShopMeatDto dto);
        Task UpdateStockAndPriceAsync(int shopId, int meatId, decimal stockQty, decimal pricePerKg);
    }


    public class InventoryService : IInventoryService
    {
        private readonly AppDbContext _db;
        public InventoryService(AppDbContext db) { _db = db; }
        public async Task<List<ShopMeat>> GetByShopAsync(int shopId) => await _db.ShopMeats.Include(sm => sm.Meat).Where(sm => sm.ShopId == shopId).ToListAsync();


        public async Task<ShopMeat> AssignAsync(int shopId, AssignShopMeatDto dto)
        {
            var existing = await _db.ShopMeats.FirstOrDefaultAsync(sm => sm.ShopId == shopId && sm.MeatId == dto.MeatId);
            if (existing != null)
            {
                existing.StockQty += dto.StockQty;
                existing.PricePerKg = dto.PricePerKg;
                existing.LastUpdated = DateTime.UtcNow;
                await _db.SaveChangesAsync();
                return existing;
            }


            var shopMeat = new ShopMeat
            {
                ShopId = shopId,
                MeatId = dto.MeatId,
                StockQty = dto.StockQty,
                PricePerKg = dto.PricePerKg,
                LastUpdated = DateTime.UtcNow
            };
            _db.ShopMeats.Add(shopMeat);
            await _db.SaveChangesAsync();
            return shopMeat;
        }


        public async Task UpdateStockAndPriceAsync(int shopId, int meatId, decimal stockQty, decimal pricePerKg)
        {
            var existing = await _db.ShopMeats.FirstOrDefaultAsync(sm => sm.ShopId == shopId && sm.MeatId == meatId);
            if (existing == null) throw new Exception("ShopMeat not found");
            existing.StockQty = stockQty;
            existing.PricePerKg = pricePerKg;
            existing.LastUpdated = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }
    }
}
