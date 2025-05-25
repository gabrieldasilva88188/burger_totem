using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TotemPWA.Models
{
    public class Category
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public required string Name { get; set; }

        public int? ParentCategoryId { get; set; }

        [JsonIgnore]
        public Category? ParentCategory { get; set; }

        public ICollection<Category> Subcategories { get; set; } = new List<Category>();

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
