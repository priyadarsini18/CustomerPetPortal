namespace CustomerPetPortal.API.Exceptions
{
    public class DuplicateEmailException : Exception
    {
        public DuplicateEmailException()
            : base("Email already exists")
        {
        }
    }
}