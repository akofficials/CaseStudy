using System.ComponentModel.DataAnnotations;

namespace RoadReady.API.Models
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        public int Year { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public decimal PricePerDay { get; set; }

        [Required]
        public string Location { get; set; }

        public bool IsAvailable { get; set; } = true;

        public ICollection<Reservation> Reservations { get; set; }
    }
}
