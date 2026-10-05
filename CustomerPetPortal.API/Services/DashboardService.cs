using CustomerPetPortal.API.Data;
using CustomerPetPortal.API.DTOs.Dashboard;
using CustomerPetPortal.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomerPetPortal.API.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;

        public DashboardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardDto> GetDashboardAsync(
            int customerId)
        {
            var petsCount = await _context.Pets
                .CountAsync(p =>
                    p.CustomerId == customerId &&
                    p.IsActive);

            var activePolicies = await _context.CustomerPolicies
                .CountAsync(p =>
                    p.CustomerId == customerId &&
                    p.Status == "Active");

            var openClaims = await _context.CustomerClaims
                .CountAsync(c =>
                    c.CustomerId == customerId &&
                    c.Status == "Open");

            var policiesExpiringSoon =
                await _context.CustomerPolicies
                    .CountAsync(p =>
                        p.CustomerId == customerId &&
                        p.ExpiryDate <= DateTime.Today.AddDays(30));

            // LINQ Join Demonstration
            var customerPets =
                await (
                    from customer in _context.Customers
                    join pet in _context.Pets
                    on customer.CustomerId equals pet.CustomerId
                    where customer.CustomerId == customerId
                    select new
                    {
                        customer.FirstName,
                        customer.LastName,
                        pet.PetName
                    }
                ).ToListAsync();

            // LINQ GroupJoin Demonstration
            var customerPolicySummary =
                await _context.Customers
                    .Where(c => c.CustomerId == customerId)
                    .GroupJoin(
                        _context.CustomerPolicies,
                        customer => customer.CustomerId,
                        policy => policy.CustomerId,
                        (customer, policies) => new
                        {
                            CustomerName =
                                customer.FirstName,

                            PolicyCount =
                                policies.Count()
                        })
                    .ToListAsync();

            return new DashboardDto
            {
                PetsCount = petsCount,
                ActivePolicies = activePolicies,
                OpenClaims = openClaims,
                PoliciesExpiringSoon = policiesExpiringSoon
            };
        }
    }
}