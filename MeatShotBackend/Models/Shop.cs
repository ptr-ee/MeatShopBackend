namespace MeatShotBackend.Models
{
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Location { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<ShopMeat> ShopMeats { get; set; } = new List<ShopMeat>();
        public ICollection<ShopBill> ShopBills { get; set; }
    }
}
