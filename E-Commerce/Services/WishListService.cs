using e_comm.Repository;
using E_comm.Models;

namespace e_comm.Services
{
    public class WishListService:IWishListService
    {
        private readonly IWishListRepository repo;
        public WishListService(IWishListRepository repo)
        {
            this.repo = repo;
        }


        public void AddToWishlist(int userId, int productId)
        {
            repo.AddToWishlist(userId, productId);
        }

        public void RemoveFromWishlist(int userId, int productId)
        {
            repo.RemoveFromWishlist(userId, productId);
        }

        public List<Product> GetWishlist(int userId)
        {
            return repo.GetWishlist(userId);
        }
    }
}
