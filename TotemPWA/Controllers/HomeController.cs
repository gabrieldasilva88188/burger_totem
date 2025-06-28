using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TotemPWA.Data;
using TotemPWA.Models;

namespace TotemPWA.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    [HttpGet("Cardapio/{categorySlug?}/{subcategorySlug?}")]
    public async Task<IActionResult> Cardapio(string categorySlug, string subcategorySlug)
    {
        // Categorias principais (sem pai)
        var rootCategoriesRaw = await _context.Categories
            .Where(c => c.ParentCategoryId == null)
            .ToListAsync();

        var activeCategory = rootCategoriesRaw
            .FirstOrDefault(c => c.Slug == categorySlug) ?? rootCategoriesRaw.FirstOrDefault();

        var activeCategoryId = activeCategory?.Id;

        var rootCategories = rootCategoriesRaw
            .Select(c => new
            {
                id = c.Id,
                name = c.Name,
                slug = c.Slug,
                active = c.Id == activeCategoryId
            })
            .ToList();

        // Subcategorias da categoria ativa
        var subcategoriesRaw = await _context.Categories
            .Where(c => c.ParentCategoryId == activeCategoryId)
            .ToListAsync();

        var activeSubcategory = subcategoriesRaw
            .FirstOrDefault(c => c.Slug == subcategorySlug) ?? subcategoriesRaw.FirstOrDefault();

        var activeSubcategoryId = activeSubcategory?.Id;

        var subcategories = subcategoriesRaw
            .Select(c => new
            {
                id = c.Id,
                name = c.Name,
                slug = c.Slug,
                active = c.Id == activeSubcategoryId
            })
            .ToList();

        // Produtos da subcategoria ativa
        var products = await _context.Products
            .Where(p => p.CategoryId == activeSubcategoryId)
            .Select(p => new
            {
                id = p.Id,
                name = p.Name,
                description = p.Description,
                price = p.Price
            })
            .ToListAsync();

        ViewBag.Category = activeCategory?.Slug;
        ViewBag.Categories = rootCategories;
        ViewBag.SubCategories = subcategories;
        ViewBag.Products = products;

        return View();
    }

    public async Task<IActionResult> Personalizar(int id)
    {
        var produto = await _context.Products
            .Include(p => p.ProductIngredients)
                .ThenInclude(pi => pi.Ingredient)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (produto == null) return NotFound();

        ViewBag.Nome = produto.Name;
        ViewBag.Preco = produto.Price.ToString("0.00");
        if (produto.Image != null)
        {
            string imgSrc = $"/Home/GetImage/{produto.Id}";
            ViewBag.Imagem = imgSrc;
        }
        else
        {
            ViewBag.Imagem = "~/img/default.png";
        }

        ViewBag.Ingredientes = produto.ProductIngredients.Select(pi => new {
            pi.Ingredient.Id,
            pi.Ingredient.Name,
            pi.Ingredient.Price,
            pi.Ingredient.Limit,
            pi.Quantity
        }).ToList();

        return View();
    }

    public IActionResult Carrinho()
    {
        return View();
    }
    public IActionResult Carregamento()
    {
        return View();
    }
    public IActionResult ClienteNome()
    {
        return View();
    }
    public IActionResult ClienteCPF()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public IActionResult Escolha()
    {
    return View("escolha");
    }

}
