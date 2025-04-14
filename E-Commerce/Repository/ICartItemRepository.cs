using e_comm.Models;
using e_comm.Models.Orders;

namespace e_comm.Repository
{
    public interface ICartItemRepository
    {
        Task AddCartItemAsync(CartItem cartItem);
        Task<List<CartItem>> GetCartItemsByCartIdAsync(int cartId);
        Task<CartItem> GetCartItemByIdAsync(int cartItemId);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task DeleteCartItemAsync(int cartItemId);

        Task<Order> CheckOutCartAsync(int cartId);
    }
}
