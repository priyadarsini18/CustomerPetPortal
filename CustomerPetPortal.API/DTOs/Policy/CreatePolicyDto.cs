namespace CustomerPetPortal.API.DTOs.Policy
{
    public class CreatePolicyDto
    {
        public int PetId { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public decimal Premium { get; set; }

        public string Status { get; set; }
            = string.Empty;
    }
}