namespace MeatShotBackend.DTOs
{
    public record SaleItemDto(int MeatId, decimal QuantityKg);
    public record CreateSaleDto(int ShopId, int CashierId, List<SaleItemDto> Items);
}
