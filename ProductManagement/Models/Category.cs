using System.Text.Json.Serialization;

namespace ProductManagement.Models
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; }

    }
}
