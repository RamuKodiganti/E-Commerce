using System.ComponentModel.DataAnnotations;

namespace e_comm.Models
{
    public class CartItemDto
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        
        public int Quantity { get; set; }
    }
}