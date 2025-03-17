using e_comm.DTO;
using E_comm.Models;


namespace E_comm.Services

{
    public interface IProductService
    {
        List<ProductWithCategoryDTO> GetProducts();
        ProductWithCategoryDTO GetProductById(int id);

        List<ProductWithCategoryDTO> GetProductByName(string productName);

        

        List<ProductWithCategoryDTO> GetProductByCategory(string categoryName);

        int StockAvail(int id);
        int AddProduct(Product product);
        int UpdateProduct(int id, Product product);
        int DeleteProduct(int id);

        List<Product> SortProductByPriceDesc();

        bool CategoryExists(int categoryId);
    }
}