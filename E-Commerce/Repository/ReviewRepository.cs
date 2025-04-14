using e_comm.DTO;

using e_comm.Models;

using E_comm.Models;
using E_Commerce.DTO;
using E_Commerce.Repository;
using System;

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



        public Review GetReviewByReviewId(int id)

        {

            return db.Reviews

                .Include(r => r.Product)

                .Include(r => r.User)

                .FirstOrDefault(r => r.ReviewId == id);

        }

        public List<ReviewWithDetailsDTO> GetReviewsForProduct(int productId)

        {

            return db.Reviews

                .Include(r => r.User)

                .Include(r => r.Product)

                .Where(r => r.ProductId == productId)

                 .Select(r => new ReviewWithDetailsDTO

                 {

                     ProductName = r.Product.ProductName,

                     Description = r.Product.Desc,

                     Price = r.Product.Price,

                     UserId = r.UserId,

                     UserName = r.User.UserName, // Assuming you have a Username property

                     Rating = r.Rating,

                     ReviewText = r.ReviewText,

                     PostedDate = r.PostedDate

                 })

                .ToList();

        }

        public IEnumerable<ReviewWithDetailsDTO> GetReviewsByProductName(string productName)

        {

            return db.Reviews

                            .Include(r => r.Product)

                           .Where(r => r.Product.ProductName.Equals(productName))

                            .Select(r => new ReviewWithDetailsDTO

                            {

                                ProductName = r.Product.ProductName,

                                Description = r.Product.Desc,

                                Price = r.Product.Price,

                                UserId = r.UserId,

                                UserName = r.User.UserName, // Assuming you have a Username property

                                Rating = r.Rating,

                                ReviewText = r.ReviewText,

                                PostedDate = r.PostedDate

                            })

                           .ToList();

        }

    }


}

