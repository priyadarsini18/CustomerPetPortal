using CustomerPetPortal.API.Data;
using CustomerPetPortal.Models;
using CustomerPetPortal.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomerPetPortal.API.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);

            await _context.SaveChangesAsync();

            return customer;
        }
public async Task<Customer?> GetCustomerWithPoliciesAsync(
    int customerId)
{
    return await _context.Customers
        .Include(c => c.Pets)
        .ThenInclude(p => p.Policies)
        .FirstOrDefaultAsync(
            c => c.CustomerId == customerId);
}
public async Task<Customer?> GetCustomerByIdAsync(
    int id)
{
    return await _context.Customers
        .FirstOrDefaultAsync(
            c => c.CustomerId == id);
}
public async Task<Customer?> GetCustomerByEmailAsync(
    string email)
{
    return await _context.Customers
        .FirstOrDefaultAsync(
            c => c.Email == email);
}
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetCustomerWithPetsAsync(
    int customerId)
{
    return await _context.Customers
        .Include(c => c.Pets)
        .FirstOrDefaultAsync(
            c => c.CustomerId == customerId);
}

        public async Task<Customer?> UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Update(customer);

            await _context.SaveChangesAsync();

            return customer;
        }
        public async Task<bool> ActiveCustomerExistsAsync(
    int customerId)
{
    return await _context.Customers
        .AnyAsync(c =>
            c.CustomerId == customerId &&
            c.IsActive);
}
        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer == null)
                return false;

            _context.Customers.Remove(customer);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Customers
                .AnyAsync(c => c.Email == email);
        }
        
    }
}