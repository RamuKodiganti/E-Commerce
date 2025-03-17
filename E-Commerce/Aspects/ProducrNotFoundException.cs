namespace E_comm.Aspects
{
    public class ProductNotFoundException : ApplicationException
    {
        public ProductNotFoundException() { }
        public ProductNotFoundException(string msg) : base(msg) { }
    }
}
