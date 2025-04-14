using E_comm.Models;
using E_comm.Services;
using Microsoft.AspNetCore.Mvc;
using E_comm.Exceptions;
using e_comm.Auth;
using e_comm.DTO;
using E_comm.Aspects;
using Microsoft.AspNetCore.Authorization;
using e_comm.Controllers;
using E_Commerce.DTO;

namespace E_comm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService service;
        private readonly IAuth auth;

        public ProductController(IAuth auth, IProductService service)
        {
            this.auth = auth;
            this.service = service;
        }

        //[AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(service.GetProducts());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            try
            {
                return Ok(service.GetProductById(id));
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //[AllowAnonymous]
        [HttpGet("Search")]
        public IActionResult GetProductByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(new { message = "Product name is required." });
            }

            try
            {
                var products = service.GetProductByName(name);
                if (products != null && products.Any())
                {
                    return Ok(products);
                }
                else
                {
                    return NotFound(new { message = "Product not found" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }


        //[Authorize(Roles = "Admin")]
        [HttpGet("stock/{id}")]
        public IActionResult GetStockAvailability(int id)
        {
            try
            {
                var product = service.GetProductById(id);
                if (product != null)
                {
                    int stock = product.StockQuantity;
                    return Ok($"The available stock of {product.ProductName}: {stock}");
                }
                return NotFound($"Product with ID {id} not found.");
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Post(ProductCreateDto productDto)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryExists = service.CategoryExists(productDto.CategoryId);

            if (!categoryExists)
            {
                return NotFound("Category does not exist.");
            }

            var product = new Product
            {
                ProductName = productDto.ProductName,
                Desc = productDto.Desc,
                StockQuantity = productDto.StockQuantity,
                Price = productDto.Price,
                CategoryId = productDto.CategoryId,
                Imgurl = productDto.Imgurl
            };

            var createdProduct = service.AddProduct(product);
            var Message = "Product added successfully";

            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, Message);
        }


        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductUpdateDto product)
        {
            try
            {
                var result = service.UpdateProduct(id, product);
                return Ok("Product updated.");
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        //[Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {

            try
            {
                var productExists = service.GetProductById(id);
                if (productExists == null)
                {
                    return NotFound(new { message = "Product not found" });
                }
                var message = "Product deleted successfully";
                service.DeleteProduct(id);
                return Ok(message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the product", error = ex.Message });
            }
        }


        //[AllowAnonymous]
        [HttpGet("sorted-by-price")]
        public IActionResult SortProductByPriceDesc()
        {
            try
            {
                var products = service.SortProductByPriceDesc();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [AllowAnonymous]
        [HttpGet("ByCategory")]
        public IActionResult GetProductByCategory(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                return BadRequest(new { message = "Category name is required." });
            }

            try
            {
                var products = service.GetProductByCategory(categoryName);
                if (products != null)
                {
                    return Ok(products);
                }
                else
                {
                    return NotFound(new { message = "No products found for the specified category." });
                }
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }
    }
}