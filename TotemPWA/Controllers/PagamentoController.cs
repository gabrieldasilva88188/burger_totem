using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TotemPWA.Data;
using TotemPWA.Models;

namespace TotemPWA.Controllers;

public class PagamentoController : Controller
{
    public IActionResult Pix()
    {
        return View();
    }
    public IActionResult cartao()
    {
        return View();
    }
    public IActionResult Googlepay()
    {
        return View();
    }
    public IActionResult Tpagamento()
    {
        return View();
    }
    public IActionResult Applepay()
    {
        return View();
    }
    public IActionResult Sucesso()
    {
        return View();
    }
    public IActionResult Dinheiro()
    {
        return View("dinheiro");
    }
}
