using System.ComponentModel.DataAnnotations;

namespace TotemPWA.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public required string Name { get; set; }

        public decimal Price { get; set; }

        public int Limit { get; set; }

        public byte[]? Image { get; set; }

        public ICollection<ProductIngredient> ProductIngredients { get; set; } = new List<ProductIngredient>();
    }
} 