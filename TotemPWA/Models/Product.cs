using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations.Schema;

namespace TotemPWA.Models
{
    public class Product
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

        [MaxLength(1000)]
        public required string Description { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        [JsonIgnore]
        public Category? Category { get; set; }

        public ICollection<Variation> Variations { get; set; } = new List<Variation>();

        // NOVO CAMPO: Imagem armazenada como array de bytes (BLOB no SQLite)
        public byte[]? Image { get; set; }

        [MaxLength(255)]
        public string Slug { get; set; } = string.Empty;

        public ICollection<ProductIngredient> ProductIngredients { get; set; } = new List<ProductIngredient>();

        // Relacionamento com combos
        public ICollection<ComboProduct> ComboProducts { get; set; } = new List<ComboProduct>();

        // Relacionamento com promoções de produto
        public ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

        [NotMapped]
        public decimal? PrecoPromocional { get; set; }

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
