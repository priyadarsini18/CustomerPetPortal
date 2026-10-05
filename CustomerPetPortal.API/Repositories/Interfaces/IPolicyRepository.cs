using CustomerPetPortal.Models;

namespace CustomerPetPortal.API.Repositories.Interfaces
{
    public interface IPolicyRepository
    {
        Task<List<CustomerPolicy>>
            GetPoliciesByCustomerIdAsync(int customerId);
        Task<bool> HasActivePolicyForPetAsync(
    int petId);

Task<bool> HasActivePolicyForCustomerAsync(
    int customerId);
    }
}