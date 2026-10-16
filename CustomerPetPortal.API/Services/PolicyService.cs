using CustomerPetPortal.API.DTOs.Policy;
using CustomerPetPortal.API.Repositories.Interfaces;
using CustomerPetPortal.API.Services.Interfaces;
using CustomerPetPortal.Models;

namespace CustomerPetPortal.API.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IPolicyRepository _repository;

        public PolicyService(
            IPolicyRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<PolicyDto>>
            GetPoliciesByCustomerIdAsync(
                int customerId)
        {
            var policies =
                await _repository
                    .GetPoliciesByCustomerIdAsync(
                        customerId);

            return policies.Select(p => new PolicyDto
            {
                PolicyId = p.PolicyId,
                PolicyNumber = p.PolicyNumber,
                CustomerId = p.CustomerId,
                PetId = p.PetId,
                PetName = p.Pet?.PetName ?? string.Empty,
                Premium = p.Premium,
                Status = p.Status,
                EffectiveDate = p.EffectiveDate,
                ExpiryDate = p.ExpiryDate
            }).ToList();
        }

        public async Task<PolicyDto>
            AddPolicyAsync(
                int customerId,
                CreatePolicyDto dto)
        {
            if (dto.ExpiryDate <= dto.EffectiveDate)
            {
                throw new ArgumentException(
                    "Expiry Date must be greater than Effective Date.");
            }

            if (dto.Premium <= 0)
            {
                throw new ArgumentException(
                    "Premium must be greater than zero.");
            }

            var hasActivePolicy =
                await _repository
                    .HasActivePolicyForPetAsync(
                        dto.PetId);

            if (hasActivePolicy)
            {
                throw new InvalidOperationException(
                    "This pet already has an active policy."
                );
            }

            var existingPolicies =
                await _repository
                    .GetPoliciesByCustomerIdAsync(
                        customerId);

            var policyNumber =
                (existingPolicies.Count + 1)
                .ToString();

            var policy =
                new CustomerPolicy
                {
                    PolicyNumber = policyNumber,
                    CustomerId = customerId,
                    PetId = dto.PetId,
                    EffectiveDate = dto.EffectiveDate,
                    ExpiryDate = dto.ExpiryDate,
                    Premium = dto.Premium,
                    Status = dto.Status
                };

            await _repository
                .AddPolicyAsync(policy);

            return new PolicyDto
            {
                PolicyId = policy.PolicyId,
                PolicyNumber = policy.PolicyNumber,
                CustomerId = policy.CustomerId,
                PetId = policy.PetId,
                PetName = policy.Pet?.PetName ?? string.Empty,
                EffectiveDate = policy.EffectiveDate,
                ExpiryDate = policy.ExpiryDate,
                Premium = policy.Premium,
                Status = policy.Status
            };
        }

        public async Task
            DeactivatePolicyAsync(
                int customerId,
                int policyId)
        {
            var policy =
                await _repository
                    .GetPolicyByIdAsync(
                        policyId);

            if (
                policy == null ||
                policy.CustomerId != customerId
            )
            {
                throw new Exception(
                    "Policy not found."
                );
            }

            policy.Status = "Inactive";

            await _repository
                .UpdatePolicyAsync(
                    policy);
        }

        public async Task
            DeletePolicyAsync(
                int customerId,
                int policyId)
        {
            var policy =
                await _repository
                    .GetPolicyByIdAsync(
                        policyId);

            if (
                policy == null ||
                policy.CustomerId != customerId
            )
            {
                throw new Exception(
                    "Policy not found."
                );
            }

            if (
                policy.Status == "Active"
            )
            {
                throw new Exception(
                    "Active policies cannot be deleted."
                );
            }

            await _repository
                .DeletePolicyAsync(
                    policy);
        }
       public async Task ActivatePolicyAsync(
    int customerId,
    int policyId)
{
    var policy =
        await _repository
            .GetPolicyByIdAsync(
                policyId);

    if (
        policy == null ||
        policy.CustomerId != customerId
    )
    {
        throw new Exception(
            "Policy not found."
        );
    }

   var policies =
    await _repository
        .GetPoliciesByCustomerIdAsync(
            customerId);

var hasActivePolicy =
    policies.Any(p =>
        p.PetId == policy.PetId &&
        p.PolicyId != policy.PolicyId &&
        p.Status == "Active");

if (hasActivePolicy)
{
    throw new InvalidOperationException(
        "This pet already has an active policy."
    );
}

    policy.Status = "Active";

    await _repository
        .UpdatePolicyAsync(
            policy);
}
    }
}