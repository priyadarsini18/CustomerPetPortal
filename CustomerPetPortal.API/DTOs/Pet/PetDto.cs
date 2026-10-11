namespace CustomerPetPortal.API.DTOs.Pet
{
    public class PetDto
    {
        public int PetId { get; set; }

        public int CustomerId { get; set; }

        public string PetName { get; set; } = string.Empty;

        public string Species { get; set; } = string.Empty;

        public string Breed { get; set; } = string.Empty;

        public DateOnly DateOfBirth { get; set; }

        public bool IsActive { get; set; }

        public int Age
        {
            get
            {
                var today =
                    DateOnly.FromDateTime(
                        DateTime.Today);

                var age =
                    today.Year -
                    DateOfBirth.Year;

                if (today <
                    DateOfBirth.AddYears(age))
                {
                    age--;
                }

                return age;
            }
        }
    }
}
