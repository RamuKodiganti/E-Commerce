using System.Linq;
using E_comm.Models;
using static e_comm.Repository.ProductRepository;
using Microsoft.EntityFrameworkCore;

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

        //public int StockAvail(int id)
        //{
        //    Product p = db.Products.Where(x => x.PId == id).FirstOrDefault();F
        //    if (p!=null)
        //    {
        //        return p.Stock;
        //    }
        //}
        //public int StockAvail(int id)
        //{
        //    Category c = db.Categories.FirstOrDefault(x => x.CategoryID == id);
        //    if (c != null)
        //    {
        //        return c.StockQuantity;
        //    }

        //    // Throw an exception if the product is not found
        //    throw new KeyNotFoundException($"Product with ID {id} not found.");
        //}

        public int GetTotalStockForCategory(int categoryId)
        {
            // Sum the stock quantities of all products in the specified category
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

            //Category c = db.Categories.Where(x => x.CategoryID == id).FirstOrDefault();
            //c.CategoryName = category.CategoryName;
            ////c.Desc = product.Desc;
            ////c.Price = product.Price;
            ////c.Category = product.Category;
            ////c.Imgurl = product.Imgurl;
            //db.Entry<Category>(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            // db.SaveChanges();
        }

        public List<Product> GetProductsByCategorySortedByPrice(int categoryId)
        {
            return db.Products
                .Where(p => p.CategoryId == categoryId)
                //.Include(p=>p.Category)
                .OrderByDescending(p => p.Price)
                .ToList();
        }

    }
}
