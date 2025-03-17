using e_comm.Services;
using E_comm.Models;
using Microsoft.AspNetCore.Mvc;
using E_comm.Exceptions;
using E_comm.Aspects;

namespace e_comm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService service;

        public CategoryController(ICategoryService service)
        {
            this.service = service;
        }

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
                return CreatedAtAction(nameof(GetCategoryById), new { id = category.CategoryID }, createdCategory);
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

                category.CategoryName = categoryName;
                service.UpdateCategory(category);

                return NoContent();
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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(service.DeleteCategory(id));
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

        [HttpGet("category/{categoryId}/sorted-by-price")]
        public IActionResult GetProductsByCategorySortedByPrice(int categoryId)
        {
            try
            {
                var products = service.GetProductsByCategorySortedByPrice(categoryId);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}