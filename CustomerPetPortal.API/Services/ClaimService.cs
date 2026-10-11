using CustomerPetPortal.API.DTOs.Claim;
using CustomerPetPortal.API.Repositories.Interfaces;
using CustomerPetPortal.API.Services.Interfaces;
using CustomerPetPortal.Models;

namespace CustomerPetPortal.API.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IClaimRepository _repository;

        public ClaimService(
            IClaimRepository repository)
        {
            _repository = repository;
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