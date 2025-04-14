using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace e_comm.DTO
{
    public class ReviewWithDetailsDTO
    {
        public string ProductName { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public DateOnly PostedDate { get; set; }
    }
}