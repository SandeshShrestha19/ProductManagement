using ProductManagement.Dtos;

namespace ProductManagement.Services.Interface
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(CategoryDto categoryDto);

        Task UpdateCategoryAsync(int id, CategoryDto categoryDto);
        Task DeleteCategoryAsync(int id);
    }
}
