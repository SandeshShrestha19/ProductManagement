using ProductManagement.Dtos;
using ProductManagement.Models;

namespace ProductManagement.Services.Interface
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategoryAsync();
        Task CreateCategoryAsync(CategoryDto categoryDto);

        Task<Category> FindByIdAsync(int id);

        Task UpdateCategoryAsync(CategoryDto categoryDto);
        Task DeleteCategoryAsync(int id);
    }
}
