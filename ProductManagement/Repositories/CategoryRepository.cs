using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Dtos;
using ProductManagement.Models;
using ProductManagement.Repositories.Interface;

namespace ProductManagement.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetAllCategoryAsync()
        {
            return await _context.Categories.ToListAsync();
        }
        public async Task<Category> FindByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task CreateCategoryAsync(CategoryDto categoryDto)
        {
            var category = new Category
            {
                Id = categoryDto.Id,
                Name = categoryDto.Name,
                Description = categoryDto.Description
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                throw new Exception("Category not found!");
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(CategoryDto categoryDto)
        {
            var category = await _context.Products.FindAsync(categoryDto.Id);
            if (category == null)
            {
                throw new Exception("Category not found!");
            }

            category.Name = categoryDto.Name;
            category.Description = categoryDto.Description;

            await _context.SaveChangesAsync();
        }
    
    }
}
