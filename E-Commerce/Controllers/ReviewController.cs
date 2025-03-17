using e_comm.DTO;
using e_comm.Models;
using e_comm.Services;
using Microsoft.AspNetCore.Mvc;
using E_comm.Exceptions;

namespace e_comm.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService service;

        public ReviewController(IReviewService service)
        {
            this.service = service;
        }

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
                return CreatedAtAction(nameof(GetReviewByReviewId), new { id = reviewId }, reviewInput);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

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
    }
}