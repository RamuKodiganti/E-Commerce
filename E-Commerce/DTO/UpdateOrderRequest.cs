using e_comm.Models.Orders;

namespace e_comm.DTO
{
    public class OrderUpdateRequest
    {
        public string ShippingAddress { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}