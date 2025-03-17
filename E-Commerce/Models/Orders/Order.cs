using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace e_comm.Models.Orders
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; }

        //[ForeignKey("UserId")]
        //public User User { get; set; }

        [Required(ErrorMessage = "Shipping Address is required.")]
        [MaxLength(200, ErrorMessage = "shipping address cannot be exceeded 200 characters")]
        public string ShippingAddress { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalBaseAmount { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ShippingCost { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        public PaymentStatus PaymentStatus { get; set; }

        [Required]
        [EnumDataType(typeof(OrderStatus), ErrorMessage = "Invalid Order Status.")]
        public OrderStatus OrderStatus { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public virtual ICollection<OrderItem> OrderItems_ { get; set; } = new List<OrderItem>();
        //public virtual ICollection<OrderItem> OrderItems_ { get; set; }


    }
}
