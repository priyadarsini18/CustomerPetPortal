using CustomerPetPortal.API.DTOs.Customer;
using CustomerPetPortal.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomerPetPortal.API.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(
            ICustomerService customerService)
        {
            _customerService =
                customerService;
        }

        // POST: api/customers
        [HttpPost]
        public async Task<IActionResult> RegisterCustomer(
            [FromBody] CreateCustomerDto dto)
        {
            var customer =
                await _customerService
                    .CreateCustomerAsync(dto);

            return CreatedAtAction(
                nameof(GetCustomerById),
                new
                {
                    id = customer.CustomerId
                },
                customer);
        }

        [HttpPost("login")]
public async Task<IActionResult> Login(
    [FromBody] LoginDto dto)
{
    var customer =
    await _customerService
        .LoginAsync(dto.Email);

    if (customer == null)
    {
        return Unauthorized(new
        {
            Message =
                "Invalid Email or Password"
        });
    }

    return Ok(new
    {
        Message = "Login Successful",
        CustomerId = customer.CustomerId,
        CustomerName =
            $"{customer.FirstName} {customer.LastName}"
    });
}


        // GET: api/customers
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers =
                await _customerService
                    .GetAllCustomersAsync();

            return Ok(customers);
        }

        // GET: api/customers/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(
            int id)
        {
            var customer =
                await _customerService
                    .GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return NotFound(
                    $"Customer with Id {id} not found");
            }

            return Ok(customer);
        }

        // PUT: api/customers/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(
            int id,
            [FromBody] UpdateCustomerDto dto)
        {
            var updatedCustomer =
                await _customerService
                    .UpdateCustomerAsync(
                        id,
                        dto);

            if (updatedCustomer == null)
            {
                return NotFound(
                    $"Customer with Id {id} not found");
            }

            return Ok(updatedCustomer);
        }

        // DELETE: api/customers/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(
            int id)
        {
            var deleted =
                await _customerService
                    .DeleteCustomerAsync(id);

            if (!deleted)
            {
                return NotFound(
                    $"Customer with Id {id} not found");
            }

            return Ok(
                $"Customer with Id {id} deleted successfully");
        }
    }
}