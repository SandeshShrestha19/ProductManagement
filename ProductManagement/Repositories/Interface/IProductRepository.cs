using ProductManagement.Dtos;
using ProductManagement.Models;

namespace ProductManagement.Repositories.Interface
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> FindByIdAsync(int id);
        Task CreateAsync(ProductDto productDto);
        Task UpdateAsync(ProductDto productDto);
        Task DeleteAsync(int id);
    }
}
