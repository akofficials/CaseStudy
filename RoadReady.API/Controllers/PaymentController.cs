using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadReady.API.DTO;
using RoadReady.API.Interfaces;
using System.Security.Claims;

namespace RoadReady.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _service;

        public PaymentController(IPaymentService service)
        {
            _service = service;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        [HttpGet]
        public async Task<IActionResult> GetMyPayments()
        {
            var userId = GetUserId();
            var payments = await _service.GetUserPaymentsAsync(userId);
            return Ok(payments);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePaymentDto dto)
        {
            var userId = GetUserId();
            await _service.AddPaymentAsync(userId, dto);
            return Ok(new { message = "Payment recorded successfully." });
        }
    }
}
