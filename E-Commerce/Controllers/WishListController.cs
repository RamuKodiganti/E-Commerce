using e_comm.Services;
using e_comm.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_comm.Controllers
{
    [ApiController]

    [Route("api/[controller]")]

    [Authorize]

    public class WishListController : ControllerBase

    {

        private readonly IWishListService wishlistService;

        private readonly IAuth auth;

        public WishListController(IAuth auth, IWishListService wishlistService)

        {

            this.auth = auth;

            this.wishlistService = wishlistService;

        }

        [Authorize(Roles = "User")]

        [HttpPost("{userId}/add/{productId}")]

        public IActionResult AddToWishlist(int userId, int productId)

        {

            try

            {

                wishlistService.AddToWishlist(userId, productId);

                var message = "Added to wishlist";

                return Ok(message);

            }

            catch (Exception ex)

            {

                return StatusCode(500, ex.Message);

            }

        }

        [Authorize(Roles = "User")]

        [HttpDelete("{userId}/remove/{productId}")]

        public IActionResult RemoveFromWishlist(int userId, int productId)

        {

            try

            {

                wishlistService.RemoveFromWishlist(userId, productId);

                var message = "Removed from wishlist";

                return Ok(message);

            }

            catch (Exception ex)

            {

                return StatusCode(500, ex.Message);

            }

        }

        [AllowAnonymous]

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

        [Authorize(Roles = "User")]

        [HttpPost("move-wishlist-to-cart")]

        public IActionResult MoveWishlistToCart(int userId)

        {

            wishlistService.MoveWishlistToCart(userId);

            return Ok("Wishlist items moved to cart successfully.");

        }

    }

}