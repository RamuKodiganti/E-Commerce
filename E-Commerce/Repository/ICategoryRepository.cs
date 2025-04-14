using e_comm.DTO;
using E_comm.Models;

namespace e_comm.Repository
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
        Category GetCategoryById(int id);

        List<ProductWithCategoryDTO> GetProductsByCategorySortedByPrice(int categoryId);
        int GetTotalStockForCategory(int id);
        int AddCategory(Category category);

        void UpdateCategory(Category category);

        int DeleteCategory(int id);
    }
}