namespace CustomerPetPortal.Models
{
    public class CustomerPolicy
    {
        public int PolicyId { get; set; }

        public string PolicyNumber { get; set; } = string.Empty;

        public int CustomerId { get; set; }

        public int PetId { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public decimal Premium { get; set; }

        public string Status { get; set; } = string.Empty;

        // Navigation Properties
        public Customer? Customer { get; set; }

        public Pet? Pet { get; set; }
    }
}