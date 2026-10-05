
using System.ComponentModel.DataAnnotations;

namespace CustomerPetPortal.API.DTOs.Customer
{
    public class CreateCustomerDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        public string CommunicationPreference { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
        public string AddressLine1 { get; set; } = string.Empty;

public string City { get; set; } = string.Empty;

public string State { get; set; } = string.Empty;

public string ZipCode { get; set; } = string.Empty;

public string ConfirmPassword { get; set; } = string.Empty;
    }
}