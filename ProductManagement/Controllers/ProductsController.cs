using Microsoft.AspNetCore.Mvc;
using ProductManagement.Dtos;
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
        public async Task<IActionResult> Index()
        {
            try
            {
                var products = await _productRepository.GetAllProductsAsync();
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
                var product = await _productService.FindByIdAsync(id);
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
        public async Task<IActionResult> Create([FromBody] ProductDto productDto)
        {
            try
            {
                if (productDto == null)
                {
                    return BadRequest(new { Message = "Invalid product data." });
                }

                await _productService.CreateAsync(productDto);

                return Ok(new
                {
                    Message = "Product created successfully!",
                    Data = productDto
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
                await _productService.DeleteAsync(id);
                return Ok(new
                {
                    Message = "Product Deleted Successfullly!",
                    Data = id
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPut("api/products/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ProductDto productDto)
        {
            try
            {
                productDto.Id = id;
                await _productService.UpdateAsync(productDto);
                return Ok(new
                {
                    Message = "Product updated successfully!",
                    Data = productDto
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
