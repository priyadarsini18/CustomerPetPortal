namespace CustomerPetPortal.API.DTOs.Customer
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string CommunicationPreference { get; set; } = string.Empty;
    }
}