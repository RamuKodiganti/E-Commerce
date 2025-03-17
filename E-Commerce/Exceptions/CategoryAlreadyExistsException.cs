namespace E_comm.Exceptions
{
    public class CategoryAlreadyExistsException : ApplicationException
    {

        public CategoryAlreadyExistsException() { }
        public CategoryAlreadyExistsException(string msg) : base(msg) { }

    }
}
