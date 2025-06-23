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
}
