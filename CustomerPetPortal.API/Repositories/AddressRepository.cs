using CustomerPetPortal.API.Data;
using CustomerPetPortal.API.Repositories.Interfaces;
using CustomerPetPortal.Models;

namespace CustomerPetPortal.API.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AppDbContext _context;

        public AddressRepository(
            AppDbContext context)
        {
            _context = context;
        }

        public async Task<Address> AddAddressAsync(
            Address address)
        {
            _context.Addresses.Add(address);

            await _context.SaveChangesAsync();

            return address;
        }
    }
}