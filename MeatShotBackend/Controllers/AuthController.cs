using MeatShotBackend.Services;
using MeatShotBackend.DTOs;
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
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var res = await _auth.AuthenticateAsync(req);
            if (res == null) return Unauthorized();
            return Ok(res);
        }


        // For initial setup only - admin should manage users later in UI
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginRequest req)
        {
            var user = await _auth.CreateUserAsync(req.Username, req.Password, req.Username, "Admin", null);
            return Ok(new { user.Id, user.Username });
        }
    }
}
