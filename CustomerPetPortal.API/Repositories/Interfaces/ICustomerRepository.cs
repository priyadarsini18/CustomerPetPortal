using CustomerPetPortal.Models;

namespace CustomerPetPortal.API.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> AddCustomerAsync(
            Customer customer);

        Task<List<Customer>>
            GetAllCustomersAsync();

        Task<Customer?> GetCustomerByIdAsync(
            int id);

        // LINQ Include Demonstration
        Task<Customer?> GetCustomerWithPetsAsync(
            int customerId);

        // LINQ ThenInclude Demonstration
        Task<Customer?> GetCustomerWithPoliciesAsync(
            int customerId);

        Task<Customer?> UpdateCustomerAsync(
            Customer customer);

        // LINQ Any Demonstration
        Task<bool> ActiveCustomerExistsAsync(
            int customerId);

        Task<bool> DeleteCustomerAsync(
            int id);

        Task<bool> EmailExistsAsync(
            string email);
        Task<Customer?> GetCustomerByEmailAsync(
    string email);
    }
}