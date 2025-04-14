using E_comm.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace e_comm.Models
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartItemId { get; set; }

        [Required]
        public int CartId { get; set; }
        [ForeignKey("CartId")]
        [JsonIgnore]
        public virtual ShoppingCart ShoppingCart { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]

        public double TotalPrice => Product != null ? Quantity * Product.Price : 0;
        [Required]
        public CartItemStatus Status { get; set; } = CartItemStatus.Pending;

    }
    public enum CartItemStatus
    {
        Pending = 0,
        Ordered = 1,
        Removed = 2
    }
}
