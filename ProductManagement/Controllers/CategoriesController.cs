using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using ProductManagement.Dtos;
using ProductManagement.Repositories.Interface;
using ProductManagement.Services;
using ProductManagement.Services.Interface;

namespace ProductManagement.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryService categoryService, ICategoryRepository categoryRepository)
        {
            _categoryService = categoryService;
            _categoryRepository = categoryRepository;
        }

        [HttpGet("api/categories")]
        public async Task<IActionResult> Index(int skip, int limit, string? name = "")
        {
            try
            {
                if (skip < 0)
                {
                    return BadRequest(new { Error = "Skip must be non-negative" });
                }

                if (limit <= 0 || limit > 1000)
                {
                    return BadRequest(new { Error = "Limit must be between 1 and 1000" });
                }

                var query = _categoryRepository.GetQueryable();

                if (!string.IsNullOrWhiteSpace(name))
                {
                    query = query.Where(x => x.Name.Contains(name.Trim()));
                }

                var categories = await query
                    .Skip(skip)
                    .Take(limit)
                    .Select(x => new
                    {
                        Name = x.Name
                    }).ToListAsync();

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
                var category = await _categoryRepository.GetQueryable()
                    .Where(x =>x.Id == id)
                    .FirstOrDefaultAsync();

                if (category is null)
                {
                    throw new Exception($"Could not found category with Id {id}");
                }

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
        public async Task<IActionResult> Create([FromBody] CategoryDto input)
        {
            try
            {
                if (string.IsNullOrEmpty(input.Name))
                {
                    throw new Exception("Category name is required!");
                }
                await _categoryService.CreateCategoryAsync(input);

                return Ok(new
                {
                    Message = "Product created successfully!",
                    Data = input
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
                var category = await _categoryRepository.GetQueryable()
                    .Where (x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (category is null)
                {
                    throw new Exception("Could not found category...");
                }
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
        public async Task<IActionResult> Edit(int id, [FromBody] CategoryDto input)
        {
            try
            {
                var category = await _categoryRepository.GetQueryable()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (category is null)
                {
                    throw new Exception("Could not found category...");
                }

                await _categoryService.UpdateCategoryAsync(id, input);
                return Ok(new
                {
                    Message = "Category updated successfully!",
                    Data = input
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
