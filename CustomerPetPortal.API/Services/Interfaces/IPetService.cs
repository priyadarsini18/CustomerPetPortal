using CustomerPetPortal.API.DTOs.Pet;

namespace CustomerPetPortal.API.Services.Interfaces
{
    public interface IPetService
    {
        Task<PetDto> AddPetAsync(
            int customerId,
            CreatePetDto dto);

        Task<List<PetDto>> GetPetsByCustomerIdAsync(
            int customerId);

        Task<PetDto?> UpdatePetAsync(
            int petId,
            UpdatePetDto dto);

        Task<bool> DeletePetAsync(int petId);
    }
}