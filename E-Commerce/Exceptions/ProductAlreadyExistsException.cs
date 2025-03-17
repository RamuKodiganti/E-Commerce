namespace E_comm.Exceptions
{
    public class ProductAlreadyExistsException : ApplicationException
    {

        public ProductAlreadyExistsException() { }
        public ProductAlreadyExistsException(string msg) : base(msg) { }

    }
}
