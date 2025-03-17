namespace Ecommerce.Exceptions
{
    public class OrderItemNotFoundException : Exception
    {
        public OrderItemNotFoundException() { }
        public OrderItemNotFoundException(string msg) : base(msg) { }
    }
}
