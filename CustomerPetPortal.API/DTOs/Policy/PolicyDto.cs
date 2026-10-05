
namespace CustomerPetPortal.API.DTOs.Policy
{
    public class PolicyDto
    {
        public int PolicyId { get; set; }

        public string PolicyNumber { get; set; }
            = string.Empty;

        public int CustomerId { get; set; }

        public int PetId { get; set; }

        public decimal Premium { get; set; }

        public string Status { get; set; }
            = string.Empty;

        public DateTime EffectiveDate { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}