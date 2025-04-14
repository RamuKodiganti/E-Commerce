using e_comm.DTO;
using e_comm.Models;
using e_comm.Repository;
using E_comm.Exceptions;
using E_comm.Models;

namespace e_comm.Services
{
    public class ReviewService : IReviewService

    {

        private readonly IReviewRepository repo;

        public ReviewService(IReviewRepository repo)

        {

            this.repo = repo;

        }

        public int AddReview(ReviewInputDto reviewInput)

        {

            var review = new Review

            {

                ProductId = reviewInput.ProductId,

                UserId = reviewInput.UserId,

                Rating = reviewInput.Rating,

                ReviewText = reviewInput.ReviewText

            };

            return repo.AddReview(review);

        }



        public ReviewWithDetailsDTO GetReviewByReviewId(int id)

        {

            var review = repo.GetReviewByReviewId(id);

            //if (review == null)

            //{

            //    throw new ReviewNotFoundException($"Review with ID {id} does not exist");

            //}

            return new ReviewWithDetailsDTO

            {

                ProductName = review.Product?.ProductName,

                UserId = review.UserId,

                UserName = review.User?.UserName,

                Rating = review.Rating,

                ReviewText = review.ReviewText,

                PostedDate = review.PostedDate

            };

        }


        public List<ReviewWithDetailsDTO> GetReviewsForProduct(int productId)

        {

            //return repo.GetReviewsForProduct(productId);

            return repo.GetReviewsForProduct(productId);

        }


        public IEnumerable<ReviewWithDetailsDTO> GetReviewsByProductName(string productName)

        {

            return repo.GetReviewsByProductName(productName);

        }

        //public List<ReviewWithDetailsDTO> GetReviewsForProduct(int productId)

        //{

        //    //return repo.GetReviewsForProduct(productId);

        //    var review = repo.GetReviewsForProduct(productId);

        //    return review.Select(review => new ReviewWithDetailsDTO

        //    {

        //        ReviewId = review.ReviewId,

        //        // ProductId = review.ProductId,

        //        ProductName = review.Product?.ProductName,

        //        //UserId = review.UserId,

        //        UserName = review.User?.UserName,

        //        Rating = review.Rating,

        //        ReviewText = review.ReviewText

        //    }).ToList();

        //}

    }

}