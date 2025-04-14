using e_comm.Models.Orders;

namespace e_comm.Services
{
    public interface IOrderService
    {
        //IEnumerable<Order> GetOrders();
        int PlaceOrderByOrderId(int orderId, string shippingAddress, PaymentStatus paymentStatus);
        int CancelOrder(int orderId);
        Order GetOrderByOrderId(int orderId);
        List<Order> GetOrders();
        List<Order> GetOrderByUserId(int userId);
        void UpdateShippingAddress(int orderId, string shippingAddress);
        void UpdateOrderStatusAutomatically(int orderId);
        OrderStatus GetOrderStatus(int orderId);
        bool UpdateTotalCalculations(int orderId);
    }
}