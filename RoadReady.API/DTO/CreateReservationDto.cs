using System.ComponentModel.DataAnnotations;

namespace RoadReady.API.DTO
{
    public class CreateReservationDto
    {
        [Required]
        public int VehicleId { get; set; }

        [Required]
        public DateTime PickupDate { get; set; }

        [Required]
        public DateTime DropOffDate { get; set; }
    }
}
