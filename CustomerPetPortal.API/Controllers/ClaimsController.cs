using CustomerPetPortal.API.DTOs.Claim;
using CustomerPetPortal.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomerPetPortal.API.Controllers
{
    [ApiController]
    [Route("api/customers/{customerId}/claims")]
    public class ClaimsController : ControllerBase
    {
        private readonly IClaimService _service;

        public ClaimsController(
            IClaimService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetClaims(
            int customerId)
        {
            var claims =
                await _service
                    .GetClaimsByCustomerIdAsync(
                        customerId);

            return Ok(claims);
        }

        // ADD THIS METHOD HERE
        [HttpPost]
        public async Task<IActionResult> AddClaim(
            int customerId,
            [FromBody] CreateClaimDto dto)
        {
            var claim =
                await _service
                    .AddClaimAsync(
                        customerId,
                        dto);

            return Ok(claim);
        }
    }
}