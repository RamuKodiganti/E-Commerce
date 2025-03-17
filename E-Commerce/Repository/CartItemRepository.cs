using e_comm.Models;
using e_comm.Models.Orders;
using E_comm.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace e_comm.Repository
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly DataContext _context;

        public CartItemRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddCartItemAsync(CartItem cartItem)
        {
            await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CartItem>> GetCartItemsByCartIdAsync(int cartId)
        {
            return await _context.CartItems
                .Include(ci => ci.Product)
                .Where(ci => ci.CartId == cartId)
                .ToListAsync();
        }

        public async Task<CartItem> GetCartItemByIdAsync(int cartItemId)
        {
            return await _context.CartItems
                .Include(ci => ci.Product)
                .FirstOrDefaultAsync(ci => ci.CartItemId == cartItemId);
        }

        public async Task UpdateCartItemAsync(CartItem cartItem)
        {
            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCartItemAsync(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }
        public Order CheckOutCart(int cartId)
        {
            var cart = _context.ShoppingCartTable
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product) // Ensure Product is included
                .FirstOrDefault(c => c.CartId == cartId);

            if (cart == null)
            {
                return null;
            }

            Order newOrder = new Order
            {
                UserId = cart.UserId,
                OrderStatus = OrderStatus.Pending, 
                OrderDate = DateTime.Now,
                TotalBaseAmount = (decimal)cart.CartItems.Sum(item => item.TotalPrice),
                PaymentStatus = PaymentStatus.Pending,
                OrderItems_ = cart.CartItems.Select(ci => new OrderItem
                {
                    ProductId = ci.ProductId,
                    Product = new Product
                    {
                        Price = ci.Product.Price
                    },
                    Quantity = ci.Quantity,
                    TotalPrice = (decimal)ci.Product.Price * ci.Quantity 
                }).ToList()
            };

            _context.Orders_.Add(newOrder);
            _context.SaveChanges();

            return newOrder;
        }
    }

}
