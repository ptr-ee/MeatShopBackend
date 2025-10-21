using MeatShotBackend.DTOs;
using MeatShotBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeatShotBackend.Controllers
{
    [ApiController]
    [Route("api/shops/{shopId}/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventory;
        public InventoryController(IInventoryService inventory) { _inventory = inventory; }


        [HttpGet]
        public async Task<IActionResult> Get(int shopId) => Ok(await _inventory.GetByShopAsync(shopId));


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Assign(int shopId, [FromBody] AssignShopMeatDto dto)
        {
            var result = await _inventory.AssignAsync(shopId, dto);
            return Ok(result);
        }


        [HttpPut("{meatId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int shopId, int meatId, [FromBody] AssignShopMeatDto dto)
        {
            await _inventory.UpdateStockAndPriceAsync(shopId, meatId, dto.StockQty, dto.PricePerKg);
            return NoContent();
        }
    }
}
