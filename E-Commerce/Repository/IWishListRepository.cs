using E_comm.Models;

namespace e_comm.Repository
{
    public interface IWishListRepository
    {
        void AddToWishlist(int userId, int productId);
        void RemoveFromWishlist(int userId, int productId);
        List<Product> GetWishlist(int userId);
    }
}
