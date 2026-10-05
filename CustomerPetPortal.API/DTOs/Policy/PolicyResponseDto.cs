namespace CustomerPetPortal.DTOs.Policy
{
    public class PolicyResponseDto
    {
        public string PolicyNumber { get; set; } = string.Empty;

        public DateTime EffectiveDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public decimal Premium { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}