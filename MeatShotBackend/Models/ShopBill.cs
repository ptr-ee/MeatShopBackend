namespace MeatShotBackend.Models
{
    public class ShopBill
    {
        public int Id { get; set; }
        public int ShopId { get; set; }
        public DateTime BillingMonth { get; set; } // Example: 2025-01-01
        public string BillType { get; set; } = string.Empty; // Electricity, Water, Rent, etc.
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; } = false;
        public DateTime? PaidAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        public Shop Shop { get; set; }
    }
}
