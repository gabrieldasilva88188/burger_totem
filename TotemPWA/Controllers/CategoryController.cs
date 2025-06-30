using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TotemPWA.Data;
using TotemPWA.Models;

namespace TotemPWA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll()
        {
            return await _context.Categories
                .Where(c => c.ParentCategoryId == null)
                .Include(c => c.Subcategories)
                    .ThenInclude(sc => sc.Products)
                        .ThenInclude(p => p.Variations)
                .Include(c => c.Products)
                    .ThenInclude(p => p.Variations)
                .ToListAsync();
        }

        // GET: api/Category/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Subcategories)
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) return NotFound();
            return category;
        }

        // GET: api/Category/slug/lanches
        [HttpGet("slug/{slug}")]
        public async Task<ActionResult<Category>> GetBySlug(string slug)
        {
            var category = await _context.Categories
                .Include(c => c.Subcategories)
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Slug == slug);

            if (category == null) return NotFound();
            return category;
        }

        // POST: api/Category
        [HttpPost]
        public async Task<ActionResult<Category>> Create(Category category)
        {
            category.Slug = GenerateSlug(category.Name);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBySlug), new { slug = category.Slug }, category);
        }

        // PUT: api/Category/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Category category)
        {
            if (id != category.Id) return BadRequest();

            _context.Entry(category).State = EntityState.Modified;
            category.Slug = GenerateSlug(category.Name); // atualiza slug se nome mudar
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // PUT: api/Category/slug/lanches
        [HttpPut("slug/{slug}")]
        public async Task<IActionResult> UpdateBySlug(string slug, Category updatedCategory)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Slug == slug);
            if (existingCategory == null) return NotFound();

            existingCategory.Name = updatedCategory.Name;
            existingCategory.Slug = GenerateSlug(updatedCategory.Name);
            existingCategory.ParentCategoryId = updatedCategory.ParentCategoryId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Category/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Category/slug/lanches
        [HttpDelete("slug/{slug}")]
        public async Task<IActionResult> DeleteBySlug(string slug)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Slug == slug);
            if (category == null) return NotFound();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Método auxiliar para gerar slugs
        private string GenerateSlug(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            string slug = name.ToLower().Trim()
                .Replace(" ", "-")
                .Replace("ç", "c")
                .Replace("á", "a")
                .Replace("é", "e")
                .Replace("í", "i")
                .Replace("ó", "o")
                .Replace("ú", "u")
                .Replace("ã", "a")
                .Replace("õ", "o")
                .Replace("â", "a")
                .Replace("ê", "e")
                .Replace("ô", "o")
                .Replace("ü", "u")
                .Replace("ñ", "n");

            return slug;
        }
    }
}
