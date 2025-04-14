using e_comm.Models;
using E_comm.Models;

namespace e_comm.Repository
{
    public class WishList_Repository : IWishListRepository

    {

        private readonly DataContext db;

        public WishList_Repository(DataContext db)

        {

            this.db = db;

        }

        public void AddToWishlist(int userId, int productId)

        {

            var wishlistItem = new Wishlist { UserId = userId, ProductId = productId };

            db.Wishlists.Add(wishlistItem);

            db.SaveChanges();

        }

        public void RemoveFromWishlist(int userId, int productId)

        {

            var item = db.Wishlists.FirstOrDefault(w => w.UserId == userId && w.ProductId == productId);

            if (item != null)

            {

                db.Wishlists.Remove(item);

                db.SaveChanges();

            }

        }

        public List<Product> GetWishlist(int userId)

        {

            return db.Wishlists

                .Where(w => w.UserId == userId)

                .Select(w => w.Product)

                .ToList();

        }


        public void MoveWishlistToCart(int userId)

        {

            var wishlistItems = db.Wishlists.Where(w => w.UserId == userId).ToList();

            foreach (var item in wishlistItems)

            {

                var cart = db.ShoppingCartTable.FirstOrDefault(c => c.UserId == userId);

                if (cart != null)

                {

                    var cartItem = db.CartItems.FirstOrDefault(c => c.CartId == cart.CartId && c.ProductId == item.ProductId);

                    if (cartItem != null)

                    {

                        cartItem.Quantity += 1;

                    }

                    else

                    {

                        var newCartItem = new CartItem

                        {

                            CartId = cart.CartId,

                            ProductId = item.ProductId,

                            Quantity = 1

                        };

                        db.CartItems.Add(newCartItem);

                    }

                }

                db.Wishlists.Remove(item);

            }

            db.SaveChanges();

        }

    }

}
