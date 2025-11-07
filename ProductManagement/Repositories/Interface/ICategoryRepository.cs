using ProductManagement.Dtos;
using ProductManagement.Models;

namespace ProductManagement.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoryAsync();
        Task CreateCategoryAsync(CategoryDto categoryDto);

        Task<Category> FindByIdAsync(int id);

        Task UpdateCategoryAsync(CategoryDto categoryDto);
        Task DeleteCategoryAsync(int id);

    }
}
