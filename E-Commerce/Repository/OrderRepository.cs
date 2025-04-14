using e_comm.Models.Orders;
using Microsoft.EntityFrameworkCore;
using E_comm.Models;
using Ecommerce.Exceptions;
namespace e_comm.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;
        private readonly string? _connectionString;

        public OrderRepository(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("SqlConnection");
        }

        public int PlaceOrderByOrderId(int orderId, string shippingAddress, PaymentStatus paymentStatus)
        {
            var order = _context.Orders_.FirstOrDefault(o => o.OrderId == orderId);
            if (order != null)
            {
                order.ShippingAddress = shippingAddress;
                order.PaymentStatus = paymentStatus;

                _context.Entry(order).State = EntityState.Modified;
                return _context.SaveChanges();
            }
            return 0; // Return 0 if the order was not found
        }

        public int CancelOrder(int orderId)
        {
            Order O = _context.Orders_.Where(x => x.OrderId == orderId).FirstOrDefault();
            _context.Orders_.Remove(O);
            return _context.SaveChanges();
        }

        public Order GetOrderByOrderId(int orderId)
        {
            return _context.Orders_.Include(oi => oi.OrderItems_).Where(x => x.OrderId == orderId).FirstOrDefault();
        }

        public List<Order> GetOrders()
        {
            return _context.Orders_.Include(oi => oi.OrderItems_).ToList();
        }

        public List<Order> GetOrderByUserId(int userId)
        {
            return _context.Orders_.Include(oi => oi.OrderItems_).Where(x => x.UserId == userId).ToList();
        }

        public int UpdateOrder(int orderId, Order order)
        {
            Order O = _context.Orders_.Where(x => x.OrderId == orderId).FirstOrDefault();
            if (O != null)
            {
                O.ShippingAddress = order.ShippingAddress;
                _context.Entry(O).State = EntityState.Modified;
                return _context.SaveChanges();
            }
            return 0;
        }

        public OrderStatus CalculateOrderStatus(Order order)
        {
            if (order.PaymentStatus == PaymentStatus.Pending)
            {
                return OrderStatus.Pending;
            }
            else if (order.PaymentStatus == PaymentStatus.Completed)
            {
                var daysSinceOrder = (DateTime.Now - order.OrderDate).Days;

                if (daysSinceOrder == 0)
                {
                    return OrderStatus.Processing;
                }
                else if (daysSinceOrder >= 2 && daysSinceOrder <= 8)
                {
                    return OrderStatus.Shipped;
                }
                else if (daysSinceOrder > 8 && daysSinceOrder <= 10)
                {
                    return OrderStatus.Delivered;
                }
            }
            return OrderStatus.Pending;
        }

        public OrderStatus GetOrderStatus(int orderId)
        {
            return _context.Orders_.Where(x => x.OrderId == orderId).Select(x => x.OrderStatus).FirstOrDefault();
        }

        public bool UpdateTotalCalculations(int orderId)
        {
            Order order = GetOrderByOrderId(orderId);
            if (order != null)
            {
                order.TotalBaseAmount = order.OrderItems_.Sum(i => i.TotalPrice);
                order.ShippingCost = order.TotalBaseAmount > 1000 ? 0 : 100;
                order.TotalAmount = order.TotalBaseAmount + order.ShippingCost;
                return _context.SaveChanges() > 0;
            }
            return false;
        }
    }
}