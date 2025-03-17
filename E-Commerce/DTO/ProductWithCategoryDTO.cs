namespace e_comm.DTO
{
    public class ProductWithCategoryDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Desc { get; set; }
        
        public int StockQuantity { get; set; }
        public double Price { get; set; }
        public string CategoryName { get; set; }
        public string Imgurl { get; set; }
        public DateOnly AddedDate { get; set; }
    }
}

