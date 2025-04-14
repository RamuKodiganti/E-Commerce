using e_comm.Models.Orders;
using e_comm.Services;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Exceptions;
using System.Text.Json;
using e_comm.DTO;
using Microsoft.AspNetCore.Authorization;
using e_comm.Auth;

namespace e_comm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IAuth? Auth;

        public OrderController(IAuth auth, IOrderService orderService)
        {
            Auth = auth;
            _orderService = orderService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Get()
        {
            try
            {
                return Ok(_orderService.GetOrders());
            }
            catch (OrderNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User")]

        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_orderService.GetOrderByOrderId(id));
            }
            catch (OrderNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("user/{userId}")]
        [Authorize(Roles = "Admin")]

        public IActionResult GetByUserId(int userId)
        {
            try
            {
                return Ok(_orderService.GetOrderByUserId(userId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{orderId}/place-order")]
        [Authorize(Roles = "User")]

        public IActionResult PlaceOrderByOrderId(int orderId, [FromBody] OrderUpdateRequest request)
        {
            try
            {
                var result = _orderService.PlaceOrderByOrderId(orderId, request.ShippingAddress, request.PaymentStatus);
                if (result > 0)
                {
                    var updatedOrder = _orderService.GetOrderByOrderId(orderId);
                    return Ok("Order Placed Successfully"); // Return the updated order details
                }
                return NotFound("Order not found.");
            }
            catch (OrderNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("{id}/shipping-address")]
        [Authorize(Roles = "User")]

        public IActionResult UpdateShippingAddress(int id, [FromBody] string shippingAddress)
        {
            try
            {
                _orderService.UpdateShippingAddress(id, shippingAddress);
                return Ok("Shipping address updated successfully.");
            }
            catch (OrderNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("{id}/update-status")]
        [AllowAnonymous]

        public IActionResult UpdateOrderStatusAutomatically(int id)
        {
            try
            {
                _orderService.UpdateOrderStatusAutomatically(id);
                return Ok("Order status updated successfully.");
            }
            catch (OrderNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}/status")]
        [AllowAnonymous]

        public IActionResult GetOrderStatus(int id)
        {
            try
            {
                var status = _orderService.GetOrderStatus(id);
                return Ok(status);
            }
            catch (OrderNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{OrderId}")]
        [Authorize(Roles = "User")]

        public IActionResult Delete(int OrderId)
        {
            try
            {
                _orderService.CancelOrder(OrderId);
                return Ok("Order Deleted Successfully");
            }
            catch (OrderNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{orderId}/UpdateTotalCalculations")]
        [AllowAnonymous]

        public IActionResult UpdateTotalCalculations(int orderId)
        {
            try
            {
                _orderService.UpdateTotalCalculations(orderId);
                var updatedOrder = _orderService.GetOrderByOrderId(orderId);
                if (updatedOrder == null)
                {
                    return NotFound("Order not found.");
                }
                return Ok(updatedOrder);
            }
            catch (OrderNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}