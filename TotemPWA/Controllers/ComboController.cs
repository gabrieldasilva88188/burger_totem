using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TotemPWA.Data;
using TotemPWA.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TotemPWA.Controllers
{
    public class ComboController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComboController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Combo
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("EmployeeId") == null)
            {
                return RedirectToAction("Login", "EmployeeLogin");
            }

            var now = DateTime.Now;
            var combos = await _context.Combos
                .Include(c => c.ComboProducts)
                    .ThenInclude(cp => cp.Product)
                .Include(c => c.Promotions)
                .ToListAsync();

            foreach (var combo in combos)
            {
                var promo = combo.Promotions
                    .Where(p => p.ValidUntil >= now)
                    .OrderByDescending(p => p.ValidUntil)
                    .FirstOrDefault();
                if (promo != null)
                {
                    // Adicionar propriedade para preço promocional se necessário
                    ViewBag.PrecoPromocional = combo.Price * (decimal)(1 - promo.Percent / 100.0);
                }
            }

            return View(combos);
        }

        // GET: Combo/Create
        public async Task<IActionResult> Create()
        {
            if (HttpContext.Session.GetInt32("EmployeeId") == null)
            {
                return RedirectToAction("Login", "EmployeeLogin");
            }

            // Carregar todas as subcategorias de combos
            var categorias = await _context.Categories
                .Where(c => c.ParentCategoryId == 2) // ID 2 é a categoria "Combos"
                .OrderBy(c => c.Name)
                .ToListAsync();

            // Carregar todos os produtos disponíveis para seleção
            var produtos = await _context.Products
                .Include(p => p.Category)
                .OrderBy(p => p.Category != null ? p.Category.Name : "")
                .ThenBy(p => p.Name)
                .ToListAsync();

            ViewBag.Categorias = categorias;
            ViewBag.Produtos = produtos;
            return View();
        }

        // POST: Combo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Combo combo, List<int> produtoIds, List<int> quantidades, IFormFile? ImageFile)
        {
            if (HttpContext.Session.GetInt32("EmployeeId") == null)
            {
                return RedirectToAction("Login", "EmployeeLogin");
            }

            if (ModelState.IsValid)
            {
                // Processar imagem se fornecida
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    using var ms = new MemoryStream();
                    await ImageFile.CopyToAsync(ms);
                    combo.Image = ms.ToArray();
                }

                // Salvar o combo
                _context.Combos.Add(combo);
                await _context.SaveChangesAsync();

                // Associar produtos ao combo
                if (produtoIds != null && quantidades != null)
                {
                    for (int i = 0; i < produtoIds.Count; i++)
                    {
                        if (produtoIds[i] > 0 && quantidades[i] > 0)
                        {
                            var comboProduct = new ComboProduct
                            {
                                ComboId = combo.Id,
                                ProductId = produtoIds[i],
                                Quantity = quantidades[i]
                            };
                            _context.ComboProducts.Add(comboProduct);
                        }
                    }
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            // Se chegou aqui, houve erro de validação
            var categorias = await _context.Categories
                .Where(c => c.ParentCategoryId == 2) // ID 2 é a categoria "Combos"
                .OrderBy(c => c.Name)
                .ToListAsync();

            var produtos = await _context.Products
                .Include(p => p.Category)
                .OrderBy(p => p.Category != null ? p.Category.Name : "")
                .ThenBy(p => p.Name)
                .ToListAsync();

            ViewBag.Categorias = categorias;
            ViewBag.Produtos = produtos;
            return View(combo);
        }

        // GET: Combo/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (HttpContext.Session.GetInt32("EmployeeId") == null)
            {
                return RedirectToAction("Login", "EmployeeLogin");
            }

            var combo = await _context.Combos
                .Include(c => c.ComboProducts)
                    .ThenInclude(cp => cp.Product)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (combo == null)
            {
                return NotFound();
            }

            // Carregar todas as subcategorias de combos
            var categorias = await _context.Categories
                .Where(c => c.ParentCategoryId == 2) // ID 2 é a categoria "Combos"
                .OrderBy(c => c.Name)
                .ToListAsync();

            // Carregar todos os produtos disponíveis
            var produtos = await _context.Products
                .Include(p => p.Category)
                .OrderBy(p => p.Category != null ? p.Category.Name : "")
                .ThenBy(p => p.Name)
                .ToListAsync();

            ViewBag.Categorias = categorias;
            ViewBag.Produtos = produtos;
            ViewBag.ProdutosAssociados = combo.ComboProducts.ToList();
            return View(combo);
        }

        // POST: Combo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Combo combo, List<int> produtoIds, List<int> quantidades, IFormFile? ImageFile)
        {
            if (HttpContext.Session.GetInt32("EmployeeId") == null)
            {
                return RedirectToAction("Login", "EmployeeLogin");
            }

            if (id != combo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var comboDb = await _context.Combos
                        .Include(c => c.ComboProducts)
                        .FirstOrDefaultAsync(c => c.Id == id);

                    if (comboDb == null)
                    {
                        return NotFound();
                    }

                    // Atualizar dados básicos do combo
                    comboDb.Name = combo.Name;
                    comboDb.Description = combo.Description;
                    comboDb.Price = combo.Price;
                    comboDb.IsActive = combo.IsActive;

                    // Processar nova imagem se fornecida
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        using var ms = new MemoryStream();
                        await ImageFile.CopyToAsync(ms);
                        comboDb.Image = ms.ToArray();
                    }

                    // Remover associações existentes
                    _context.ComboProducts.RemoveRange(comboDb.ComboProducts);

                    // Adicionar novas associações
                    if (produtoIds != null && quantidades != null)
                    {
                        for (int i = 0; i < produtoIds.Count; i++)
                        {
                            if (produtoIds[i] > 0 && quantidades[i] > 0)
                            {
                                var comboProduct = new ComboProduct
                                {
                                    ComboId = combo.Id,
                                    ProductId = produtoIds[i],
                                    Quantity = quantidades[i]
                                };
                                _context.ComboProducts.Add(comboProduct);
                            }
                        }
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComboExists(combo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            // Se chegou aqui, houve erro de validação
            var categorias = await _context.Categories
                .Where(c => c.ParentCategoryId == 2) // ID 2 é a categoria "Combos"
                .OrderBy(c => c.Name)
                .ToListAsync();

            var produtos = await _context.Products
                .Include(p => p.Category)
                .OrderBy(p => p.Category != null ? p.Category.Name : "")
                .ThenBy(p => p.Name)
                .ToListAsync();

            ViewBag.Categorias = categorias;
            ViewBag.Produtos = produtos;
            return View(combo);
        }

        // GET: Combo/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (HttpContext.Session.GetInt32("EmployeeId") == null)
            {
                return RedirectToAction("Login", "EmployeeLogin");
            }

            var combo = await _context.Combos
                .Include(c => c.ComboProducts)
                    .ThenInclude(cp => cp.Product)
                .Include(c => c.Promotions)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (combo == null)
            {
                return NotFound();
            }

            // Calcular preço promocional se houver promoção ativa
            var now = DateTime.Now;
            var promo = combo.Promotions
                .Where(p => p.ValidUntil >= now)
                .OrderByDescending(p => p.ValidUntil)
                .FirstOrDefault();

            if (promo != null)
            {
                ViewBag.PrecoPromocional = combo.Price * (decimal)(1 - promo.Percent / 100.0);
                ViewBag.Promocao = promo;
            }

            return View(combo);
        }

        // GET: Combo/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (HttpContext.Session.GetInt32("EmployeeId") == null)
            {
                return RedirectToAction("Login", "EmployeeLogin");
            }

            var combo = await _context.Combos
                .Include(c => c.ComboProducts)
                    .ThenInclude(cp => cp.Product)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (combo == null)
            {
                return NotFound();
            }

            return View(combo);
        }

        // POST: Combo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetInt32("EmployeeId") == null)
            {
                return RedirectToAction("Login", "EmployeeLogin");
            }

            var combo = await _context.Combos
                .Include(c => c.ComboProducts)
                .Include(c => c.Promotions)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (combo != null)
            {
                // Remover associações de produtos
                _context.ComboProducts.RemoveRange(combo.ComboProducts);
                
                // Remover promoções
                _context.Promotions.RemoveRange(combo.Promotions);
                
                // Remover o combo
                _context.Combos.Remove(combo);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Combo/Promotion/5
        public async Task<IActionResult> Promotion(int id)
        {
            if (HttpContext.Session.GetInt32("EmployeeId") == null)
            {
                return RedirectToAction("Login", "EmployeeLogin");
            }

            var combo = await _context.Combos
                .Include(c => c.Promotions)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (combo == null)
            {
                return NotFound();
            }

            var now = DateTime.Now;
            var promo = combo.Promotions
                .Where(p => p.ValidUntil >= now)
                .OrderByDescending(p => p.ValidUntil)
                .FirstOrDefault();

            ViewBag.Promotion = promo;
            return View(combo);
        }

        // POST: Combo/Promotion/5
        [HttpPost]
        public async Task<IActionResult> Promotion(int id, double percent, DateTime validUntil)
        {
            if (HttpContext.Session.GetInt32("EmployeeId") == null)
            {
                return RedirectToAction("Login", "EmployeeLogin");
            }

            var combo = await _context.Combos
                .Include(c => c.Promotions)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (combo == null)
            {
                return NotFound();
            }

            // Remove promoções antigas
            var oldPromos = combo.Promotions.Where(p => p.ValidUntil >= DateTime.Now).ToList();
            _context.Promotions.RemoveRange(oldPromos);

            // Adiciona nova promoção
            var promo = new Promotion
            {
                ComboId = id,
                Percent = percent,
                ValidUntil = validUntil
            };

            _context.Promotions.Add(promo);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // POST: Combo/RemovePromotion/5
        [HttpPost]
        public async Task<IActionResult> RemovePromotion(int id)
        {
            if (HttpContext.Session.GetInt32("EmployeeId") == null)
            {
                return RedirectToAction("Login", "EmployeeLogin");
            }

            var promos = await _context.Promotions
                .Where(p => p.ComboId == id && p.ValidUntil >= DateTime.Now)
                .ToListAsync();

            _context.Promotions.RemoveRange(promos);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Combo/GetImage/5
        [HttpGet]
        public async Task<IActionResult> GetImage(int id)
        {
            var combo = await _context.Combos.FindAsync(id);
            if (combo?.Image == null)
            {
                return NotFound();
            }

            // Detectar tipo MIME
            string mimeType = "image/jpeg";
            var img = combo.Image;
            if (img.Length > 3 && img[0] == 0x89 && img[1] == 0x50 && img[2] == 0x4E && img[3] == 0x47) // PNG
                mimeType = "image/png";
            else if (img.Length > 2 && img[0] == 0x47 && img[1] == 0x49 && img[2] == 0x46) // GIF
                mimeType = "image/gif";
            else if (img.Length > 11 && img[8] == 0x57 && img[9] == 0x45 && img[10] == 0x42 && img[11] == 0x50) // WEBP
                mimeType = "image/webp";

            return File(combo.Image, mimeType);
        }

        private bool ComboExists(int id)
        {
            return _context.Combos.Any(e => e.Id == id);
        }
    }
} 