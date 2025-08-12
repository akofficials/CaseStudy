using RoadReady.API.DTO;
using RoadReady.API.Interfaces;
using RoadReady.API.Models;

namespace RoadReady.API.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repo;
        private readonly IVehicleRepository _vehicleRepo;

        public ReservationService(IReservationRepository repo, IVehicleRepository vehicleRepo)
        {
            _repo = repo;
            _vehicleRepo = vehicleRepo;
        }

        // Get all reservations for the logged-in user
        public async Task<IEnumerable<ReservationDto>> GetUserReservationsAsync(int userId)
        {
            var reservations = await _repo.GetByUserIdAsync(userId);

            var result = reservations.Select(r => new ReservationDto
            {
                ReservationId = r.ReservationId,
                VehicleId = r.VehicleId,
                VehicleModel = r.Vehicle?.Model ?? "Unknown", // defensive
                PickupDate = r.PickupDate,
                DropOffDate = r.DropOffDate,
                TotalPrice = r.TotalPrice,
                IsCancelled = r.IsCancelled
            });

            return result;
        }


        // Create reservation using full ReservationDto (used by front-end)
        public async Task CreateReservationAsync(ReservationDto dto)
        {
            var vehicle = await _vehicleRepo.GetByIdAsync(dto.VehicleId);
            if (vehicle == null || !vehicle.IsAvailable)
                throw new Exception("Vehicle not available.");

            var overlapping = vehicle.Reservations?.Any(r =>
                !r.IsCancelled &&
                r.PickupDate < dto.DropOffDate &&
                r.DropOffDate > dto.PickupDate) ?? false;

            if (overlapping)
                throw new Exception("Vehicle is already booked for selected dates.");

            var totalDays = (dto.DropOffDate - dto.PickupDate).Days;
            if (totalDays <= 0)
                throw new Exception("Invalid date range.");

            var reservation = new Reservation
            {
                UserId = dto.UserId,
                VehicleId = dto.VehicleId,
                PickupDate = dto.PickupDate,
                DropOffDate = dto.DropOffDate,
                TotalPrice = vehicle.PricePerDay * totalDays,
                IsCancelled = false
            };

            await _repo.AddAsync(reservation);
            await _repo.SaveChangesAsync();
        }

        // Create reservation using CreateReservationDto and userId (used internally)
        public async Task AddReservationAsync(int userId, CreateReservationDto dto)
        {
            var vehicle = await _vehicleRepo.GetByIdAsync(dto.VehicleId);
            if (vehicle == null || !vehicle.IsAvailable)
                throw new Exception("Vehicle not available.");

            var overlapping = vehicle.Reservations?.Any(r =>
                !r.IsCancelled &&
                r.PickupDate < dto.DropOffDate &&
                r.DropOffDate > dto.PickupDate) ?? false;

            if (overlapping)
                throw new Exception("Vehicle is already booked for selected dates.");

            var totalDays = (dto.DropOffDate - dto.PickupDate).Days;
            if (totalDays <= 0)
                throw new Exception("Invalid date range.");

            var reservation = new Reservation
            {
                UserId = userId,
                VehicleId = dto.VehicleId,
                PickupDate = dto.PickupDate,
                DropOffDate = dto.DropOffDate,
                TotalPrice = vehicle.PricePerDay * totalDays,
                IsCancelled = false
            };

            await _repo.AddAsync(reservation);
            await _repo.SaveChangesAsync();
        }

        // Cancel reservation by user
        public async Task<bool> CancelReservationAsync(int userId, int reservationId)
        {
            var reservation = await _repo.GetByIdAsync(reservationId);
            if (reservation == null || reservation.UserId != userId)
                return false;

            reservation.IsCancelled = true;
            return await _repo.SaveChangesAsync();
        }
    }
}
