using MeatShotBackend.Data;
using MeatShotBackend.DTOs;
using MeatShotBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MeatShotBackend.Services
{
    public interface IMeatService
    {
        Task<List<Meat>> GetAllAsync();
        Task<Meat> CreateAsync(MeatCreateDto dto);
    }


    public class MeatService : IMeatService
    {
        private readonly AppDbContext _db;
        public MeatService(AppDbContext db) { _db = db; }
        public async Task<List<Meat>> GetAllAsync() => await _db.Meats.ToListAsync();
        public async Task<Meat> CreateAsync(MeatCreateDto dto)
        {
            var meat = new Meat { Name = dto.Name, ImageUrl = dto.ImageUrl, Unit = dto.Unit ?? "kg" };
            _db.Meats.Add(meat);
            await _db.SaveChangesAsync();
            return meat;
        }
    }
}
