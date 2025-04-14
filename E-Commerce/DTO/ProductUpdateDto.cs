using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTO
{
    public class ProductUpdateDto
    {
        [Required(ErrorMessage = "Stock quantity must be greater than zero.")]
        [Range(1, 1500, ErrorMessage = "Stock quantity must be between 1 and 1500.")]
        public int StockQuantity { get; set; }

        [Required(ErrorMessage = "The Price is required")]
        [Range(1, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public double Price { get; set; }
    }
}