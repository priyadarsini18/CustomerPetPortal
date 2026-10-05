
using System.ComponentModel.DataAnnotations;

namespace CustomerPetPortal.API.DTOs.Pet
{
    public class CreatePetDto
    {
        [Required]
        public string PetName { get; set; } = string.Empty;

        [Required]
        public string Species { get; set; } = string.Empty;

        [Required]
        public string Breed { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}