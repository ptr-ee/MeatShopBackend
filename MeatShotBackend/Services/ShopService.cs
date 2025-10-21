using MeatShotBackend.Data;
using MeatShotBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MeatShotBackend.Services
{
    public interface IShopService
    {
        Task<List<Shop>> GetAllAsync();
        Task<Shop> CreateAsync(Shop shop);
    }


    public class ShopService : IShopService
    {
        private readonly AppDbContext _db;
        public ShopService(AppDbContext db) { _db = db; }
        public async Task<List<Shop>> GetAllAsync() => await _db.Shops.ToListAsync();
        public async Task<Shop> CreateAsync(Shop shop)
        {
            _db.Shops.Add(shop);
            await _db.SaveChangesAsync();
            return shop;
        }
    }
}
