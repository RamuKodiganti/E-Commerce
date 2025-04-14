using System.Linq;
using E_comm.Models;
using static e_comm.Repository.ProductRepository;
using Microsoft.EntityFrameworkCore;
using e_comm.DTO;

namespace e_comm.Repository
{
    public class CategoryRepository : ICategoryRepository

    {

        private readonly DataContext db;

        public CategoryRepository(DataContext db)

        {

            this.db = db;

        }

        public int AddCategory(Category category)

        {

            db.Categories.Add(category);

            return db.SaveChanges();

        }

        public int GetTotalStockForCategory(int categoryId)

        {

            return db.Products

                     .Where(p => p.CategoryId == categoryId)

                     .Sum(p => p.StockQuantity);

        }

        public int DeleteCategory(int id)

        {

            Category c = db.Categories.Where(x => x.CategoryID == id).FirstOrDefault();

            db.Categories.Remove(c);

            return db.SaveChanges();

        }

        public Category GetCategoryById(int id)

        {

            return db.Categories.Where(x => x.CategoryID == id).FirstOrDefault();

        }

        public List<Category> GetCategories()

        {

            return db.Categories.ToList();

        }

        public void UpdateCategory(Category category)

        {

            var existingCategory = db.Categories.Find(category.CategoryID);

            if (existingCategory != null)

            {

                existingCategory.CategoryName = category.CategoryName;

                db.SaveChanges();

            }

        }

        public List<ProductWithCategoryDTO> GetProductsByCategorySortedByPrice(int categoryId)

        {

            return db.Products

                .Where(p => p.CategoryId == categoryId)

                .Include(p => p.Category)

                .OrderByDescending(p => p.Price)

                .Select(p => new ProductWithCategoryDTO

                {

                    ProductId = p.ProductId,

                    ProductName = p.ProductName,

                    Desc = p.Desc,

                    StockQuantity = p.StockQuantity,

                    Price = p.Price,

                    CategoryName = p.Category.CategoryName,

                    Imgurl = p.Imgurl,

                    AddedDate = p.AddedDate

                }).ToList();

        }

    }

}