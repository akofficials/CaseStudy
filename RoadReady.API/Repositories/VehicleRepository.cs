using Microsoft.EntityFrameworkCore;
using RoadReady.API.Data;
using RoadReady.API.Models;
using RoadReady.API.Interfaces;

namespace RoadReady.API.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly RoadReadyDbContext _context;

        public VehicleRepository(RoadReadyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            return await _context.Vehicles.ToListAsync();
        }

        public async Task<Vehicle> GetByIdAsync(int id)
        {
            return await _context.Vehicles.FindAsync(id);
        }

        public async Task AddAsync(Vehicle vehicle)
        {
            await _context.Vehicles.AddAsync(vehicle);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<Vehicle>> SearchAvailableAsync(string location, DateTime pickupDate, DateTime dropOffDate)
        {
            // Get vehicles that are in the location AND not reserved during the date range
            var reservedVehicleIds = await _context.Reservations
                .Where(r =>
                    r.PickupDate < dropOffDate &&
                    r.DropOffDate > pickupDate &&
                    !r.IsCancelled)
                .Select(r => r.VehicleId)
                .Distinct()
                .ToListAsync();

            return await _context.Vehicles
                .Where(v =>
                    v.Location == location &&
                    !reservedVehicleIds.Contains(v.VehicleId) &&
                    v.IsAvailable)
                .ToListAsync();
        }
        public async Task<List<Vehicle>> GetAvailableVehiclesAsync(string model, string location, DateTime pickup, DateTime dropoff)
        {
            return await _context.Vehicles
                .Where(v =>
                    v.IsAvailable &&
                    v.Location == location &&
                    (string.IsNullOrEmpty(model) || v.Model.Contains(model)) &&
                    !_context.Reservations.Any(r =>
                        r.VehicleId == v.VehicleId &&
                        !(dropoff <= r.PickupDate || pickup >= r.DropOffDate)
                    )
                )
                .ToListAsync();
        }


    }
}
