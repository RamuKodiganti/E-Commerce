using System.ComponentModel.DataAnnotations;

namespace e_comm.DTO
{
    public class ReviewInputDto
    {
        //[Required]
        //public int ReviewId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        public string ReviewText { get; set; }
    }
}
