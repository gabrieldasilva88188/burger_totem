using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TotemPWA.Models;

namespace TotemPWA.Controllers;

public class ExampleController : Controller
{
    private readonly ILogger<ExampleController> _logger;

    public ExampleController(ILogger<ExampleController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Receive(string name)
    {
        ViewBag.nome = name;
        return View();
        //return RedirectionToAction("NovaAction", new{ name });
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
