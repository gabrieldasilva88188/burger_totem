using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TotemPWA.Models
{
    public class Product
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public required string Name { get; set; }

        [MaxLength(1000)]
        public required string Description { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        [JsonIgnore]
        public Category? Category { get; set; }

        public ICollection<Variation> Variations { get; set; } = new List<Variation>();
    }
}
