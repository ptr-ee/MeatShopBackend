using MeatShotBackend.DTOs;
using MeatShotBackend.Models;


namespace MeatShotBackend.Services;


public interface IAuthService
{
    Task<LoginResponse?> AuthenticateAsync(LoginRequest request);
    Task<User> CreateUserAsync(string username, string password, string fullName, string role, int? shopId);
}