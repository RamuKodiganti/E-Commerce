using E_comm.Exceptions;
using static E_comm.Services.ProductService;
using E_comm.Models;
using E_comm.Aspects;
using e_comm.DTO;
using e_comm.Repository;

namespace E_comm.Services

{

    public class ProductService : IProductService

    {

        private readonly IProductRepository repo;

        public ProductService(IProductRepository repo)

        {

            this.repo = repo;

        }


        public int StockAvail(int id)

        {

            if (repo.GetProductById(id) == null)

            {

                throw new ProductNotFoundException($"Product with product id {id} does not exists");

            }

            return repo.StockAvail(id);

        }


        public int DeleteProduct(int id)

        {

            if (repo.GetProductById(id) == null)

            {

                throw new ProductNotFoundException($"Product with product id {id} does not exists");

            }

            return repo.DeleteProduct(id);

        }

        public ProductWithCategoryDTO GetProductById(int id)

        {

            var p = repo.GetProductById(id);

            if (p == null)

            {

                throw new ProductNotFoundException($"Product with product id {id} does not exists");

            }

            return new ProductWithCategoryDTO

            {

                ProductId = p.ProductId,

                ProductName = p.ProductName,

                Desc = p.Desc,

                StockQuantity = p.StockQuantity,

                Price = p.Price,

                CategoryName = p.Category?.CategoryName, // Use the CategoryName

                Imgurl = p.Imgurl,

                AddedDate = p.AddedDate

            };

        }

        public List<ProductWithCategoryDTO> GetProducts()

        {

            var products = repo.GetProducts();

            return products.Select(p => new ProductWithCategoryDTO

            {

                ProductId = p.ProductId,

                ProductName = p.ProductName,

                Desc = p.Desc,

                StockQuantity = p.StockQuantity,

                Price = p.Price,

                CategoryName = p.Category?.CategoryName, // Use the CategoryName

                Imgurl = p.Imgurl,

                AddedDate = p.AddedDate

            }).ToList();

        }

        public int UpdateProduct(int id, Product product)

        {

            if (repo.GetProductById(id) == null)

            {

                throw new ProductNotFoundException($"Product with customer id {id} does not exists");

            }

            return repo.UpdateProduct(id, product);

        }

        public List<Product> SortProductByPriceDesc()

        {

            return repo.SortProductByPriceDesc();

        }

        public int AddProduct(Product product)

        {

            if (repo.GetProductById(product.ProductId) != null)

            {

                throw new ProductAlreadyExistsException($"Product with product id {product.ProductId} already exists");

            }

            return repo.AddProduct(product);

        }

        //public Product AddProduct(Product product)

        //{

        //    return repo.AddProduct(product);

        //}

        public bool CategoryExists(int categoryId)

        {

            return repo.CategoryExists(categoryId);

        }

        public List<ProductWithCategoryDTO> GetProductByName(string productName)

        {

            var products = repo.GetProductByName(productName);

            return products.Select(p => new ProductWithCategoryDTO

            {

                ProductId = p.ProductId,

                ProductName = p.ProductName,

                Desc = p.Desc,

                StockQuantity = p.StockQuantity,

                Price = p.Price,

                CategoryName = p.Category?.CategoryName, // Use the CategoryName

                Imgurl = p.Imgurl,

                AddedDate = p.AddedDate

            }).ToList();

        }

        public List<ProductWithCategoryDTO> GetProductByCategory(string categoryName)

        {

            var products = repo.GetProductByCategory(categoryName);

            return products.Select(p => new ProductWithCategoryDTO

            {

                ProductId = p.ProductId,

                ProductName = p.ProductName,

                Desc = p.Desc,

                StockQuantity = p.StockQuantity,

                Price = p.Price,

                CategoryName = p.Category?.CategoryName, // Use the CategoryName

                Imgurl = p.Imgurl,

                AddedDate = p.AddedDate

            }).ToList();

        }

    }

}

