namespace MeatShotBackend.Models
{
    public class ShopMeat
    {
        public int Id { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; } = default!;


        public int MeatId { get; set; }
        public Meat Meat { get; set; } = default!;


        public decimal StockQty { get; set; } // in kg
        public decimal PricePerKg { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
