using ProductManagement.Dtos;

namespace ProductManagement.Services.Interface
{
    public interface IProductService
    {
        Task CreateProductAsync(ProductDto productCreateDto);
        Task DeleteProductAsync(int id);
        Task UpdateProductAsync(int id, ProductDto productUpdateDto);
    }
}
