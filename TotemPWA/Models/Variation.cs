using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TotemPWA.Models
{
    public class Variation
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public required string Description { get; set; }

        public decimal AdditionalPrice { get; set; }

        public int ProductId { get; set; }

        [JsonIgnore]
        public Product Product { get; set; }
    }
}
