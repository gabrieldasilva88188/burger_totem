using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TotemPWA.Data;
using TotemPWA.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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

// CRUD de Produtos
[HttpGet]
public async Task<IActionResult> Products()
{
    var produtos = await _context.Products.Include(p => p.Category).ToListAsync();
    return View("Products/Index", produtos);
}

[HttpGet]
public async Task<IActionResult> Create()
{
    ViewBag.Categorias = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
    return View("Products/Create");
}

[HttpPost]
public async Task<IActionResult> Create(Product product, IFormFile? ImageFile)
{
    if (ImageFile != null && ImageFile.Length > 0)
    {
        using var ms = new MemoryStream();
        await ImageFile.CopyToAsync(ms);
        product.Image = ms.ToArray();
    }
    _context.Products.Add(product);
    await _context.SaveChangesAsync();
    return RedirectToAction("Products");
}

[HttpGet]
public async Task<IActionResult> Edit(int id)
{
    var produto = await _context.Products.FindAsync(id);
    if (produto == null) return NotFound();
    ViewBag.Categorias = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", produto.CategoryId);
    return View("Products/Edit", produto);
}

[HttpPost]
public async Task<IActionResult> Edit(Product product, IFormFile? ImageFile)
{
    var produtoDb = await _context.Products.FindAsync(product.Id);
    if (produtoDb == null) return NotFound();
    produtoDb.Name = product.Name;
    produtoDb.Description = product.Description;
    produtoDb.Price = product.Price;
    produtoDb.CategoryId = product.CategoryId;
    if (ImageFile != null && ImageFile.Length > 0)
    {
        using var ms = new MemoryStream();
        await ImageFile.CopyToAsync(ms);
        produtoDb.Image = ms.ToArray();
    }
    await _context.SaveChangesAsync();
    return RedirectToAction("Products");
}

[HttpGet]
public async Task<IActionResult> Delete(int id)
{
    var produto = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
    if (produto == null) return NotFound();
    return View("Products/Delete", produto);
}

[HttpPost]
public async Task<IActionResult> Delete(Product product)
{
    var produtoDb = await _context.Products.FindAsync(product.Id);
    if (produtoDb == null) return NotFound();
    _context.Products.Remove(produtoDb);
    await _context.SaveChangesAsync();
    return RedirectToAction("Products");
}

[HttpGet]
public async Task<IActionResult> Details(int id)
{
    var produto = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
    if (produto == null) return NotFound();
    return View("Products/Details", produto);
}

[HttpGet]
public async Task<IActionResult> GetImage(int id)
{
    var produto = await _context.Products.FindAsync(id);
    if (produto?.Image == null) return NotFound();

    // Detectar tipo MIME
    string mimeType = "image/jpeg";
    var img = produto.Image;
    if (img.Length > 3 && img[0] == 0x89 && img[1] == 0x50 && img[2] == 0x4E && img[3] == 0x47) // PNG
        mimeType = "image/png";
    else if (img.Length > 2 && img[0] == 0x47 && img[1] == 0x49 && img[2] == 0x46) // GIF
        mimeType = "image/gif";
    else if (img.Length > 11 && img[8] == 0x57 && img[9] == 0x45 && img[10] == 0x42 && img[11] == 0x50) // WEBP
        mimeType = "image/webp";
    // JPEG já é o padrão

    return File(produto.Image, mimeType);
}
}