using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Client
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
