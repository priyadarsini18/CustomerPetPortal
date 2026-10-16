using CustomerPetPortal.API.DTOs.Claim;
using CustomerPetPortal.API.Repositories.Interfaces;
using CustomerPetPortal.API.Services.Interfaces;
using CustomerPetPortal.Models;
using CustomerPetPortal.API.Repositories.Interfaces;

namespace CustomerPetPortal.API.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IClaimRepository _repository;
        private readonly IPolicyRepository _policyRepository;
        public ClaimService(
    IClaimRepository repository,
    IPolicyRepository policyRepository)
{
    _repository = repository;
    _policyRepository = policyRepository;
}

        public async Task<List<ClaimDto>>
            GetClaimsByCustomerIdAsync(
                int customerId)
        {
            var claims =
                await _repository
                    .GetClaimsByCustomerIdAsync(
                        customerId);

            return claims.Select(c => new ClaimDto
            {
                ClaimId = c.ClaimId,
                ClaimNumber = c.ClaimNumber,
                CustomerId = c.CustomerId,
                PetId = c.PetId,
PetName = c.Pet?.PetName ?? string.Empty,
                TreatmentDate =
    DateOnly.FromDateTime(
        c.TreatmentDate),
                ClaimedAmount = c.ClaimedAmount,
                ApprovedAmount = c.ApprovedAmount,
                Status = c.Status
            }).ToList();
        }

        public async Task<ClaimDto>
            AddClaimAsync(
                int customerId,
                CreateClaimDto dto)
        {
            var policies =
    await _policyRepository
        .GetPoliciesByCustomerIdAsync(
            customerId);

var activePolicy =
    policies.FirstOrDefault(p =>
        p.PetId == dto.PetId &&
        p.Status == "Active");

if (activePolicy == null)
{
    throw new InvalidOperationException(
        "Cannot create claim. No active policy exists for this pet.");
}
            var claimCount =
                (await _repository
                    .GetClaimsByCustomerIdAsync(
                        customerId))
                    .Count;

            var claim =
                new CustomerClaim
                {
                    ClaimNumber =
                        (claimCount + 1)
                        .ToString(),

                    CustomerId =
                        customerId,

                    PetId =
                        dto.PetId,

                    TreatmentDate =
                        dto.TreatmentDate,

                    ClaimedAmount =
                        dto.ClaimedAmount,

                    ApprovedAmount =
                        dto.ApprovedAmount,

                    Status =
                        dto.Status
                };

            await _repository
                .AddClaimAsync(claim);

            return new ClaimDto
            {
                ClaimId =
                    claim.ClaimId,

                ClaimNumber =
                    claim.ClaimNumber,

                CustomerId =
                    claim.CustomerId,

                PetId =
                    claim.PetId,
                    PetName =
    claim.Pet?.PetName ?? string.Empty,

                TreatmentDate =
    DateOnly.FromDateTime(
        claim.TreatmentDate),

                ClaimedAmount =
                    claim.ClaimedAmount,

                ApprovedAmount =
                    claim.ApprovedAmount,

                Status =
                    claim.Status
            };
        }
    }
}