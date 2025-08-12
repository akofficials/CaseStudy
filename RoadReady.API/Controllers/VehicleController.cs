using Microsoft.AspNetCore.Mvc;
using RoadReady.API.DTO;
using RoadReady.API.Interfaces;

namespace RoadReady.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _service;

        public VehicleController(IVehicleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vehicles = await _service.GetAllVehiclesAsync();
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var vehicle = await _service.GetVehicleByIdAsync(id);
            if (vehicle == null) return NotFound();
            return Ok(vehicle);
        }

        [HttpPost]
        public async Task<IActionResult> Add(VehicleDto dto)
        {
            await _service.AddVehicleAsync(dto);
            return Ok(new { message = "Vehicle added successfully" });
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string model, [FromQuery] string location, [FromQuery] DateTime pickup, [FromQuery] DateTime dropoff)
        {
            var vehicles = await _service.SearchAvailableVehiclesAsync(model, location, pickup, dropoff);
            return Ok(vehicles);
        }
    }
}
