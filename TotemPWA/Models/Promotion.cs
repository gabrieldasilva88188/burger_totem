using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TotemPWA.Models
{
    public class Promotion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(0, 100)]
        public double Percent { get; set; }

        [Required]
        public DateTime ValidUntil { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
} 