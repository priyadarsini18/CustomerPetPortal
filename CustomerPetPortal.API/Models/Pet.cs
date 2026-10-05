namespace CustomerPetPortal.Models
{
    public class Pet
    {
        public int PetId { get; set; }

        public int CustomerId { get; set; }

        public string PetName { get; set; } = string.Empty;

        public string Species { get; set; } = string.Empty;

        public string Breed { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation Property
        public Customer? Customer { get; set; }

        public ICollection<CustomerPolicy>? Policies { get; set; }

        public ICollection<CustomerClaim>? Claims { get; set; }
    }
}