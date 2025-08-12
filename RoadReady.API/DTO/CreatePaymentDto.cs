using System.ComponentModel.DataAnnotations;

namespace RoadReady.API.DTO
{
    public class CreatePaymentDto
    {
        [Required]
        public int ReservationId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string PaymentMethod { get; set; } // e.g., Credit Card, PayPal
    }
}
