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

    [HttpGet]
    public async Task<IActionResult> VerificarCupom(string codigo)
    {
        if (string.IsNullOrWhiteSpace(codigo))
            return Json(new { valido = false, mensagem = "Cupom inválido." });

        var codigoFormatado = codigo.Trim().ToUpper();
        var cupom = await _context.Cupons
            .FirstOrDefaultAsync(c => c.Code == codigoFormatado);


        if (cupom == null)
            return Json(new { valido = false, mensagem = "Cupom não encontrado." });

        if (!cupom.Active )
            return Json(new { valido = false, mensagem = "Cupom inativo." });

        if (cupom.ValidUntil.HasValue && cupom.ValidUntil < DateTime.Now)
            return Json(new { valido = false, mensagem = "Cupom expirado." });

        return Json(new
        {
            valido = true,
            desconto = cupom.Discount.ToString(System.Globalization.CultureInfo.InvariantCulture),
            mensagem = $"Cupom aplicado! {cupom.Discount:P0} de desconto."
        });
    }

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
        var now = DateTime.Now;
        var products = await _context.Products
            .Where(p => p.CategoryId == activeSubcategoryId)
            .Include(p => p.Promotions)
            .Select(p => new
            {
                id = p.Id,
                name = p.Name,
                description = p.Description,
                price = p.Price,
                slug = p.Slug,
                precoPromocional = p.Promotions
                    .Where(pr => pr.ValidUntil >= now)
                    .OrderByDescending(pr => pr.ValidUntil)
                    .Select(pr => p.Price * (decimal)(1 - pr.Percent / 100.0))
                    .FirstOrDefault()
            })
            .ToListAsync();

        ViewBag.Category = activeCategory?.Slug;
        ViewBag.Categories = rootCategories;
        ViewBag.SubCategories = subcategories;
        ViewBag.Products = products;

        return View();
    }

    public async Task<IActionResult> Personalizar(string slug, bool? editando = false)
    {
        var produto = await _context.Products
            .Include(p => p.ProductIngredients)
                .ThenInclude(pi => pi.Ingredient)
            .Include(p => p.Promotions)
            .FirstOrDefaultAsync(p => p.Slug == slug);

        if (produto == null) return NotFound();

        var now = DateTime.Now;
        var promo = produto.Promotions
            .Where(p => p.ValidUntil >= now)
            .OrderByDescending(p => p.ValidUntil)
            .FirstOrDefault();
        decimal precoFinal = produto.Price;
        if (promo != null)
        {
            precoFinal = produto.Price * (decimal)(1 - promo.Percent / 100.0);
        }

        ViewBag.Nome = produto.Name;
        ViewBag.Preco = precoFinal.ToString("0.00");
        ViewBag.ProdutoId = produto.Id;
        ViewBag.ProdutoSlug = produto.Slug;
        ViewBag.Editando = editando;
        if (produto.Image != null)
        {
            string imgSrc = Url.Action("GetImage", "HubAdministrativo", new { id = produto.Id });
            ViewBag.Imagem = imgSrc;
        }
        else
        {
            ViewBag.Imagem = Url.Content("~/img/default.png");
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

    // Método legado para compatibilidade (aceita ID)
    public async Task<IActionResult> PersonalizarPorId(int id, bool? editando = false)
    {
        var produto = await _context.Products
            .Include(p => p.ProductIngredients)
                .ThenInclude(pi => pi.Ingredient)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (produto == null) return NotFound();

        // Redireciona para a URL com slug
        return RedirectToAction("Personalizar", new { slug = produto.Slug, editando });
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
