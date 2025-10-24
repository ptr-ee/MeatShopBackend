using MeatShotBackend.DTOs;
using MeatShotBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace MeatShotBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShopBillsController : ControllerBase
    {
        private readonly IShopBillService _service;

        public ShopBillsController(IShopBillService service)
        {
            _service = service;
        }

        [HttpGet("{shopId}")]
        public async Task<IActionResult> GetBills(int shopId) =>
            Ok(await _service.GetBillsByShop(shopId));

        [HttpPost]
        public async Task<IActionResult> AddBill(ShopBillCreateDto dto)
        {
            await _service.AddBill(dto);
            return Ok(new { message = "Bill added successfully" });
        }

        [HttpPut("{billId}/pay")]
        public async Task<IActionResult> MarkAsPaid(int billId)
        {
            await _service.MarkAsPaid(billId);
            return Ok(new { message = "Bill marked as paid" });
        }
    }
}
