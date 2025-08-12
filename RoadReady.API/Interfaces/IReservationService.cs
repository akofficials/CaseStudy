using RoadReady.API.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoadReady.API.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDto>> GetUserReservationsAsync(int userId);
        Task CreateReservationAsync(ReservationDto dto); // For full ReservationDto
        Task AddReservationAsync(int userId, CreateReservationDto dto); // For minimal DTO input
        Task<bool> CancelReservationAsync(int userId, int reservationId);
    }
}
