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
    }
}
