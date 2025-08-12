using Microsoft.AspNetCore.Mvc;
using RoadReady.API.Interfaces;
using System.Threading.Tasks;
using System.Linq;

namespace RoadReady.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public AdminController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userRepo.GetAllAsync();
                if (users == null || !users.Any())
                    return NotFound(new { message = "No users found." });

                var result = users.Select(u => new {
                    userId = u.UserId,
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    email = u.Email,
                    phoneNumber = u.PhoneNumber,
                    status = string.IsNullOrEmpty(u.Status) ? "active" : u.Status
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpPut("users/{userId}/status")]
        public async Task<IActionResult> UpdateStatus(int userId, [FromBody] StatusUpdateDto dto)
        {
            try
            {
                var user = await _userRepo.GetByIdAsync(userId);
                if (user == null) return NotFound(new { message = "User not found." });

                user.Status = dto.Status;
                var success = await _userRepo.SaveChangesAsync();

                if (!success)
                    return StatusCode(500, new { message = "Failed to update status." });

                return Ok(new { message = "User status updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
    }

    public class StatusUpdateDto
    {
        public string Status { get; set; } = "active";
    }
}
