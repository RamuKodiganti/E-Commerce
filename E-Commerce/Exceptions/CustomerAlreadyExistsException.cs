namespace UserWebAPI.Exceptions
{
    namespace DemoAPI.Exception
    {
        public class CustomerAlreadyExistsException : ApplicationException
        {
            public CustomerAlreadyExistsException() { }
            public CustomerAlreadyExistsException(string msg) : base(msg) { }
        }
    }

}
