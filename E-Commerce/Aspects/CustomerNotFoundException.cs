namespace UserWebAPI.Exceptions
{
    public class CustomerNotFoundException : ApplicationException
    {
        public CustomerNotFoundException() { }
        public CustomerNotFoundException(string msg) : base(msg) { }
    }
}
