using MeatShotBackend.Models;
using MeatShotBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeatShotBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class ShopsController : ControllerBase
    {
        private readonly IShopService _shopService;
        public ShopsController(IShopService shopService) { _shopService = shopService; }


        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _shopService.GetAllAsync());


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Shop shop) => Ok(await _shopService.CreateAsync(shop));
    }
}
