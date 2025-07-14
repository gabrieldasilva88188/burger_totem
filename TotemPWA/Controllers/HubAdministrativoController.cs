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
        var now = DateTime.Now;
        var produtos = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Promotions)
            .ToListAsync();

        foreach (var produto in produtos)
        {
            var promo = produto.Promotions
                .Where(p => p.ValidUntil >= now)
                .OrderByDescending(p => p.ValidUntil)
                .FirstOrDefault();
            if (promo != null)
            {
                produto.PrecoPromocional = produto.Price * (decimal)(1 - promo.Percent / 100.0);
            }
        }
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
        var produto = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Promotions)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (produto == null) return NotFound();
        var now = DateTime.Now;
        var promo = produto.Promotions
            .Where(p => p.ValidUntil >= now)
            .OrderByDescending(p => p.ValidUntil)
            .FirstOrDefault();
        if (promo != null)
        {
            produto.PrecoPromocional = produto.Price * (decimal)(1 - promo.Percent / 100.0);
        }
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

    // CRUD de Ingredientes
    [HttpGet]
    public async Task<IActionResult> Ingredients()
    {
        var ingredientes = await _context.Ingredients.ToListAsync();
        return View("Ingredients/Index", ingredientes);
    }

    [HttpGet]
    public IActionResult CreateIngredient()
    {
        return View("Ingredients/Create");
    }

    [HttpPost]
    public async Task<IActionResult> CreateIngredient(Ingredient ingredient)
    {
        if (ModelState.IsValid)
        {
            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();
            return RedirectToAction("Ingredients");
        }
        return View("Ingredients/Create", ingredient);
    }

    [HttpGet]
    public async Task<IActionResult> EditIngredient(int id)
    {
        var ingredient = await _context.Ingredients.FindAsync(id);
        if (ingredient == null) return NotFound();
        return View("Ingredients/Edit", ingredient);
    }

    [HttpPost]
    public async Task<IActionResult> EditIngredient(Ingredient ingredient)
    {
        if (ModelState.IsValid)
        {
            _context.Ingredients.Update(ingredient);
            await _context.SaveChangesAsync();
            return RedirectToAction("Ingredients");
        }
        return View("Ingredients/Edit", ingredient);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteIngredient(int id)
    {
        var ingredient = await _context.Ingredients.FindAsync(id);
        if (ingredient == null) return NotFound();
        return View("Ingredients/Delete", ingredient);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteIngredient(Ingredient ingredient)
    {
        var ingredientDb = await _context.Ingredients.FindAsync(ingredient.Id);
        if (ingredientDb == null) return NotFound();
        _context.Ingredients.Remove(ingredientDb);
        await _context.SaveChangesAsync();
        return RedirectToAction("Ingredients");
    }

    [HttpGet]
    public async Task<IActionResult> ManageIngredients(int id)
    {
        var produto = await _context.Products
            .Include(p => p.ProductIngredients)
                .ThenInclude(pi => pi.Ingredient)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (produto == null) return NotFound();

        var todosIngredientes = await _context.Ingredients.ToListAsync();
        var ingredientesAssociados = produto.ProductIngredients.Select(pi => new
        {
            pi.IngredientId,
            pi.Ingredient.Name,
            pi.Quantity,
            pi.Ingredient.Limit
        }).ToList();
        var ingredientesDisponiveis = todosIngredientes.Where(i => !produto.ProductIngredients.Any(pi => pi.IngredientId == i.Id)).ToList();

        ViewBag.Produto = produto;
        ViewBag.IngredientesAssociados = ingredientesAssociados;
        ViewBag.IngredientesDisponiveis = ingredientesDisponiveis;
        return View("Products/ManageIngredients");
    }

    [HttpPost]
    public async Task<IActionResult> AddIngredientToProduct(int productId, int ingredientId, int quantity)
    {
        var exists = await _context.ProductIngredients.AnyAsync(pi => pi.ProductId == productId && pi.IngredientId == ingredientId);
        if (!exists)
        {
            var pi = new ProductIngredient { ProductId = productId, IngredientId = ingredientId, Quantity = quantity };
            _context.ProductIngredients.Add(pi);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("ManageIngredients", new { id = productId });
    }

    [HttpPost]
    public async Task<IActionResult> RemoveIngredientFromProduct(int productId, int ingredientId)
    {
        var pi = await _context.ProductIngredients.FirstOrDefaultAsync(x => x.ProductId == productId && x.IngredientId == ingredientId);
        if (pi != null)
        {
            _context.ProductIngredients.Remove(pi);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("ManageIngredients", new { id = productId });
    }

    // CRUD de Categorias
    [HttpGet]
    public async Task<IActionResult> Categories()
    {
        var categorias = await _context.Categories.Include(c => c.ParentCategory).ToListAsync();
        return View("Categories/Index", categorias);
    }

    [HttpGet]
    public async Task<IActionResult> CreateCategory()
    {
        ViewBag.Categorias = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
        return View("Categories/Create");
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(Category category)
    {
        if (ModelState.IsValid)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Categories");
        }
        ViewBag.Categorias = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", category.ParentCategoryId);
        return View("Categories/Create", category);
    }

    [HttpGet]
    public async Task<IActionResult> EditCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return NotFound();
        ViewBag.Categorias = new SelectList(await _context.Categories.Where(c => c.Id != id).ToListAsync(), "Id", "Name", category.ParentCategoryId);
        return View("Categories/Edit", category);
    }

    [HttpPost]
    public async Task<IActionResult> EditCategory(Category category)
    {
        if (ModelState.IsValid)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Categories");
        }
        ViewBag.Categorias = new SelectList(await _context.Categories.Where(c => c.Id != category.Id).ToListAsync(), "Id", "Name", category.ParentCategoryId);
        return View("Categories/Edit", category);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _context.Categories.Include(c => c.ParentCategory).FirstOrDefaultAsync(c => c.Id == id);
        if (category == null) return NotFound();
        return View("Categories/Delete", category);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCategory(Category category)
    {
        var categoryDb = await _context.Categories.FindAsync(category.Id);
        if (categoryDb == null) return NotFound();
        _context.Categories.Remove(categoryDb);
        await _context.SaveChangesAsync();
        return RedirectToAction("Categories");
    }

    // CRUD de Funcionários
    [HttpGet]
    public async Task<IActionResult> Employees()
    {
        var funcionarios = await _context.Employees.ToListAsync();
        return View("Employees/Index", funcionarios);
    }

    [HttpGet]
    public IActionResult CreateEmployee()
    {
        return View("Employees/Create");
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee(Employee employee)
    {
        if (ModelState.IsValid)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction("Employees");
        }
        return View("Employees/Create", employee);
    }

    [HttpGet]
    public async Task<IActionResult> EditEmployee(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return NotFound();
        return View("Employees/Edit", employee);
    }

    [HttpPost]
    public async Task<IActionResult> EditEmployee(Employee employee)
    {
        if (ModelState.IsValid)
        {
            var employeeDb = await _context.Employees.FindAsync(employee.Id);
            if (employeeDb == null) return NotFound();

            // Atualiza os campos básicos
            employeeDb.Name = employee.Name;
            employeeDb.Email = employee.Email;
            employeeDb.User = employee.User;
            employeeDb.Type = employee.Type;

            // Só atualiza a senha se foi fornecida
            if (!string.IsNullOrEmpty(employee.Password))
            {
                employeeDb.Password = employee.Password;
            }

            _context.Employees.Update(employeeDb);
            await _context.SaveChangesAsync();
            return RedirectToAction("Employees");
        }
        return View("Employees/Edit", employee);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return NotFound();
        return View("Employees/Delete", employee);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteEmployee(Employee employee)
    {
        var employeeDb = await _context.Employees.FindAsync(employee.Id);
        if (employeeDb == null) return NotFound();
        _context.Employees.Remove(employeeDb);
        await _context.SaveChangesAsync();
        return RedirectToAction("Employees");
    }

    [HttpGet]
    public async Task<IActionResult> Promotion(int id)
    {
        var produto = await _context.Products
            .Include(p => p.Promotions)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (produto == null) return NotFound();
        var now = DateTime.Now;
        var promo = produto.Promotions
            .Where(p => p.ValidUntil >= now)
            .OrderByDescending(p => p.ValidUntil)
            .FirstOrDefault();
        ViewBag.Promotion = promo;
        return View("Products/Promotion", produto);
    }

    [HttpPost]
    public async Task<IActionResult> Promotion(int id, double percent, DateTime validUntil)
    {
        var produto = await _context.Products.Include(p => p.Promotions).FirstOrDefaultAsync(p => p.Id == id);
        if (produto == null) return NotFound();
        // Remove promoções antigas
        var oldPromos = produto.Promotions.Where(p => p.ValidUntil >= DateTime.Now).ToList();
        _context.Promotions.RemoveRange(oldPromos);
        // Adiciona nova promoção
        var promo = new Promotion
        {
            ProductId = id,
            Percent = percent,
            ValidUntil = validUntil
        };
        _context.Promotions.Add(promo);
        await _context.SaveChangesAsync();
        return RedirectToAction("Products");
    }

    [HttpPost]
    public async Task<IActionResult> RemovePromotion(int id)
    {
        var promos = await _context.Promotions.Where(p => p.ProductId == id && p.ValidUntil >= DateTime.Now).ToListAsync();
        _context.Promotions.RemoveRange(promos);
        await _context.SaveChangesAsync();
        return RedirectToAction("Products");
    }
}