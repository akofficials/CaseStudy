using RoadReady.API.DTO;

namespace RoadReady.API.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDto>> GetUserPaymentsAsync(int userId);
        Task AddPaymentAsync(int userId, CreatePaymentDto dto);
    }
}
