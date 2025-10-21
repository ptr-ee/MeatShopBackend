namespace MeatShotBackend.DTOs
{
    public record MeatCreateDto(string Name, string? ImageUrl, string? Unit);
    public record MeatDto(int Id, string Name, string? ImageUrl, string Unit, bool IsActive);
}
