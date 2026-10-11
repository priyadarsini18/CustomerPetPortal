using CustomerPetPortal.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CustomerPetPortal.API.DTOs.Policy;

namespace CustomerPetPortal.API.Controllers
{
    [ApiController]
    [Route("api/customers/{customerId}/policies")]
    public class PoliciesController : ControllerBase
    {
        private readonly IPolicyService _service;

        public PoliciesController(
            IPolicyService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult>
            GetPolicies(int customerId)
        {
            var policies =
                await _service
                    .GetPoliciesByCustomerIdAsync(customerId);

            return Ok(policies);
        }
        [HttpPost]
public async Task<IActionResult> AddPolicy(
    int customerId,
    [FromBody] CreatePolicyDto dto)
{
    var policy =
        await _service
            .AddPolicyAsync(
                customerId,
                dto);

    return Ok(policy);
}
    }
}