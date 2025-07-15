using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TotemPWA.Models
{
    public class Promotion
    {
        [Key]
        public int Id { get; set; }

        // Agora ProductId e ComboId são opcionais, permitindo promoção para produto OU combo
        public int? ProductId { get; set; }
        public int? ComboId { get; set; }

        [Required]
        [Range(0, 100)]
        public double Percent { get; set; }

        [Required]
        public DateTime ValidUntil { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

        [ForeignKey("ComboId")]
        public Combo? Combo { get; set; }
    }
} 