using e_comm.Models.Orders;
using e_comm.Repository;
using Ecommerce.Exceptions;
using e_comm.Aspects;

namespace e_comm.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }


        public int PlaceOrderByOrderId(int orderId, string shippingAddress, PaymentStatus paymentStatus)
        {
            if (_orderRepository.GetOrderByOrderId(orderId) == null)
            {
                throw new OrderNotFoundException($"Order with Order Id {orderId} does not exist");
            }
            return _orderRepository.PlaceOrderByOrderId(orderId, shippingAddress, paymentStatus);
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

        public void UpdateShippingAddress(int orderId, string shippingAddress)
        {
            var order = _orderRepository.GetOrderByOrderId(orderId);
            if (order == null)
            {
                throw new OrderNotFoundException($"Order with Order Id {orderId} does not exist");
            }
            order.ShippingAddress = shippingAddress;
            _orderRepository.UpdateOrder(orderId, order);
        }

        public void UpdateOrderStatusAutomatically(int orderId)
        {
            var order = _orderRepository.GetOrderByOrderId(orderId);
            if (order == null)
            {
                throw new OrderNotFoundException($"Order with ID {orderId} does not exist.");
            }

            order.OrderStatus = _orderRepository.CalculateOrderStatus(order);
            _orderRepository.UpdateOrder(orderId, order);
        }

        public OrderStatus GetOrderStatus(int orderId)
        {
            var orderStatus = _orderRepository.GetOrderStatus(orderId);
            if (orderStatus == default(OrderStatus))
            {
                throw new OrderNotFoundException($"Order with ID {orderId} does not exist.");
            }
            return orderStatus;
        }

        public bool UpdateTotalCalculations(int orderId)
        {
            if (_orderRepository.GetOrderByOrderId(orderId) == null)
            {
                throw new OrderNotFoundException($"Order with Order Id {orderId} does not exist");
            }
            return _orderRepository.UpdateTotalCalculations(orderId);
        }
    }
}