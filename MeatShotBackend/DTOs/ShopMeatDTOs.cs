namespace MeatShotBackend.DTOs
{
    public record AssignShopMeatDto(int MeatId, decimal StockQty, decimal PricePerKg);
    public record ShopMeatDto(int Id, int ShopId, int MeatId, decimal StockQty, decimal PricePerKg, DateTime LastUpdated);
}
