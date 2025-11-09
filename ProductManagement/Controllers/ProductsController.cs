using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Dtos;
using ProductManagement.Repositories;
using ProductManagement.Repositories.Interface;
using ProductManagement.Services.Interface;

namespace ProductManagement.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductService productService, IProductRepository productRepository)
        {
            _productService = productService;
            _productRepository = productRepository;
        }
        [HttpGet("api/products")]
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

                var query = _productRepository.GetQueryable();

                if (!string.IsNullOrWhiteSpace(name))
                {
                    query = query.Where(x => x.Name.Contains(name.Trim()));
                }
                var products = await query
                    .Skip(skip)
                    .Take(limit)
                    .Select(x => new
                    {
                        Name = x.Name
                    }).ToListAsync();

                return Ok(new
                {
                    Message = "List of Products",
                    Data = products
                });
            }
            catch(Exception ex)
            {
                return BadRequest( new { Error = ex.Message });
            }
        }

        [HttpGet("api/products/{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            try
            {
                var product = await _productRepository.GetQueryable()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();
                if(product is null)
                {
                    throw new Exception($"Could not found product with Id {id}");
                }
                return Ok(new
                {
                    Message = "Product Details",
                    Data = product
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("api/products")]
        public async Task<IActionResult> Create([FromBody] ProductDto productCreateDto)
        {
            try
            {
                if (string.IsNullOrEmpty(productCreateDto.Name))
                {
                    throw new Exception("Product name is required!");
                }
                await _productService.CreateProductAsync(productCreateDto);

                return Ok(new
                {
                    Message = "Product created successfully!",
                    Data = productCreateDto
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    Message = "Product not created!",
                    Error = ex.Message
                });
            }
        }

        [HttpDelete("api/products/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = await _productRepository.GetQueryable()
                    .Where (x => x.Id == id)
                    .FirstOrDefaultAsync();
                if (product is null)
                {
                    throw new Exception("Product not found!");
                }
                await _productService.DeleteProductAsync(id);
                return Ok(new
                {
                    Message = "Product deleted successfully",
                    Data = id
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPut("api/products/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ProductDto productUpdateDto)
        {
            try
            {
                var product = await _productRepository.GetQueryable()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();
                
                if(product is null)
                {
                    throw new Exception("Couldn't found product...");
                }

                await _productService.UpdateProductAsync(id, productUpdateDto);
                return Ok(new
                {
                    Message = "Product updated successfully!",
                    Data = productUpdateDto
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = "Error updating product!",
                    Error = ex.Message
                });
            }
        }

    }
}
