using CustomerPetPortal.API.DTOs.Pet;
using CustomerPetPortal.API.Repositories.Interfaces;
using CustomerPetPortal.API.Services.Interfaces;
using CustomerPetPortal.Models;

namespace CustomerPetPortal.API.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _repository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPolicyRepository _policyRepository;
    
       public PetService(
    IPetRepository repository,
    ICustomerRepository customerRepository,
    IPolicyRepository policyRepository)
{
    _repository = repository;
    _customerRepository =
        customerRepository;

    _policyRepository =
        policyRepository;
}

        public async Task<PetDto> AddPetAsync(
            int customerId,
            CreatePetDto dto)
        {
            if (dto.DateOfBirth > DateTime.Today)
{
throw new ArgumentException(
"Pet date of birth cannot be in the future.");
}
bool customerExists =
    await _customerRepository
        .ActiveCustomerExistsAsync(
            customerId);

if (!customerExists)
{
    throw new InvalidOperationException(
        "Customer does not exist or is inactive.");
}
bool petExists =
    await _repository.ActivePetExistsAsync(
        customerId,
        dto.PetName);

if (petExists)
{
    throw new InvalidOperationException(
        "An active pet with the same name already exists.");
}
            var pet = new Pet
            {
                CustomerId = customerId,
                PetName = dto.PetName,
                Species = dto.Species,
                Breed = dto.Breed,
                DateOfBirth = dto.DateOfBirth,
                IsActive = true
            };

            await _repository.AddPetAsync(pet);

            return new PetDto
            {
                PetId = pet.PetId,
                CustomerId = pet.CustomerId,
                PetName = pet.PetName,
                Species = pet.Species,
                Breed = pet.Breed,
                DateOfBirth =
    DateOnly.FromDateTime(
        pet.DateOfBirth),
                IsActive = pet.IsActive
            };
        }

        public async Task<List<PetDto>>
            GetPetsByCustomerIdAsync(int customerId)
        {
            var pets =
                await _repository.GetPetsByCustomerIdAsync(customerId);

            return pets.Select(p => new PetDto
            {
                PetId = p.PetId,
                CustomerId = p.CustomerId,
                PetName = p.PetName,
                Species = p.Species,
                Breed = p.Breed,
                DateOfBirth =
    DateOnly.FromDateTime(
        p.DateOfBirth),
                IsActive = p.IsActive
            }).ToList();
        }

        public async Task<PetDto?> UpdatePetAsync(
            int petId,
            UpdatePetDto dto)
        {
            if (dto.DateOfBirth > DateTime.Today)
{
    throw new ArgumentException(
        "Pet date of birth cannot be in the future.");
}
            var pet = await _repository.GetPetByIdAsync(petId);

            if (pet == null)
                return null;

            pet.PetName = dto.PetName;
            pet.Species = dto.Species;
            pet.Breed = dto.Breed;
            pet.DateOfBirth = dto.DateOfBirth;

            await _repository.UpdatePetAsync(pet);

            return new PetDto
            {
                PetId = pet.PetId,
                CustomerId = pet.CustomerId,
                PetName = pet.PetName,
                Species = pet.Species,
                Breed = pet.Breed,
                DateOfBirth =
DateOnly.FromDateTime(
pet.DateOfBirth),
                IsActive = pet.IsActive
            };
        }

        public async Task<bool> DeletePetAsync(
    int petId)
{
    bool hasActivePolicy =
        await _policyRepository
            .HasActivePolicyForPetAsync(
                petId);

    if (hasActivePolicy)
    {
        var pet =
            await _repository
                .GetPetByIdAsync(petId);

        if (pet == null)
            return false;

        pet.IsActive = false;

        await _repository
            .UpdatePetAsync(pet);

        return false;
    }

    return await _repository
        .DeletePetAsync(petId);
}
    }
}