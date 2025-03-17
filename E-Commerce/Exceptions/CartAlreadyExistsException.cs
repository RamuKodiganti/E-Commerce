//using Ecommerce.Models;
namespace Ecommerce.Exceptions
{
    public class CartAlreadyExistsException : ApplicationException
    {
        public CartAlreadyExistsException() { }
        public CartAlreadyExistsException(string msg) : base(msg) { }
    }
}
