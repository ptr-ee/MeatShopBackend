namespace MeatShotBackend.Models
{
    public class Meat
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? ImageUrl { get; set; }
        public string Unit { get; set; } = "kg";
        public bool IsActive { get; set; } = true;
        public ICollection<ShopMeat> ShopMeats { get; set; } = new List<ShopMeat>();
    }
}
