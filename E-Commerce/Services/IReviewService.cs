using e_comm.Models;
using e_comm.DTO;
namespace e_comm.Services
{
    public interface IReviewService
    {
        int AddReview(ReviewInputDto review);
        //Review GetReviewById(int id);

        ReviewWithDetailsDTO GetReviewByReviewId(int id);

        List<ReviewWithDetailsDTO> GetReviewsForProduct(int productId);

        IEnumerable<ReviewWithDetailsDTO> GetReviewsByProductName(string productName);

    }
}