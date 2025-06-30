using System.ComponentModel.DataAnnotations.Schema;

namespace TotemPWA.Models
{
    public class ProductIngredient
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        public int Quantity { get; set; }
    }
} 