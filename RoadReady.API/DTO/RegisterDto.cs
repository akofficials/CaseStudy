
using System.ComponentModel.DataAnnotations;

namespace RoadReady.API.DTO
{
    public class RegisterDto
    {
        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [Required, EmailAddress, MaxLength(200)]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        [MaxLength(20)]
        public string PhoneNumber { get; set; }
    }
}

