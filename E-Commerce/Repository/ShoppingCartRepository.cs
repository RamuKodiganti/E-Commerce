using e_comm.Models;
using E_comm.Models;
using Ecommerce.Aspects;
using Microsoft.EntityFrameworkCore;
namespace E_Commerce.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository

    {

        private readonly DataContext db;

        public ShoppingCartRepository(DataContext db)

        {

            this.db = db;

        }

        public void AddCart(ShoppingCart cart)

        {

            db.ShoppingCartTable.Add(cart);

            db.SaveChanges();

        }


        public ShoppingCart GetCartByUserId(int userId)

        {

            var cart = db.ShoppingCartTable.Include(c => c.User).FirstOrDefault(c => c.UserId == userId);

            if (cart == null)

            {

                throw new CartNotFoundException("Cart not found.");

            }

            return cart;

        }

        public void UpdateCartStatus(int cartId)

        {

            var cart = db.ShoppingCartTable

                .Include(c => c.CartItems)

                .FirstOrDefault(c => c.CartId == cartId);

            if (cart == null)

            {

                throw new Exception("Cart not found.");

            }

            if (!cart.CartItems.Any())

            {

                cart.Status = CartStatus.Empty;

            }

            else if (cart.CartItems.All(ci => ci.Status == CartItemStatus.Ordered))

            {

                cart.Status = CartStatus.Completed;

            }

            else

            {

                cart.Status = CartStatus.Pending;

            }

            db.ShoppingCartTable.Update(cart);

            db.SaveChanges();

        }


        public CartStatus GetCartStatus(int cartId)

        {

            var cart = db.ShoppingCartTable

                .FirstOrDefault(c => c.CartId == cartId);

            if (cart == null)

            {

                throw new Exception("Cart not found.");

            }

            return cart.Status;

        }

    }


}
