namespace MeatShotBackend.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; } = default!;
        public int CashierId { get; set; }
        public User Cashier { get; set; } = default!;
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();
    }
}
