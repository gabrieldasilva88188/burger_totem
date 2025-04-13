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
    new { Id = 1, Nome = "Buguer 404", Preco = "29,00", Imagem = "~/img/lanches.png" },
    new { Id = 2, Nome = "Full Stack Burger", Preco = "32,00", Imagem = "~/img/full1.png" },
    new { Id = 3, Nome = "Looping Duplo", Preco = "30,00", Imagem = "~/img/duplo1.jpg" },
    new { Id = 4, Nome = "Back-End Crispy Triplo", Preco = "30,00", Imagem = "~/img/back1.jpg" }, // Três carnes
    new { Id = 5, Nome = "Back-End Crispy Onion", Preco = "30,00", Imagem = "~/img/back1.jpg" }, // Cebola crispy
    new { Id = 6, Nome = "Back-End Crispy Onion 2", Preco = "30,00", Imagem = "~/img/back1.jpg" } // Igual ao anterior, mas separado se quiser variar depois
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
