using MeatShotBackend.DTOs;
using MeatShotBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace MeatShotBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth) { _auth = auth; }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] DTOs.LoginRequest req)
        {
            var res = await _auth.AuthenticateAsync(req);
            if (res == null) return Unauthorized();
            return Ok(res);
        }


        [HttpPost("register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register([FromBody] DTOs.RegisterRequest req)
        {
            var user = await _auth.CreateUserAsync(req.Username, req.Password, req.FullName, "Cashier", null);
            return Ok(new { user.Id, user.Username });
        }
    }
}
