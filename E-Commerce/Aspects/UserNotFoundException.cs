namespace UserWebAPI.Exceptions
{
    public class UserNotFoundException : ApplicationException
    {
        public UserNotFoundException() { }
        public UserNotFoundException(string msg) : base(msg) { }
    }
}
