using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TotemPWA.Models;

namespace TotemPWA.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About(){
        return View();
    }

     public IActionResult cardapio()
    {
        return View();
    }
    
    public IActionResult Personalizar(int id)
    {
    // Exemplo hardcoded, você pode buscar do banco
  // Exemplo hardcoded, você pode buscar do banco
    var produtos = new List<dynamic>
    {
        // Lanches de Frango
        new { Id = 1, Nome = "Chicken Code", Preco = "27,00", Imagem = "~/img/frango1.png" },
        new { Id = 2, Nome = "React Crispy", Preco = "28,50", Imagem = "~/img/frango2.png" },
        new { Id = 3, Nome = "Frango JS", Preco = "25,00", Imagem = "~/img/frango3.png" },

        // Lanches de Carne
        new { Id = 4, Nome = "Buguer 404", Preco = "29,00", Imagem = "~/img/carne1.png" },
        new { Id = 5, Nome = "Full Stack Burger", Preco = "32,00", Imagem = "~/img/full1.png" },
        new { Id = 6, Nome = "Looping Duplo", Preco = "30,00", Imagem = "~/img/carne2.png" },

        // Lanches Veganos
        new { Id = 7, Nome = "Green Stack", Preco = "26,00", Imagem = "~/img/vegano1.png" },
        new { Id = 8, Nome = "Vegan Full", Preco = "27,00", Imagem = "~/img/vegano2.png" },
        new { Id = 9, Nome = "Buguer Plant", Preco = "28,00", Imagem = "~/img/vegano3.png" },

        // Lanches de Peixe
        new { Id = 10, Nome = "Fish Dev", Preco = "29,00", Imagem = "~/img/peixe1.png" },
        new { Id = 11, Nome = "Salmão Byte", Preco = "34,00", Imagem = "~/img/peixe2.png" },
        new { Id = 12, Nome = "Tuna Stack", Preco = "31,00", Imagem = "~/img/peixe3.png" }
    };




    var produto = produtos.FirstOrDefault(p => p.Id == id);
    if (produto == null) return NotFound();

    ViewBag.Nome = produto.Nome;
    ViewBag.Preco = produto.Preco;
    ViewBag.Imagem = produto.Imagem;

    return View();
    }

       public IActionResult carrinho()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
