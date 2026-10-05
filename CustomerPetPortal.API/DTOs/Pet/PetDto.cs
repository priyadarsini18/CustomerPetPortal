namespace CustomerPetPortal.API.DTOs.Pet
{
    public class PetDto
    {
        public int PetId { get; set; }
        public int CustomerId { get; set; }

        public string PetName { get; set; } = string.Empty;

        public string Species { get; set; } = string.Empty;

        public string Breed { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public int Age { get; set; }

        public bool IsActive { get; set; }
    }
}