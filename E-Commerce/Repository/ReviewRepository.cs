using e_comm.Models;
using E_comm.Models;
using Microsoft.EntityFrameworkCore;
using static e_comm.Repository.ReviewRepository;

namespace e_comm.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext db;

        public ReviewRepository(DataContext db)
        {
            this.db = db;
        }

        public int AddReview(Review review)
        {
            review.PostedDate = DateOnly.FromDateTime(DateTime.Now);
            db.Reviews.Add(review);
            db.SaveChanges();
            return review.ReviewId; // Return the generated ReviewId
        }

        //public Review GetReviewById(int id)
        //{
        //    return db.Reviews.Find(id);
        //}

        //public List<Review> GetReviewsForProduct(int productId)
        //{
        //    return db.Reviews.Where(r => r.ProductId == productId).ToList();
        //}

        public Review GetReviewByReviewId(int id)
        {
            return db.Reviews
                .Include(r => r.Product)
                .Include(r => r.User)
                .FirstOrDefault(r => r.ReviewId == id);
        }

        public List<Review> GetReviewsForProduct(int productId)
        {
            return db.Reviews
                .Include(r => r.User)
                .Include(r => r.Product)
                .Where(r => r.ProductId == productId).ToList();
        }

        //public void UpdateReview(Review review)
        //{
        //    db.Reviews.Update(review);
        //    db.SaveChanges();
        //}

        //public void DeleteReview(int id)
        //{
        //    var review = db.Reviews.Find(id);
        //    if (review != null)
        //    {
        //        db.Reviews.Remove(review);
        //        db.SaveChanges();
        //    }
        //}
    }
}
