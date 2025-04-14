using e_comm.DTO;
using E_comm.Models;
using E_comm.Models;
using E_Commerce.DTO;
namespace e_comm.Repository
{
    public interface IProductRepository

    {
        List<Product> GetProducts();

        Product GetProductById(int id);

        List<Product> GetProductByName(string ProductName);

        // List<Product> GetProductByPrice(double ProducPrice);

        List<Product> GetProductByCategory(string catgeoryName);

        int StockAvail(int id);

        int AddProduct(Product product);

        int UpdateProduct(int id, ProductUpdateDto product);

        int DeleteProduct(int id);

        List<Product> SortProductByPriceDesc();

        bool CategoryExists(int categoryId);

    }


}