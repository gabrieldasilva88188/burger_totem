using System;

namespace TotemPWA.Models
{
    public class Cupom
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public decimal Discount { get; set; } // Ex: 0.10 para 10%
        public DateTime? ValidUntil { get; set; }
        public bool Active  { get; set; }
    }
}
