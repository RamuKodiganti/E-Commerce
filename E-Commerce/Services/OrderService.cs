using e_comm.Exceptions;
using e_comm.Models.Orders;
using e_comm.Repository;
using Ecommerce.Exceptions;

namespace e_comm.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }


        public int PlaceOrder(Order order)
        {
            if (_orderRepository.GetOrderByOrderId(order.OrderId) != null)
            {
                throw new OrderAlreadyExistsException($"Order with Order Id {order.OrderId} already exists");

            }
            return _orderRepository.PlaceOrder(order);
        }

        public int CancelOrder(int orderId)
        {
            if (_orderRepository.GetOrderByOrderId(orderId) == null)
            {
                throw new OrderNotFoundException($"Order with Order Id{orderId} does not exists");

            }
            return _orderRepository.CancelOrder(orderId);
        }

        public Order GetOrderByOrderId(int orderId)
        {
            Order O = _orderRepository.GetOrderByOrderId(orderId);
            if (O == null)
            {
                throw new OrderNotFoundException($"Order with Order Id {orderId} does not exists");
            }
            return O;
        }

        public List<Order> GetOrders()
        {
            return _orderRepository.GetOrders();
        }
        public List<Order> GetOrderByUserId(int userId)
        {
            return _orderRepository.GetOrderByUserId(userId);
        }

        public int UpdateOrder(int orderId, Order order)
        {
            if (_orderRepository.GetOrderByOrderId(orderId) == null)
            {
                throw new OrderNotFoundException($"Order with Order Id {orderId} does not exists");
            }
            return _orderRepository.UpdateOrder(orderId, order);
        }

        public int UpdateOrderStatus(int orderId, OrderStatus newStatus)
        {
            if (_orderRepository.GetOrderByOrderId(orderId) == null)
            {
                throw new OrderNotFoundException($"Order with Order Id {orderId} does not exist");
            }
            return _orderRepository.UpdateOrderStatus(orderId, newStatus);
        }

        //public int UpdateOrderTotal(int orderId, decimal newTotalBaseAmount, decimal newShippingCost)
        //{
        //    if (_orderRepository.GetOrderByOrderId(orderId) == null)
        //    {
        //        throw new OrderNotFoundException($"Order with Order Id {orderId} does not exist");
        //    }
        //    return _orderRepository.UpdateOrderTotal(orderId, newTotalBaseAmount, newShippingCost);
        //}

        public bool UpdateTotalBaseAmount (int orderId)
        {
            if(_orderRepository.GetOrderByOrderId(orderId) == null)
            {
                throw new OrderNotFoundException($"Order with Order Id {orderId} does not exist");
            }
            return _orderRepository.UpdateTotalBaseAmount(orderId);
        }
    }
}
