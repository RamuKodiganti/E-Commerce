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
            return await _context.CartItems.Include(ci => ci.Product).FirstOrDefaultAsync(ci => ci.CartItemId == cartItemId);
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
        public async Task<Order> CheckOutCartAsync(int cartId)

        {

            try

            {

                var cart = await _context.ShoppingCartTable

                    .Include(c => c.CartItems)

                    .ThenInclude(ci => ci.Product)

                    .FirstOrDefaultAsync(c => c.CartId == cartId);

                if (cart == null || !cart.CartItems.Any())

                {

                    return null; // Cart not found or empty

                }

                var newOrder = new Order

                {

                    UserId = cart.UserId,

                    OrderStatus = OrderStatus.Pending,

                    OrderDate = DateTime.Now,

                    TotalBaseAmount = (decimal)cart.CartItems.Sum(item => item.TotalPrice),

                    PaymentStatus = PaymentStatus.Pending,

                    OrderItems_ = cart.CartItems.Select(ci => new OrderItem

                    {

                        ProductId = ci.ProductId,

                        Product = ci.Product,

                        Quantity = ci.Quantity,

                        TotalPrice = (decimal)ci.Product.Price * ci.Quantity

                    }).ToList()

                };

                using var transaction = await _context.Database.BeginTransactionAsync();

                try

                {

                    await _context.Orders_.AddAsync(newOrder);

                    foreach (var orderItem in newOrder.OrderItems_)

                    {

                        var product = await _context.Products.FindAsync(orderItem.ProductId);

                        if (product != null)

                        {

                            if (product.StockQuantity >= orderItem.Quantity)

                            {

                                product.StockQuantity -= orderItem.Quantity;

                                _context.Products.Update(product);

                            }

                            else

                            {

                                throw new InvalidOperationException($"Insufficient stock for product {product.ProductName}.");

                            }

                        }

                    }

                    foreach (var cartItem in cart.CartItems)

                    {

                        cartItem.Status = CartItemStatus.Ordered;

                        _context.CartItems.Update(cartItem);

                    }

                    cart.Status = CartStatus.Completed;

                    _context.ShoppingCartTable.Update(cart);

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                }

                catch

                {

                    await transaction.RollbackAsync();

                    throw;

                }

                return newOrder;

            }

            catch (Exception ex)

            {

                Console.WriteLine($"Error during checkout: {ex.Message}");

                throw;

            }

        }




    }

}
