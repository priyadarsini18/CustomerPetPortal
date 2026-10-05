
namespace CustomerPetPortal.API.DTOs.Pet
{
    public class UpdatePetDto
    {
        public string PetName { get; set; } = string.Empty;

        public string Species { get; set; } = string.Empty;

        public string Breed { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }
    }
}