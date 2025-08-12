using RoadReady.API.DTO;
using RoadReady.API.Models;

namespace RoadReady.API.Interfaces
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync();
        Task<VehicleDto> GetVehicleByIdAsync(int id);
        Task AddVehicleAsync(VehicleDto dto);
        Task<IEnumerable<VehicleDto>> SearchVehiclesAsync(string location, DateTime pickupDate, DateTime dropOffDate);
        Task<List<Vehicle>> SearchAvailableVehiclesAsync(string model, string location, DateTime pickup, DateTime dropoff);


    }
}
