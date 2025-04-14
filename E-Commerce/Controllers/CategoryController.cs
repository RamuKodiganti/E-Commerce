using e_comm.Services;
using E_comm.Models;
using Microsoft.AspNetCore.Mvc;
using E_comm.Exceptions;
using E_comm.Aspects;
using e_comm.Auth;
using Microsoft.AspNetCore.Authorization;

namespace e_comm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService service;
        private readonly IAuth auth;
        public CategoryController(IAuth auth, ICategoryService service)
        {
            this.auth = auth;
            this.service = service;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(service.GetCategories());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            try
            {
                return Ok(service.GetCategoryById(id));
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Post(Category category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdCategory = service.AddCategory(category);
                var message = "New Category added successfully";
                return Ok(message);
            }
            catch (CategoryAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string categoryName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(categoryName))
                {
                    return BadRequest("Category Name is required.");
                }

                var category = service.GetCategoryById(id);
                if (category == null)
                {
                    return NotFound($"Category with ID {id} does not exist.");
                }

                var message = "Category updated.";
                category.CategoryName = categoryName;
                service.UpdateCategory(category);

                return Ok(message);
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                service.DeleteCategory(id);
                var message = "Category deleted successfully";
                return Ok(message);
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("category/{id}/stock")]
        public IActionResult GetTotalStockForCategory(int id)
        {
            try
            {
                var totalStock = service.GetTotalStockForCategory(id);
                return Ok(totalStock);
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("category/{categoryId}/sorted-by-price")]
        public IActionResult GetProductsByCategorySortedByPrice(int categoryId)
        {
            try
            {
                var products = service.GetProductsByCategorySortedByPrice(categoryId);
                return Ok(products);
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}