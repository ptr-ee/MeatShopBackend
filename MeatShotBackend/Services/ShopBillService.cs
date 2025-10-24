using AutoMapper;
using MeatShotBackend.Data;
using MeatShotBackend.DTOs;
using MeatShotBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MeatShotBackend.Services
{
    public interface IShopBillService
    {
        Task<IEnumerable<ShopBillDto>> GetBillsByShop(int shopId);
        Task AddBill(ShopBillCreateDto dto);
        Task MarkAsPaid(int billId);
    }
    public class ShopBillService : IShopBillService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ShopBillService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShopBillDto>> GetBillsByShop(int shopId)
        {
            var bills = await _context.ShopBills.Where(b => b.ShopId == shopId).ToListAsync();
            return _mapper.Map<IEnumerable<ShopBillDto>>(bills);
        }

        public async Task AddBill(ShopBillCreateDto dto)
        {
            var bill = _mapper.Map<ShopBill>(dto);
            _context.ShopBills.Add(bill);
            await _context.SaveChangesAsync();
        }

        public async Task MarkAsPaid(int billId)
        {
            var bill = await _context.ShopBills.FindAsync(billId);
            if (bill != null)
            {
                bill.IsPaid = true;
                bill.PaidAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
    }

}
