namespace MeatShotBackend.DTOs
{
    public record LoginRequest(string Username, string Password);
    public record LoginResponse(string Token, string Role, int? ShopId, string FullName);
}
