using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
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
        public async Task CreateCategoryAsync(CategoryDto categoryDto)
        {
            try
            {
                var category = new Category
                {
                    Name = categoryDto.Name,
                    Description = categoryDto.Description
                };
                await _categoryRepository.AddAsync(category);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        public async Task UpdateCategoryAsync(int id, CategoryDto categoryDto)
        {
            try
            {
                var category = await _categoryRepository.GetQueryable().FirstOrDefaultAsync(x => x.Id == id);
                if (category is null)
                {
                    throw new Exception("Category not found!");
                }
                await _categoryRepository.UpdateAsync(category);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        public async Task DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await _categoryRepository.GetQueryable().FirstOrDefaultAsync(x => x.Id == id);
                if (category is null)
                {
                    throw new Exception("Category not found!");
                }
                await _categoryRepository.DeleteAsync(category);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
