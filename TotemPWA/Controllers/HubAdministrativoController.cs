using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TotemPWA.Data;
using TotemPWA.Models;

namespace TotemPWA.Controllers;

public class HubAdministrativoController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HubAdministrativoController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _context = context;
        _logger = logger;
    }


  public IActionResult Index()
{
    if (HttpContext.Session.GetInt32("EmployeeId") == null)
    {
        return RedirectToAction("Login", "EmployeeLogin");
    }
    return View();
}
}