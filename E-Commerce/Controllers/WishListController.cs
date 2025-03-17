using e_comm.Services;
using Microsoft.AspNetCore.Mvc;

namespace e_comm.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WishListController : ControllerBase
    {
        private readonly IWishListService wishlistService;

        public WishListController(IWishListService wishlistService)
        {
            this.wishlistService = wishlistService;
        }

        [HttpPost("{userId}/add/{productId}")]
        public IActionResult AddToWishlist(int userId, int productId)
        {
            try
            {
                wishlistService.AddToWishlist(userId, productId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{userId}/remove/{productId}")]
        public IActionResult RemoveFromWishlist(int userId, int productId)
        {
            try
            {
                wishlistService.RemoveFromWishlist(userId, productId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{userId}")]
        public IActionResult GetWishlist(int userId)
        {
            try
            {
                var products = wishlistService.GetWishlist(userId);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}