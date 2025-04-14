using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace e_comm.Models
{
    public class ShoppingCart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public required User User { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        [Required]
        public CartStatus Status { get; set; } = CartStatus.Empty;
    }
    public enum CartStatus
    {
        Empty = 0,
        Pending = 1,
        Completed = 2
    }
}
