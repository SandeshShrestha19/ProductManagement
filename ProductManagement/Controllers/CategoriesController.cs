using Microsoft.AspNetCore.Mvc;
using ProductManagement.Dtos;
using ProductManagement.Services;
using ProductManagement.Services.Interface;

namespace ProductManagement.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("api/categories")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var categories = await _categoryService.GetAllCategoryAsync();
                return Ok(new
                {
                    Message = "List of Categories",
                    Data = categories
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("api/categories/{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            try
            {
                var category = await _categoryService.FindByIdAsync(id);
                return Ok(new
                {
                    Message = "Category Details",
                    Data = category
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("api/categories")]
        public async Task<IActionResult> Create([FromBody] CategoryDto categoryDto)
        {
            try
            {
                if (categoryDto == null)
                {
                    return BadRequest(new { Message = "Invalid category data." });
                }

                await _categoryService.CreateCategoryAsync(categoryDto);

                return Ok(new
                {
                    Message = "Product created successfully!",
                    Data = categoryDto
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = "Category not created!",
                    Error = ex.Message
                });
            }
        }

        [HttpDelete("api/categories/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(id);
                return Ok(new
                {
                    Message = "Category Deleted Successfullly!",
                    Data = id
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPut("api/categories/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CategoryDto categoryDto)
        {
            try
            {
                categoryDto.Id = id;
                await _categoryService.UpdateCategoryAsync(categoryDto);
                return Ok(new
                {
                    Message = "Category updated successfully!",
                    Data = categoryDto
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = "Error updating category!",
                    Error = ex.Message
                });
            }
        }
    }
}
