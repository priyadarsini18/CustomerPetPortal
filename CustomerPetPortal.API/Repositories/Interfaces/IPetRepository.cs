using CustomerPetPortal.Models;

namespace CustomerPetPortal.API.Repositories.Interfaces
{
    public interface IPetRepository
    {
        Task<Pet> AddPetAsync(Pet pet);

        Task<List<Pet>> GetPetsByCustomerIdAsync(int customerId);

        Task<Pet?> GetPetByIdAsync(int petId);

        Task UpdatePetAsync(Pet pet);
        Task<bool> ActivePetExistsAsync(
    int customerId,
    string petName);
        Task<bool> DeletePetAsync(int petId);
    }
}
