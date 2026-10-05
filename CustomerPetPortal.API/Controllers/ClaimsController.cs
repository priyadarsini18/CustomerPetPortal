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
                    .GetClaimsByCustomerIdAsync(customerId);

            return Ok(claims);
        }
    }
}