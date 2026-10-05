namespace CustomerPetPortal.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string CommunicationPreference { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        // Navigation Properties
        public ICollection<Address>? Addresses { get; set; }

        public ICollection<Pet> Pets { get; set; }
    = new List<Pet>();

        public ICollection<CustomerPolicy>? Policies { get; set; }

        public ICollection<CustomerClaim>? Claims { get; set; }
    }
}