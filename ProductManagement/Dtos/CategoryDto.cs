using ProductManagement.Models;

namespace ProductManagement.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; }
        public List<Product> Products { get; set; }
    }
}
