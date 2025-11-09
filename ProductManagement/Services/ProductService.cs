using Microsoft.EntityFrameworkCore;
using ProductManagement.Dtos;
using ProductManagement.Models;
using ProductManagement.Repositories.Interface;
using ProductManagement.Services.Interface;

namespace ProductManagement.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        public async Task CreateProductAsync(ProductDto productCreateDto)
        {
            try
            {
                var product = new Product
                {
                    Name = productCreateDto.Name,
                    Price = productCreateDto.Price,
                    Description = productCreateDto.Description ?? string.Empty,
                    CategoryId = productCreateDto.CategoryId
                };
                await _productRepository.CreateAsync(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            try
            {
                var product = await _productRepository.GetQueryable().FirstOrDefaultAsync(x => x.Id == id);
                if (product is null)
                {
                    throw new Exception("Product not found!");
                }
                await _productRepository.DeleteAsync(product);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateProductAsync(int id, ProductDto productCreateDto)
        {
            try
            {
                var product = await _productRepository.GetQueryable().FirstOrDefaultAsync(x => x.Id == id);
                if (product is null)
                {
                    throw new Exception("Product not found!");
                }
                await _productRepository.UpdateAsync(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}
