using e_comm.DTO;
using e_comm.Models;

namespace e_comm.Repository
{
    public interface IReviewRepository
    {
        int AddReview(Review review);
        Review GetReviewByReviewId(int id);

        List<ReviewWithDetailsDTO> GetReviewsForProduct(int productId);

        IEnumerable<ReviewWithDetailsDTO> GetReviewsByProductName(string productName);


    }
}