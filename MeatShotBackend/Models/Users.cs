namespace MeatShotBackend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string Role { get; set; } = "Cashier"; // Admin or Cashier
        public int? ShopId { get; set; }
        public Shop? Shop { get; set; }
    }
}
