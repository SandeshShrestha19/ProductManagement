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
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }
        public async Task<Product> FindByIdAsync(int id)
        {
            return await _productRepository.FindByIdAsync(id);
        }
        public async Task CreateAsync(ProductDto productDto)
        {
            await _productRepository.CreateAsync(productDto);
        }
        public async Task UpdateAsync(ProductDto productDto)
        {
            await _productRepository.UpdateAsync(productDto);
        }
        public async Task DeleteAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }
    }
}
