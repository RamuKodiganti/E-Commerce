namespace e_comm.DTO
{
    public class ReviewWithDetailsDTO
    {
        public int ReviewId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public DateOnly PostedDate { get; set; }
    }
}