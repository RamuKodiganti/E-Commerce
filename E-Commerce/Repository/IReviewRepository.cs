using e_comm.Models;

namespace e_comm.Repository
{
    public interface IReviewRepository
    {
        int AddReview(Review review);
        //review product by productid as well as productname
        Review GetReviewByReviewId(int id);

        List<Review> GetReviewsForProduct(int productId);

        //  void DeleteReview(int id);


    }
}
