using E_comm.Models;
using Microsoft.EntityFrameworkCore;
using static e_comm.Repository.ProductRepository;

namespace e_comm.Repository
{
    public class ProductRepository : IProductRepository
    {

        private readonly DataContext db;

        public ProductRepository(DataContext db)

        {

            this.db = db;

        }

        //public int AddProduct(Product product)

        //{

        //    db.Products.Add(product);

        //    return db.SaveChanges();

        //}
        public int AddProduct(Product product)
        {
            product.AddedDate = DateOnly.FromDateTime(DateTime.Now);
            db.Products.Add(product);
            return db.SaveChanges();
        }

        public bool CategoryExists(int categoryId)
        {
            return db.Categories.Any(c => c.CategoryID == categoryId);
        }

        //public int StockAvail(int id)
        //{
        //    Product p = db.Products.Where(x => x.PId == id).FirstOrDefault();F
        //    if (p!=null)
        //    {
        //        return p.Stock;
        //    }
        //}
        public int StockAvail(int id)
        {
            Product p = db.Products.FirstOrDefault(x => x.ProductId == id);
            if (p != null)
            {
                return p.StockQuantity;
            }

            // Throw an exception if the product is not found
            throw new KeyNotFoundException($"Product with ID {id} not found.");
        }

        public int DeleteProduct(int id)

        {

            Product p = db.Products.Where(x => x.ProductId == id).FirstOrDefault();

            db.Products.Remove(p);

            return db.SaveChanges();

        }

        public Product GetProductById(int id)

        {

            return db.Products
                  .Include(p => p.Category) // Eagerly load the Category
                  .FirstOrDefault(p => p.ProductId == id);

        }

        public List<Product> GetProducts()

        {

            return db.Products
                .Include(p => p.Category)
                .ToList();

        }

        public int UpdateProduct(int id, Product product)

        {

            Product p = db.Products.Where(x => x.ProductId == id).FirstOrDefault();

            p.ProductName = product.ProductName;

            p.Desc = product.Desc;

            p.Price = product.Price;

            p.Category = product.Category;

            p.Imgurl = product.Imgurl;

            db.Entry(p).State = EntityState.Modified;

            return db.SaveChanges();

        }

        public List<Product> SortProductByPriceDesc()
        {

            return db.Products.FromSqlRaw("Exec SortProductByPriceDesc").AsEnumerable().ToList();
        }

        public List<Product> GetProductByName(string ProductName)
        {
            return db.Products
                .Where(p => p.ProductName == ProductName)
                .Include(p => p.Category)
                .ToList(); ;
        }



        public List<Product> GetProductByCategory(string categoryName)
        {
            return db.Products
                .Include(p => p.Category)
                .Where(p => p.Category.CategoryName.Contains(categoryName))
                .ToList();
        }
    }
}
