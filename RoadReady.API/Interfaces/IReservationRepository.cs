using RoadReady.API.Models;

namespace RoadReady.API.Interfaces
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetByUserIdAsync(int userId);
        Task<Reservation> GetByIdAsync(int id);
        Task AddAsync(Reservation reservation);
        Task<bool> SaveChangesAsync();
    }
}
