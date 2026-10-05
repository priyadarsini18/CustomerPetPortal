namespace CustomerPetPortal.API.DTOs.Claim
{
    public class ClaimDto
    {
        public int ClaimId { get; set; }

        public string ClaimNumber { get; set; }
            = string.Empty;

        public int CustomerId { get; set; }

        public int PetId { get; set; }

        public DateTime TreatmentDate { get; set; }

        public decimal ClaimedAmount { get; set; }

        public decimal ApprovedAmount { get; set; }

        public string Status { get; set; }
            = string.Empty;
    }
}