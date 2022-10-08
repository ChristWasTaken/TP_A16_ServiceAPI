using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP_A16_ServiceAPI.models;

namespace TP_A16_ServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduitsController : ControllerBase
    {
        private readonly ProduitsContext _context;

        public ProduitsController(ProduitsContext context)
        {
            _context = context;
        }

        // GET: api/Produits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produits>>> GetProduits()
        {
            return await _context.Produits.ToListAsync();
        }

        // GET: api/Produits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produits>> GetProduits(int id)
        {
            var produits = await _context.Produits.FindAsync(id);

            if (produits == null)
            {
                return NotFound();
            }

            return produits;
        }

        // GET : api/Produits/categorie
        [HttpGet("categorie/{categorie}")]
        public async Task<ActionResult<IEnumerable<Produits>>> GetProduits(string categorie)
        {
            Console.WriteLine("\nCATEGORIE : " + categorie + "\n");
            return await _context.Produits.Where(p => p.Categorie == categorie).ToListAsync();
        }

        // PUT: api/Produits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduits(int id, Produits produits)
        {
            if (id != produits.Id)
            {
                return BadRequest();
            }

            _context.Entry(produits).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProduitsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Produits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Produits>> PostProduits(Produits produits)
        {
            _context.Produits.Add(produits);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduits), new { id = produits.Id }, produits);
        }

        // DELETE: api/Produits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduits(int id)
        {
            var produits = await _context.Produits.FindAsync(id);
            if (produits == null)
            {
                return NotFound();
            }

            _context.Produits.Remove(produits);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProduitsExists(int id)
        {
            return _context.Produits.Any(e => e.Id == id);
        }
    }
}
