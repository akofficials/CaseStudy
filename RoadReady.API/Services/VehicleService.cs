using RoadReady.API.DTO;
using RoadReady.API.Interfaces;
using RoadReady.API.Models;

namespace RoadReady.API.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _repo;

        public VehicleService(IVehicleRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync()
        {
            var vehicles = await _repo.GetAllAsync();
            return vehicles.Select(v => new VehicleDto
            {
                VehicleId = v.VehicleId,
                Make = v.Make,
                Model = v.Model,
                Year = v.Year,
                ImageUrl = v.ImageUrl,
                PricePerDay = v.PricePerDay,
                Location = v.Location,
                IsAvailable = v.IsAvailable
            });
        }

        public async Task<VehicleDto> GetVehicleByIdAsync(int id)
        {
            var v = await _repo.GetByIdAsync(id);
            if (v == null) return null;

            return new VehicleDto
            {
                VehicleId = v.VehicleId,
                Make = v.Make,
                Model = v.Model,
                Year = v.Year,
                ImageUrl = v.ImageUrl,
                PricePerDay = v.PricePerDay,
                Location = v.Location,
                IsAvailable = v.IsAvailable
            };
        }

        public async Task AddVehicleAsync(VehicleDto dto)
        {
            var vehicle = new Vehicle
            {
                Make = dto.Make,
                Model = dto.Model,
                Year = dto.Year,
                ImageUrl = dto.ImageUrl,
                PricePerDay = dto.PricePerDay,
                Location = dto.Location,
                IsAvailable = dto.IsAvailable
            };

            await _repo.AddAsync(vehicle);
            await _repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<VehicleDto>> SearchVehiclesAsync(string location, DateTime pickupDate, DateTime dropOffDate)
        {
            var vehicles = await _repo.SearchAvailableAsync(location, pickupDate, dropOffDate);
            return vehicles.Select(v => new VehicleDto
            {
                VehicleId = v.VehicleId,
                Make = v.Make,
                Model = v.Model,
                Year = v.Year,
                ImageUrl = v.ImageUrl,
                PricePerDay = v.PricePerDay,
                Location = v.Location,
                IsAvailable = v.IsAvailable
            });
        }

        public async Task<List<Vehicle>> SearchAvailableVehiclesAsync(string model, string location, DateTime pickup, DateTime dropoff)
        {
            return await _repo.GetAvailableVehiclesAsync(model, location, pickup, dropoff);
        }
    }
}
