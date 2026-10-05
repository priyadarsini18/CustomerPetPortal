using CustomerPetPortal.Models;

namespace CustomerPetPortal.API.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        Task<Address> AddAddressAsync(
            Address address);
    }
}