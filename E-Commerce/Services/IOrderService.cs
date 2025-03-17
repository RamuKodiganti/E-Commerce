using e_comm.Models.Orders;

namespace e_comm.Services
{
    public interface IOrderService
    {
        //IEnumerable<Order> GetOrders();
        int PlaceOrder(Order order);
        int CancelOrder(int orderId);
        Order GetOrderByOrderId(int orderId);
        List<Order> GetOrders();
        List<Order> GetOrderByUserId(int userId);
        int UpdateOrder(int orderId, Order order);
        int UpdateOrderStatus(int orderId, OrderStatus newStatus);
        //int UpdateOrderTotal(int orderId, decimal newTotalBaseAmount, decimal newShippingCost);

        bool UpdateTotalBaseAmount(int orderId);
    }
}
