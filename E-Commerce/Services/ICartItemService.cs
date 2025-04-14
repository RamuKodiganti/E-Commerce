using e_comm.Models;
using e_comm.Models.Orders;

namespace e_comm.Services
{
    public interface ICartItemService
    {
        Task AddCartItemAsync(CartItem cartItem);
        Task<List<CartItem>> GetCartItemsByCartIdAsync(int cartId);
        Task<CartItem> GetCartItemByIdAsync(int cartItemId);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task DeleteCartItemAsync(int cartItemId);

        Task<Order> CheckOutCartAsync(int cartId);
        void UpdateCartItemStatus(int cartItemId, CartItemStatus newStatus);
    }
}
