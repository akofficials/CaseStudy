using RoadReady.API.DTO;
using RoadReady.API.Interfaces;
using RoadReady.API.Models;

namespace RoadReady.API.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repo;
        private readonly IReservationRepository _reservationRepo;

        public PaymentService(IPaymentRepository repo, IReservationRepository reservationRepo)
        {
            _repo = repo;
            _reservationRepo = reservationRepo;
        }

        public async Task<IEnumerable<PaymentDto>> GetUserPaymentsAsync(int userId)
        {
            var payments = await _repo.GetPaymentsByUserIdAsync(userId);

            return payments.Select(p => new PaymentDto
            {
                PaymentId = p.PaymentId,
                ReservationId = p.ReservationId,
                Amount = p.Amount,
                PaymentMethod = p.PaymentMethod,
                PaymentDate = p.PaymentDate
            }).ToList();
        }

        public async Task AddPaymentAsync(int userId, CreatePaymentDto dto)
        {
            var reservation = await _reservationRepo.GetByIdAsync(dto.ReservationId);
            if (reservation == null || reservation.UserId != userId)
                throw new Exception("Invalid reservation.");

            var payment = new Payment
            {
                ReservationId = dto.ReservationId,
                Amount = dto.Amount,
                PaymentMethod = dto.PaymentMethod,
                PaymentDate = DateTime.UtcNow
            };

            await _repo.AddAsync(payment);
            await _repo.SaveChangesAsync();
        }
    }
}
