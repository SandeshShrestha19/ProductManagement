using ProductManagement.Dtos;
using ProductManagement.Models;
using ProductManagement.Repositories.Interface;
using ProductManagement.Services.Interface;

namespace ProductManagement.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> GetAllCategoryAsync()
        {
            return await _categoryRepository.GetAllCategoryAsync();
        }
        public async Task<Category> FindByIdAsync(int id)
        {
            return await _categoryRepository.FindByIdAsync(id);
        }
        public async Task CreateCategoryAsync(CategoryDto categoryDto)
        {
            await _categoryRepository.CreateCategoryAsync(categoryDto);
        }
        public async Task UpdateCategoryAsync(CategoryDto categoryDto)
        {
            await _categoryRepository.UpdateCategoryAsync(categoryDto);
        }
        public async Task DeleteCategoryAsync(int id)
        {
            await _categoryRepository.DeleteCategoryAsync(id);
        }
    }
}
