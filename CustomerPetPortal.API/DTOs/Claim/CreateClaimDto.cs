namespace CustomerPetPortal.API.DTOs.Claim
{
    public class CreateClaimDto
    {
        public int CustomerId { get; set; }

        public int PetId { get; set; }

        public DateTime TreatmentDate { get; set; }

        public decimal ClaimedAmount { get; set; }

        public decimal ApprovedAmount { get; set; }

        public string Status { get; set; }
            = string.Empty;
    }
}