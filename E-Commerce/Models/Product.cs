using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace E_comm.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(30, ErrorMessage = "Product name must be less than 30 characters")]
        public string ProductName { get; set; }


        [StringLength(50, ErrorMessage = "Description must be less than 50 characters")]
        [Required]
        public string Desc { get; set; }


        [Required(ErrorMessage = "Stock quantity must be greater than zero.")]
        [Range(1, 1500, ErrorMessage = "Stock quantity  must be greater than zero.")]
        public int StockQuantity { get; set; }


        [Required(ErrorMessage = "The Price is requided")]
        [Range(1, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public double Price { get; set; }


        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }


        [ForeignKey("CategoryId")]
        public Category Category { get; set; }


        //[RegularExpression(@".*\.com.*", ErrorMessage = "The URL must contain '.com'.")]
        public string Imgurl { get; set; }

        public DateOnly AddedDate { get; set; }

        [NotMapped]
        public double AverageRating { get; set; }
    }
}

