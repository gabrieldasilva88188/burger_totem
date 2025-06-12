
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Employee : Client
{
    public int clienteId { get; set; }
    public string type { get; set; }
    public string user { get; set; }
    public string password { get; set; }

    // Permissões futuras, ex: gerenciamento de produtos
}
