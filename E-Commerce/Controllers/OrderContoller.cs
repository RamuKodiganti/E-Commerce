using e_comm.Models.Orders;
using e_comm.Services;
using Microsoft.AspNetCore.Mvc;
using e_comm.Exceptions;
using Ecommerce.Exceptions;

namespace e_comm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_orderService.GetOrders());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
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

        [HttpPost]
        public IActionResult Post(Order order)
        {
            try
            {
                return StatusCode(201, _orderService.PlaceOrder(order));
            }
            catch (OrderAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Order order)
        {
            try
            {
                return Ok(_orderService.UpdateOrder(id, order));
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

        [HttpPut("{id}/Orderstatus")]
        public IActionResult UpdateOrderStatus(int id, [FromBody] OrderStatus newStatus)
        {
            try
            {
                var result = _orderService.UpdateOrderStatus(id, newStatus);
                return Ok(result);
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
        public IActionResult Delete(int OrderId)
        {
            try
            {
                return Ok(_orderService.CancelOrder(OrderId));
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

        [HttpPut("{orderId}/UpdateTotalBaseAmount")]

        public IActionResult UpdateTotalBaseAmount(int orderId)
        {
            _orderService.UpdateTotalBaseAmount(orderId);
            var updatedOrder = _orderService.GetOrderByOrderId(orderId);
            if (updatedOrder == null) 
            {
                return NotFound();
            
            }
            return Ok(updatedOrder);
        }





        //[HttpPut("{orderId}/UpdateTotalBaseAmount")]
        //public IActionResult UpdateTotalBaseAmount(int orderId)
        //{
        //    if (orderId <= 0)
        //    {
        //        return BadRequest("Invalid order ID.");
        //    }

        //    try
        //    {
        //        bool updateResult = _orderService.UpdateTotalBaseAmount(orderId);
        //        if (!updateResult)
        //        {
        //            return NotFound("Order not found.");
        //        }

        //        var updatedOrder = _orderService.GetOrderByOrderId(orderId);
        //        return Ok(updatedOrder);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception if needed
        //        return StatusCode(500, "An error occurred while updating the order.");
        //    }
        //}
    }
}