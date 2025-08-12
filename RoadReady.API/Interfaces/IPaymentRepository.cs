using RoadReady.API.Models;

namespace RoadReady.API.Interfaces
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(int userId);
        Task AddAsync(Payment payment);
        Task<bool> SaveChangesAsync();
    }
}
