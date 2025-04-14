using e_comm.Models;
using e_comm.Models.Orders;
using e_comm.Services;
using E_Commerce.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_comm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;

        public CartItemController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [HttpPost]
        [Authorize(Roles = "user")]

        public async Task<IActionResult> AddCartItem(CartItemDto cartItemDto)
        {
            try
            {
                var cartItem = new CartItem
                {
                    CartId = cartItemDto.CartId,
                    ProductId = cartItemDto.ProductId,
                    Quantity = cartItemDto.Quantity
                };

                await _cartItemService.AddCartItemAsync(cartItem);
                return CreatedAtAction(nameof(GetCartItem), new { id = cartItem.CartItemId }, cartItem);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }   



        [HttpGet("cart/{cartId}")]
        [Authorize(Roles = "user")]

        public async Task<ActionResult<IEnumerable<CartItem>>> GetCartItemsByCartId(int cartId)
        {
            try
            {
                var items = await _cartItemService.GetCartItemsByCartIdAsync(cartId);

                if (items == null || !items.Any())
                {
                    return NotFound("Cart not found or is empty.");
                }

                return Ok(items);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CartItem>> GetCartItem(int id)
        {
            try
            {
                var cartItem = await _cartItemService.GetCartItemByIdAsync(id);
                if (cartItem == null)
                {
                    return NotFound("CartItemId Not Found.");
                }
                return Ok(cartItem);
            }
            catch (Exception )
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "user")]

        public async Task<IActionResult> UpdateCartItem(int id, CartItem cartItem)
        {
            if (id != cartItem.CartItemId)
            {
                return BadRequest("CartItem ID mismatch.");
            }

            try
            {
                await _cartItemService.UpdateCartItemAsync(cartItem);
                return NoContent();
            }
            catch (Exception )
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "user")]

        public async Task<IActionResult> DeleteCartItem(int id)
        {
            try
            {
                await _cartItemService.DeleteCartItemAsync(id);
                return NoContent();
            }
            catch (Exception )
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPost("checkout/{cartId}")]
        public async Task<IActionResult> Checkout(int cartId)
        {
            try
            {
                var order = await _cartItemService.CheckOutCartAsync(cartId);
                if (order == null)
                {
                    return NotFound("Cart not found or empty.");
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                Console.WriteLine($"Checkout error: {ex.Message}");
                return StatusCode(500, "An error occurred during checkout.");
            }
        }

       
    }
}
