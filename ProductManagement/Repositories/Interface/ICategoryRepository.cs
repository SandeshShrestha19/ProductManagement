
using ProductManagement.Models;

namespace ProductManagement.Repositories.Interface
{
    public interface ICategoryRepository
    {
        IQueryable<Category> GetQueryable();
        Task<Category> AddAsync(Category category);
        Task<Category> UpdateAsync(Category category);
        Task DeleteAsync(Category category);
    }
}
