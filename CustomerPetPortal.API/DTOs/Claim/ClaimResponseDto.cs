namespace CustomerPetPortal.DTOs.Claim
{
    public class ClaimResponseDto
    {
        public string ClaimNumber { get; set; } = string.Empty;

        public DateTime TreatmentDate { get; set; }

        public decimal ClaimedAmount { get; set; }

        public decimal ApprovedAmount { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}