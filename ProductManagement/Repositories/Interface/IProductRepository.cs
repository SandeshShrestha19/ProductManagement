
using ProductManagement.Models;

namespace ProductManagement.Repositories.Interface
{
    public interface IProductRepository
    {
        IQueryable<Product> GetQueryable();
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task DeleteAsync(Product product);
    }
}
