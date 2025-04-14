using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTO
{
    public class CartItemDto
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        [Range(1,100,ErrorMessage ="Quantity should be between 1 and 100")]
        public int Quantity { get; set; }
    }
}