namespace CustomerPetPortal.API.Exceptions
{
    public class PetNotFoundException : Exception
    {
        public PetNotFoundException()
            : base("Pet not found")
        {
        }
    }
}