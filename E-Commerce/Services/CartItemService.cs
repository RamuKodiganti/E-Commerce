using e_comm.Models;
using e_comm.Models.Orders;
using e_comm.Repository;

namespace e_comm.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _cartItemRepository;

        public CartItemService(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public async Task AddCartItemAsync(CartItem cartItem)
        {
            await _cartItemRepository.AddCartItemAsync(cartItem);
        }

        public async Task<List<CartItem>> GetCartItemsByCartIdAsync(int cartId)
        {
            return await _cartItemRepository.GetCartItemsByCartIdAsync(cartId);
        }

        public async Task<CartItem> GetCartItemByIdAsync(int cartItemId)
        {
            return await _cartItemRepository.GetCartItemByIdAsync(cartItemId);
        }

        public async Task UpdateCartItemAsync(CartItem cartItem)
        {
            await _cartItemRepository.UpdateCartItemAsync(cartItem);
        }

        public async Task DeleteCartItemAsync(int cartItemId)
        {
            await _cartItemRepository.DeleteCartItemAsync(cartItemId);
        }

        public async Task<Order> CheckOutCartAsync(int cartId)
        {
            var order = await _cartItemRepository.CheckOutCartAsync(cartId);
            if (order == null)
            {
                throw new Exception("Cart not found or empty.");
            }
            return order;
        }
        public void UpdateCartItemStatus(int cartItemId, CartItemStatus newStatus)
        {
            var cartItem = _cartItemRepository.GetCartItemByIdAsync(cartItemId).Result;

            if (cartItem == null)
            {
                throw new Exception("Cart Item not found.");
            }

            cartItem.Status = newStatus;
            _cartItemRepository.UpdateCartItemAsync(cartItem).Wait();
        }

    }
}
