using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TotemPWA.Data;
using TotemPWA.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TotemPWA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromotionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PromotionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Promotion
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var promotions = await _context.Promotions.Include(p => p.Product).ToListAsync();
            return Ok(promotions);
        }

        // POST: api/Promotion
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Promotion promotion)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _context.Promotions.Add(promotion);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAll), new { id = promotion.Id }, promotion);
        }

        // DELETE: api/Promotion/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var promotion = await _context.Promotions.FindAsync(id);
            if (promotion == null) return NotFound();
            _context.Promotions.Remove(promotion);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}