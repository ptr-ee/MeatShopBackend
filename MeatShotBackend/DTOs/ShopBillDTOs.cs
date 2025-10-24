namespace MeatShotBackend.DTOs
{
    public record ShopBillCreateDto(int ShopId, DateTime BillingMonth, string BillType, decimal Amount);
    public record ShopBillDto(int Id, int ShopId, DateTime BillingMonth, string BillType, decimal Amount, bool IsPaid, DateTime? PaidAt);
    public record ShopBillUpdateStatusDto(bool IsPaid);

}
