namespace e_comm.Exceptions
{
    public class OrderItemAlreadyExistsException : Exception
    {
        public OrderItemAlreadyExistsException() { }
        public OrderItemAlreadyExistsException(string msg) : base(msg)
        { }
    }
}
