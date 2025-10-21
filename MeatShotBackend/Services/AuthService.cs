using MeatShotBackend.Data;
using MeatShotBackend.Services;
using MeatShotBackend.DTOs;
using MeatShotBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace MeatShotBackend.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _config;


        public AuthService(AppDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }


        public async Task<LoginResponse?> AuthenticateAsync(LoginRequest request)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null) return null;

            // Use BCrypt to verify hashed password
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash)) return null;

            var token = GenerateJwt(user);
            return new LoginResponse(token, user.Role, user.ShopId, user.FullName);
        }


        public async Task<User> CreateUserAsync(string username, string password, string fullName, string role, int? shopId)
        {
            var user = new User
            {
                Username = username,
                PasswordHash = HashPassword(password),
                FullName = fullName,
                Role = role,
                ShopId = shopId
            };
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }


        private string GenerateJwt(User user)
        {
            var jwt = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };


            if (user.ShopId.HasValue)
                claims.Add(new Claim("shopId", user.ShopId.Value.ToString()));


            var token = new JwtSecurityToken(
            issuer: jwt["Issuer"],
            audience: jwt["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(jwt["ExpiresInMinutes"])),
            signingCredentials: creds
            );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}