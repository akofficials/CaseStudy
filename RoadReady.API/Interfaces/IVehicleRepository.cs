using RoadReady.API.Models;

namespace RoadReady.API.Interfaces
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Vehicle>> GetAllAsync();
        Task<Vehicle> GetByIdAsync(int id);
        Task AddAsync(Vehicle vehicle);
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<Vehicle>> SearchAvailableAsync(string location, DateTime pickupDate, DateTime dropOffDate);
        Task<List<Vehicle>> GetAvailableVehiclesAsync(string model, string location, DateTime pickup, DateTime dropoff);


    }
}
