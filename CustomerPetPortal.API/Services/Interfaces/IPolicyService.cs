using CustomerPetPortal.API.DTOs.Policy;

namespace CustomerPetPortal.API.Services.Interfaces
{
    public interface IPolicyService
    {
        Task<List<PolicyDto>>
            GetPoliciesByCustomerIdAsync(int customerId);
            Task<PolicyDto> AddPolicyAsync(
    int customerId,
    CreatePolicyDto dto);
    Task DeactivatePolicyAsync(
    int customerId,
    int policyId);
Task ActivatePolicyAsync(
    int customerId,
    int policyId);
Task DeletePolicyAsync(
    int customerId,
    int policyId);
    }
}