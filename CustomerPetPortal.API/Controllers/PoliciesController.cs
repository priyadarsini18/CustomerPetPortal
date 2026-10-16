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
    try
    {
        var policy =
            await _service
                .AddPolicyAsync(
                    customerId,
                    dto);

        return Ok(policy);
    }
    catch (InvalidOperationException ex)
    {
        return BadRequest(new
        {
            Message = ex.Message
        });
    }
}
[HttpPut("{policyId}/deactivate")]
public async Task<IActionResult>
DeactivatePolicy(
    int customerId,
    int policyId
)
{
    await _service
        .DeactivatePolicyAsync(
            customerId,
            policyId);

    return NoContent();
}
[HttpDelete("{policyId}")]
public async Task<IActionResult>
DeletePolicy(
    int customerId,
    int policyId)
{
    await _service
        .DeletePolicyAsync(
            customerId,
            policyId);

    return NoContent();
}
[HttpPut("{policyId}/activate")]
public async Task<IActionResult> ActivatePolicy(
    int customerId,
    int policyId)
{
    await _service.ActivatePolicyAsync(
        customerId,
        policyId);

    return NoContent();
}

    }
}