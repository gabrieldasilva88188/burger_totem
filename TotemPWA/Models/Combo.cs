using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TotemPWA.Models
{
    public class Combo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [Display(Name = "Nome")]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Descrição")]
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "O preço é obrigatório")]
        [Display(Name = "Preço")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
        public decimal Price { get; set; }

        [Display(Name = "Imagem")]
        public byte[]? Image { get; set; }

        [Display(Name = "Ativo")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Categoria")]
        public int? CategoryId { get; set; }

        [JsonIgnore]
        public Category? Category { get; set; }

        // Relacionamento com produtos através da tabela de junção
        public ICollection<ComboProduct> ComboProducts { get; set; } = new List<ComboProduct>();

        // Relacionamento com promoções
        public ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();
    }
} 