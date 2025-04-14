using e_comm.DTO;
using E_comm.Aspects;
using E_comm.Models;
using E_Commerce.DTO;
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


        public int StockAvail(int id)

        {

            Product p = db.Products.FirstOrDefault(x => x.ProductId == id);

            if (p != null)

            {

                return p.StockQuantity;

            }

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
            var product = db.Products
             .Include(p => p.Category)
            .FirstOrDefault(p => p.ProductId == id);

            if (product != null)
            {
                var reviews = db.Reviews.Where(r => r.ProductId == product.ProductId);
                product.AverageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0;
            }

            return product;
        }

        public List<Product> GetProducts()

        {

            var products = db.Products

                .Include(p => p.Category)

                .ToList();

            foreach (var product in products)

            {

                var reviews = db.Reviews.Where(r => r.ProductId == product.ProductId);

                product.AverageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0;

            }

            return products;

        }



        public int UpdateProduct(int id, ProductUpdateDto productUpdateDto)

        {

            var product = db.Products.FirstOrDefault(x => x.ProductId == id);

            if (product == null)

            {

                throw new ProductNotFoundException("Product not found.");

            }

            product.StockQuantity = productUpdateDto.StockQuantity;

            product.Price = productUpdateDto.Price;

            db.Entry(product).State = EntityState.Modified;

            return db.SaveChanges();

        }


        public List<Product> SortProductByPriceDesc()

        {

            return db.Products

                    .FromSqlRaw("Exec SortProductByPriceDesc")

                    .AsEnumerable()

                    .ToList();

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