using e_comm.Models;

namespace E_Commerce.Repository
{
    public interface IShoppingCartRepository
    {
        void AddCart(ShoppingCart cart);
        ShoppingCart GetCartByUserId(int userId);
        void UpdateCartStatus(int cartId);
        CartStatus GetCartStatus(int cartId);
    }
}
