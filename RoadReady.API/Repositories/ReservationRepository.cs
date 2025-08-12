using Microsoft.EntityFrameworkCore;
using RoadReady.API.Data;
using RoadReady.API.Models;
using RoadReady.API.Interfaces;

namespace RoadReady.API.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly RoadReadyDbContext _context;

        public ReservationRepository(RoadReadyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetByUserIdAsync(int userId)
        {
            return await _context.Reservations
                .Include(r => r.Vehicle) // Ensure Vehicle data is loaded
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task<Reservation> GetByIdAsync(int id)
        {
            return await _context.Reservations.FindAsync(id);
        }

        public async Task AddAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        

    }
}
