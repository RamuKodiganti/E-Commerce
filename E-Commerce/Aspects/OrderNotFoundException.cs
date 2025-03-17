namespace Ecommerce.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException() { }
        public OrderNotFoundException(string msg) : base(msg) { }

    }
}
