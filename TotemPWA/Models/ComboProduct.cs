using System.ComponentModel.DataAnnotations;

namespace TotemPWA.Models
{
    public class ComboProduct
    {
        public int Id { get; set; }

        [Required]
        public int ComboId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser pelo menos 1")]
        public int Quantity { get; set; } = 1;

        // Propriedades de navegação
        public Combo Combo { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
} 