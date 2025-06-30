using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TotemPWA.Models
{
    public class Employee : Client
    {
        [Required(ErrorMessage = "O tipo é obrigatório")]
        [Display(Name = "Tipo")]
        public string Type { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O usuário é obrigatório")]
        [Display(Name = "Usuário")]
        public string User { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "A senha é obrigatória")]
        [Display(Name = "Senha")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres")]
        public string Password { get; set; } = string.Empty;
    }
}
