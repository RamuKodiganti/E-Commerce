using e_comm.Models;
using e_comm.Models.Orders;
using e_comm.Services;
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
        public async Task<ActionResult<IEnumerable<CartItem>>> GetCartItemsByCartId(int cartId)
        {
            try
            {
                var items = await _cartItemService.GetCartItemsByCartIdAsync(cartId);
                return Ok(items);
            }
            catch (Exception ex)
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
                    return NotFound();
                }
                return Ok(cartItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
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
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            try
            {
                await _cartItemService.DeleteCartItemAsync(id);
                return NoContent();
            }
            catch (Exception ex)
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
                return StatusCode(500, "An error occurred during checkout.");
            }
        }

        //total price
        //[HttpGet("cart/{cartId}/total")]
        //public async Task<ActionResult<decimal>> GetTotalPriceByCartId(int cartId)
        //{
        //    try
        //    {
        //        var cartItems = await _cartItemService.GetCartItemsByCartIdAsync(cartId);

        //        if (cartItems == null || !cartItems.Any())
        //        {
        //            return NotFound(new { Message = "Cart not found or is empty." });
        //        }

        //        var totalPrice = await _cartItemService.GetTotalPriceByCartIdAsync(cartId);
        //        return Ok(new { TotalPrice = totalPrice });

        //    }
        //    catch (Exception)
        //    {
        //        // Log the exception
        //        return StatusCode(500, "Internal server error");
        //    }
        //}

    }
}
