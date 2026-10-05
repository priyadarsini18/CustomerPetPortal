namespace CustomerPetPortal.API.DTOs.Dashboard
{
    public class DashboardDto
    {
        public int PetsCount { get; set; }

        public int ActivePolicies { get; set; }

        public int OpenClaims { get; set; }

        public int PoliciesExpiringSoon { get; set; }
    }
}