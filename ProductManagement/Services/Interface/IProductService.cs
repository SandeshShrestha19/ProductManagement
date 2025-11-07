using ProductManagement.Dtos;
using ProductManagement.Models;

namespace ProductManagement.Services.Interface
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> FindByIdAsync(int id);
        Task CreateAsync(ProductDto productDto);
        Task DeleteAsync(int id);
        Task UpdateAsync(ProductDto productDto);
    }
}
