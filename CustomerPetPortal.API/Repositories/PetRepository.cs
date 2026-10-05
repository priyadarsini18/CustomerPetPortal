using CustomerPetPortal.API.Data;
using CustomerPetPortal.API.Repositories.Interfaces;
using CustomerPetPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerPetPortal.API.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly AppDbContext _context;

        public PetRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Pet> AddPetAsync(Pet pet)
        {
            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();

            return pet;
        }

        public async Task<List<Pet>>
            GetPetsByCustomerIdAsync(int customerId)
        {
            return await _context.Pets
                .Where(x => x.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<Pet?>
            GetPetByIdAsync(int petId)
        {
            return await _context.Pets
                .FirstOrDefaultAsync(x => x.PetId == petId);
        }

        public async Task UpdatePetAsync(Pet pet)
        {
            _context.Pets.Update(pet);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeletePetAsync(int petId)
        {
            var pet =
                await _context.Pets.FindAsync(petId);

            if (pet == null)
                return false;

            _context.Pets.Remove(pet);

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> ActivePetExistsAsync(
    int customerId,
    string petName)
{
    return await _context.Pets
        .AnyAsync(p =>
            p.CustomerId == customerId &&
            p.PetName == petName &&
            p.IsActive);
}
public async Task<bool>
    HasActivePolicyForPetAsync(
    int petId)
{
    return await _context.CustomerPolicies
        .AnyAsync(p =>
            p.PetId == petId &&
            p.Status == "Active");
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
    }
}