using E_comm.Exceptions;
using static E_comm.Services.CategoryService;
using E_comm.Models;
//using E_comm.Repository;
using E_comm.Aspects;
using e_comm.Services;
using e_comm.Repository;


namespace E_comm.Services
{
    public class CategoryService : ICategoryService

    {

        private readonly ICategoryRepository repo;

        public CategoryService(ICategoryRepository repo)
        {
            this.repo = repo;
        }

        public int AddCategory(Category category)
        {
            if (repo.GetCategoryById(category.CategoryID) != null)
            {
                throw new CategoryAlreadyExistsException($"Category with category id {category.CategoryID} already exists");
            }
            return repo.AddCategory(category);
        }


        public int GetTotalStockForCategory(int id)
        {
            var category = repo.GetCategoryById(id);
            if (category == null)
            {
                throw new CategoryNotFoundException($"Category with category id {id} does not exist");
            }

            string categoryName = category.CategoryName;

            // Get the total stock for the category
            int totalstock = repo.GetTotalStockForCategory(id);
            Console.WriteLine($"The total available stock in {categoryName} is: {totalstock}");

            return totalstock;
        }


        public int DeleteCategory(int id)
        {
            if (repo.GetCategoryById(id) == null)
            {

                throw new CategoryNotFoundException($"Category with category id {id} does not exists");
            }
            return repo.DeleteCategory(id);
        }

        public Category GetCategoryById(int id)
        {
            Category c = repo.GetCategoryById(id);
            if (c == null)
            {
                throw new CategoryNotFoundException($"Category with category id {id} does not exists");
            }
            return c;
        }

        public List<Category> GetCategories()
        {
            return repo.GetCategories();
        }

        public void UpdateCategory(Category category)
        {
            //if (repo.GetCategoryById(id) == null)
            //{
            //    throw new CategoryNotFoundException($"Category with category id {id} does not exists");
            //}
             repo.UpdateCategory(category);
        }

        public List<Product> GetProductsByCategorySortedByPrice(int categoryId)
        {
            return repo.GetProductsByCategorySortedByPrice(categoryId);
        }
    }

}
