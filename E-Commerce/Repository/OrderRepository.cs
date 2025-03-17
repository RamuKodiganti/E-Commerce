using e_comm.Models.Orders;
using Microsoft.EntityFrameworkCore;
using E_comm.Models;
namespace e_comm.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;
        private readonly string _connectionString;

        public OrderRepository(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("SqlConnection");
        }

        public int PlaceOrder(Order order)
        {
            _context.Orders_.Add(order);
            int result = _context.SaveChanges();
            return result;
        }

        public int CancelOrder(int orderId)
        {
            Order O = _context.Orders_.Where(x => x.OrderId == orderId).FirstOrDefault();
            _context.Orders_.Remove(O);
            return _context.SaveChanges();
        }

        public Order GetOrderByOrderId(int orderId)
        {
            return _context.Orders_.Include(oi=> oi.OrderItems_).Where(x => x.OrderId == orderId).FirstOrDefault();
        }

        public List<Order> GetOrders()
        {
            return _context.Orders_.Include(oi => oi.OrderItems_).ToList();
        }

        public List<Order> GetOrderByUserId(int userId)
        {
            return _context.Orders_.Include(oi=> oi.OrderItems_).Where(x => x.UserId == userId).ToList();
        }

        public int UpdateOrder(int orderId, Order order)
        {
            Order O = _context.Orders_.Where(x => x.OrderId == orderId).FirstOrDefault();
            if (O != null)
            {
                O.ShippingAddress = order.ShippingAddress;
                //O.OrderDate = order.OrderDate;
                //O.UserId = order.UserId;
                //O.OrderStatus = order.OrderStatus;
                _context.Entry(O).State = EntityState.Modified;
                int result = _context.SaveChanges();
                return result;
            }
            return 0;
        }

        public int UpdateOrderStatus(int orderId, OrderStatus newStatus)
        {
            Order O = _context.Orders_.Where(x => x.OrderId == orderId).FirstOrDefault();
            if (O != null)
            {
                O.OrderStatus = newStatus; // Update the order status
                _context.Entry(O).State = EntityState.Modified;
                return _context.SaveChanges();
            }
            return 0; // Return 0 if the order was not found
        }

        //public int UpdateOrderTotal(int orderId, decimal newTotalBaseAmount, decimal newShippingCost)
        //{
        //    Order O = _context.Orders_.Where(x => x.OrderId == orderId).FirstOrDefault();
        //    if (O != null)
        //    {
        //        O.TotalBaseAmount = newTotalBaseAmount;
        //        O.ShippingCost = newShippingCost;
        //        O.TotalAmount = newTotalBaseAmount + newShippingCost; // Recalculate total amount
        //        _context.Entry<Order>(O).State = EntityState.Modified;
        //        return _context.SaveChanges();
        //    }
        //    return 0; // Return 0 if the order was not found
        //}

        public bool UpdateTotalBaseAmount(int orderId)
        {
            Order order = GetOrderByOrderId(orderId);
            if(order != null)
            {
                order.TotalBaseAmount = order.OrderItems_.Sum(i => i.TotalPrice);
                order.ShippingCost = order.TotalBaseAmount > 1000 ? 0 : 100;
                order.TotalAmount = order.TotalBaseAmount + order.ShippingCost;
                return _context.SaveChanges()>0;
            }
            return false;
        }
    }
}
