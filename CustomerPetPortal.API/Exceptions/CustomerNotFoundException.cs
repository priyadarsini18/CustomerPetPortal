namespace CustomerPetPortal.API.Exceptions
{
    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException()
            : base("Customer not found")
        {
        }
    }
}