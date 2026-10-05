using CustomerPetPortal.API.Data;
using CustomerPetPortal.API.Repositories.Interfaces;
using CustomerPetPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerPetPortal.API.Repositories
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly AppDbContext _context;

        public ClaimRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerClaim>>
            GetClaimsByCustomerIdAsync(int customerId)
        {
            return await _context.CustomerClaims
                .Where(c => c.CustomerId == customerId)
                .ToListAsync();
        }
    }
}