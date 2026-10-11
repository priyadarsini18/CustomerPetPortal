using CustomerPetPortal.Models;

namespace CustomerPetPortal.API.Repositories.Interfaces
{
    public interface IClaimRepository
    {
        Task<List<CustomerClaim>>
            GetClaimsByCustomerIdAsync(int customerId);
            Task<CustomerClaim> AddClaimAsync(
    CustomerClaim claim);
    }
}