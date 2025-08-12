using Microsoft.EntityFrameworkCore;
using RoadReady.API.Data;
using RoadReady.API.Models;
using RoadReady.API.Interfaces;

namespace RoadReady.API.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly RoadReadyDbContext _context;

        public PaymentRepository(RoadReadyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(int userId)
        {
            return await _context.Payments
                .Include(p => p.Reservation)
                .Where(p => p.Reservation.UserId == userId)
                .ToListAsync();
        }

        public async Task AddAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
