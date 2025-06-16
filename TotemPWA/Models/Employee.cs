using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


public class Employee : Client
{
    public string Type { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
