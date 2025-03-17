using E_comm.Models;

namespace e_comm.Services
{
    public interface ICategoryService
    {
        List<Category> GetCategories();
        Category GetCategoryById(int id);

        int GetTotalStockForCategory(int id);
        int AddCategory(Category category);

        void UpdateCategory(Category category);

        int DeleteCategory(int id);

        List<Product> GetProductsByCategorySortedByPrice(int categoryId);


    }
}
