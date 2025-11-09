using ProductManagement.Models;

namespace ProductManagement.Dtos
{
    public class CategoryDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
