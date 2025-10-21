using MeatShotBackend.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;


namespace MeatShotBackend.Data;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


    public DbSet<Shop> Shops => Set<Shop>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Meat> Meats => Set<Meat>();
    public DbSet<ShopMeat> ShopMeats => Set<ShopMeat>();
    public DbSet<Sale> Sales => Set<Sale>();
    public DbSet<SaleItem> SaleItems => Set<SaleItem>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();


        modelBuilder.Entity<ShopMeat>()
        .HasOne(sm => sm.Shop)
        .WithMany(s => s.ShopMeats)
        .HasForeignKey(sm => sm.ShopId);


        modelBuilder.Entity<ShopMeat>()
        .HasOne(sm => sm.Meat)
        .WithMany(m => m.ShopMeats)
        .HasForeignKey(sm => sm.MeatId);


        modelBuilder.Entity<SaleItem>()
        .HasOne(si => si.Sale)
        .WithMany(s => s.Items)
        .HasForeignKey(si => si.SaleId);


        modelBuilder.Entity<SaleItem>()
        .HasOne(si => si.Meat)
        .WithMany()
        .HasForeignKey(si => si.MeatId);
    }
}