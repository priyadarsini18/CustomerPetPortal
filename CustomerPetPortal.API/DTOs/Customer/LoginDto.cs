using System.ComponentModel.DataAnnotations;

namespace CustomerPetPortal.API.DTOs.Customer
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
            = string.Empty;

        [Required]
        public string Password { get; set; }
            = string.Empty;
    }
}