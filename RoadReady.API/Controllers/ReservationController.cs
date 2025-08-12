using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadReady.API.DTO;
using RoadReady.API.Interfaces;
using System.Security.Claims;

namespace RoadReady.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        private int GetUserId()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr))
                throw new UnauthorizedAccessException("User ID not found in token.");

            return int.Parse(userIdStr);
        }


        // GET: api/Reservation
        [HttpGet]
        public async Task<IActionResult> GetMyReservations()
        {
            try
            {
                var userId = GetUserId();
                var reservations = await _reservationService.GetUserReservationsAsync(userId);
                return Ok(reservations);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }



        // POST: api/Reservation
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReservationDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserId();
            await _reservationService.AddReservationAsync(userId, dto);
            return Ok(new { message = "Reservation created successfully." });
        }

        // PUT: api/Reservation/cancel/5
        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            var userId = GetUserId();
            var result = await _reservationService.CancelReservationAsync(userId, id);
            if (!result)
                return BadRequest(new { message = "Unable to cancel reservation." });

            return Ok(new { message = "Reservation cancelled." });
        }
    }
}
