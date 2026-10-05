using CustomerPetPortal.API.DTOs.Policy;
using CustomerPetPortal.API.Repositories.Interfaces;
using CustomerPetPortal.API.Services.Interfaces;

namespace CustomerPetPortal.API.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IPolicyRepository _repository;

        public PolicyService(IPolicyRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<PolicyDto>>
            GetPoliciesByCustomerIdAsync(int customerId)
        {
            var policies =
                await _repository
                    .GetPoliciesByCustomerIdAsync(customerId);

            return policies.Select(p => new PolicyDto
            {
                PolicyId = p.PolicyId,
                PolicyNumber = p.PolicyNumber,
                CustomerId = p.CustomerId,
                PetId = p.PetId,
                Premium = p.Premium,
                Status = p.Status,
                EffectiveDate = p.EffectiveDate,
                ExpiryDate = p.ExpiryDate
            }).ToList();
        }
    }
}
