namespace MeatShotBackend.Models
{
    public class SaleItem
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public Sale Sale { get; set; } = default!;
        public int MeatId { get; set; }
        public Meat Meat { get; set; } = default!;
        public decimal QuantityKg { get; set; }
        public decimal PricePerKg { get; set; }
        public decimal Subtotal { get; set; }
    }
}
