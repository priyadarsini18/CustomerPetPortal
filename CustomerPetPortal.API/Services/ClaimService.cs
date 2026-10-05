using CustomerPetPortal.API.DTOs.Claim;
using CustomerPetPortal.API.Repositories.Interfaces;
using CustomerPetPortal.API.Services.Interfaces;

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
            GetClaimsByCustomerIdAsync(int customerId)
        {
            var claims =
                await _repository
                    .GetClaimsByCustomerIdAsync(customerId);

            return claims.Select(c => new ClaimDto
            {
                ClaimId = c.ClaimId,
                ClaimNumber = c.ClaimNumber,
                CustomerId = c.CustomerId,
                PetId = c.PetId,
                TreatmentDate = c.TreatmentDate,
                ClaimedAmount = c.ClaimedAmount,
                ApprovedAmount = c.ApprovedAmount,
                Status = c.Status
            }).ToList();
        }
    }
}