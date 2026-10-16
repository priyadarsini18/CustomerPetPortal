using CustomerPetPortal.API.Data;
using CustomerPetPortal.API.Repositories.Interfaces;
using CustomerPetPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerPetPortal.API.Repositories
{
    public class PolicyRepository : IPolicyRepository
    {
        private readonly AppDbContext _context;

        public PolicyRepository(
            AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerPolicy>>
    GetPoliciesByCustomerIdAsync(
        int customerId)
{
    return await _context.CustomerPolicies
        .Include(p => p.Pet)
        .Where(p =>
            p.CustomerId == customerId)
        .ToListAsync();
}
        public async Task<bool>
            HasActivePolicyForPetAsync(
                int petId)
        {
            return await _context.CustomerPolicies
    .AnyAsync(p =>
        p.PetId == petId &&
        p.Status.ToLower() == "active");
        }

        public async Task<bool>
            HasActivePolicyForCustomerAsync(
                int customerId)
        {
            return await _context.CustomerPolicies
                .AnyAsync(p =>
                    p.CustomerId == customerId &&
                    p.Status == "Active");
        }

        public async Task<CustomerPolicy>
            AddPolicyAsync(
                CustomerPolicy policy)
        {
            _context.CustomerPolicies
                .Add(policy);

            await _context
                .SaveChangesAsync();

            return policy;
        }
        public async Task<CustomerPolicy?>
GetPolicyByIdAsync(
    int policyId)
{
    return await _context
        .CustomerPolicies
        .FirstOrDefaultAsync(
            p => p.PolicyId == policyId);
}
public async Task UpdatePolicyAsync(
    CustomerPolicy policy)
{
    _context.CustomerPolicies
        .Update(policy);

    await _context
        .SaveChangesAsync();
}
public async Task DeletePolicyAsync(
    CustomerPolicy policy)
{
    _context.CustomerPolicies
        .Remove(policy);

    await _context
        .SaveChangesAsync();
}
}
}