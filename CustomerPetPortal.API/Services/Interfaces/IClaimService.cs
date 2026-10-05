using CustomerPetPortal.API.DTOs.Claim;

namespace CustomerPetPortal.API.Services.Interfaces
{
    public interface IClaimService
    {
        Task<List<ClaimDto>>
            GetClaimsByCustomerIdAsync(int customerId);
    }
}