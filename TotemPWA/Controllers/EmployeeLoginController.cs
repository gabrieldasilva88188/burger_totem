using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TotemPWA.Data;
using TotemPWA.Models;
using System.Security.Cryptography;
using System.Text;

namespace TotemPWA.Controllers;

public class EmployeeLoginController : Controller
{
    private readonly ApplicationDbContext _context;

    public EmployeeLoginController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

  [HttpPost("Login")]
public async Task<IActionResult> Login(string user, string password)
{
    // Debug
    Console.WriteLine($"Tentativa de login - User: '{user}', Password: '{password}'");
    
    var employee = await _context.Employees
        .FirstOrDefaultAsync(e => e.User == user);
    
    if (employee != null)
    {
        Console.WriteLine($"Usuário encontrado. Senha no BD: '{employee.Password}'");
        
        // Comparação direta sem hash
        if (password == employee.Password)
        {
            HttpContext.Session.SetInt32("EmployeeId", employee.Id);
            HttpContext.Session.SetString("EmployeeName", employee.User);
            HttpContext.Session.SetString("EmployeeRole", employee.Type);
            return RedirectToAction("Index", "HubAdministrativo");
        }
    }
    
    ViewBag.Error = "Usuário ou senha inválidos.";
    return View();
}

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "EmployeeLogin");
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    private bool VerifyPassword(string inputPassword, string storedHash)
    {
        var inputHash = HashPassword(inputPassword);
        return inputHash == storedHash;
    }
}