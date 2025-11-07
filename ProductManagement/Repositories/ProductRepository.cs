using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Dtos;
using ProductManagement.Models;
using ProductManagement.Repositories.Interface;

namespace ProductManagement.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }
        public async Task<Product?> FindByIdAsync( int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task CreateAsync(ProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description,
                CategoryId = productDto.CategoryId
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product == null)
            {
                throw new Exception("Product not found!");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductDto productDto)
        {
            var product = await _context.Products.FindAsync(productDto.Id);
            if (product == null)
            {
                throw new Exception("Product not found!");
            }

            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.Description = productDto.Description;
            product.CategoryId = productDto.CategoryId;

            await _context.SaveChangesAsync();
        }

    }
}
