using MeatShotBackend.DTOs;
using MeatShotBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeatShotBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class MeatsController : ControllerBase
    {
        private readonly IMeatService _meatService;
        public MeatsController(IMeatService meatService) { _meatService = meatService; }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll() => Ok(await _meatService.GetAllAsync());


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MeatCreateDto dto)
        {
            var meat = await _meatService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = meat.Id }, meat);
        }
    }
}
