namespace UserWebAPI.Exceptions
{
    namespace DemoAPI.Exception
    {
        public class UserAlreadyExistsException : ApplicationException
        {
            public UserAlreadyExistsException() { }
            public UserAlreadyExistsException(string msg) : base(msg) { }
        }
    }

}
