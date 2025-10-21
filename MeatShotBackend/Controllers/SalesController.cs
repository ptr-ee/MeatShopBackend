using MeatShotBackend.DTOs;
using MeatShotBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeatShotBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _sales;
        public SalesController(ISalesService sales) { _sales = sales; }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSaleDto dto)
        {
            var sale = await _sales.CreateSaleAsync(dto);
            return Ok(sale);
        }


        [HttpGet("summary")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Summary(int shopId, DateTime? date)
        {
            var res = await _sales.GetSalesSummaryAsync(shopId, date);
            return Ok(res);
        }
    }
}
