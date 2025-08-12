using System.ComponentModel.DataAnnotations;

namespace RoadReady.API.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [Required, EmailAddress, MaxLength(200)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        public string Status { get; set; } = "active"; // or "inactive"



        public ICollection<Reservation> Reservations { get; set; }
    }
}
