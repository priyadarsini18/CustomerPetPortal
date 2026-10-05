using CustomerPetPortal.API.DTOs.Policy;

namespace CustomerPetPortal.API.Services.Interfaces
{
    public interface IPolicyService
    {
        Task<List<PolicyDto>>
            GetPoliciesByCustomerIdAsync(int customerId);
    }
}