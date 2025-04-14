using e_comm.DTO;
using e_comm.Models;
using e_comm.Auth;
using e_comm.Services;
using Microsoft.AspNetCore.Mvc;
using E_comm.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace e_comm.Controllers
{
    [ApiController]

    [Route("api/[controller]")]

    //[Authorize]

    public class ReviewController : ControllerBase

    {

        private readonly IReviewService service;

        private readonly IAuth auth;

        public ReviewController(IAuth auth, IReviewService service)

        {

            this.auth = auth;

            this.service = service;

        }

        //[Authorize(Roles = "User")]

        [HttpPost]

        public IActionResult AddReview(ReviewInputDto reviewInput)

        {

            try

            {

                if (!ModelState.IsValid)

                {

                    return BadRequest(ModelState);

                }

                var reviewId = service.AddReview(reviewInput);

                var message = "Review added successfully";

                //return CreatedAtAction(nameof(GetReviewByReviewId), new { id = reviewId }, reviewInput);

                return Ok(message);

            }

            catch (Exception ex)

            {

                Console.WriteLine(ex.InnerException?.Message);

                return StatusCode(500, "An error occurred while processing your request.");

            }

        }

        [Authorize(Roles = "Admin")]

        [HttpGet("{id}")]

        public IActionResult GetReviewByReviewId(int id)

        {

            try

            {

                var review = service.GetReviewByReviewId(id);

                if (review == null)

                {

                    return NotFound();

                }

                return Ok(review);

            }

            catch (Exception ex)

            {

                return StatusCode(500, ex.Message);

            }

        }

        [AllowAnonymous]

        [HttpGet("product/{productId}")]

        public IActionResult GetReviewsForProduct(int productId)

        {

            try

            {

                var reviews = service.GetReviewsForProduct(productId);

                return Ok(reviews);

            }

            catch (Exception ex)

            {

                return StatusCode(500, ex.Message);

            }

        }


        [AllowAnonymous]

        [HttpGet("Search/{ProductName}")]

        public IActionResult GetReviewsByProductName(string ProductName)

        {

            if (string.IsNullOrWhiteSpace(ProductName))

            {

                return BadRequest(new { message = "Product name is required." });

            }

            try

            {

                var reviews = service.GetReviewsByProductName(ProductName);

                if (reviews != null && reviews.Any())

                {

                    return Ok(reviews);

                }

                else

                {

                    return NotFound(new { message = "No reviews found for the specified product." });

                }

            }

            catch (Exception ex)

            {

                // Log the exception here

                Console.WriteLine(ex);

                return StatusCode(500, new { message = "An error occurred while processing your request." });

            }

        }

    }

}