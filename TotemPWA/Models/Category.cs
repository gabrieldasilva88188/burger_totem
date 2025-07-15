using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace TotemPWA.Models
{
    public class Category
    {
        public int Id { get; set; }

        private string _name = string.Empty;

        [MaxLength(255)]
        public required string Name
        {
            get => _name;
            set
            {
                _name = value;
                Slug = GenerateSlug(value);
            }
        }

        [MaxLength(255)]
        public string Slug { get; set; } = string.Empty;

        public int? ParentCategoryId { get; set; }

        [JsonIgnore]
        public Category? ParentCategory { get; set; }

        public ICollection<Category> Subcategories { get; set; } = new List<Category>();

        public ICollection<Product> Products { get; set; } = new List<Product>();

        public ICollection<Combo> Combos { get; set; } = new List<Combo>();

        // Imagem da categoria
        public byte[]? Image { get; set; }

        private string GenerateSlug(string text)
        {
            text = text.ToLowerInvariant().Trim();
            text = Regex.Replace(text, @"[^a-z0-9\s-]", "");
            text = Regex.Replace(text, @"\s+", "-");
            text = Regex.Replace(text, @"-+", "-");
            return text;
        }
    }
}
