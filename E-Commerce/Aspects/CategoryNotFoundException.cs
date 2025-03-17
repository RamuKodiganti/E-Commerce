namespace E_comm.Aspects
{
    public class CategoryNotFoundException : ApplicationException
    {
        public CategoryNotFoundException() { }
        public CategoryNotFoundException(string msg) : base(msg) { }
    }
}
