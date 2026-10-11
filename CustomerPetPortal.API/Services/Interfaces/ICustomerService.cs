using CustomerPetPortal.API.DTOs.Customer;

namespace CustomerPetPortal.API.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDto> CreateCustomerAsync(
            CreateCustomerDto dto);

        Task<List<CustomerDto>> GetAllCustomersAsync();

        Task<CustomerDto?> GetCustomerByIdAsync(
            int id);

        Task<CustomerDto?> UpdateCustomerAsync(
            int id,
            UpdateCustomerDto dto);

        Task<bool> DeleteCustomerAsync(
            int id);

        Task<CustomerDto?> LoginAsync(
            string email);
    }
}