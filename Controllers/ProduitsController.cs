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
        public async Task<ActionResult<IEnumerable<ProduitsDTO>>> GetProduits()
        {
            return await _context.Produits.Select(x => ProduitsDTO.ProduitsToDTO(x)).ToListAsync();
        }    

        // GET: api/Produits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProduitsDTO>> GetProduits(int id)
        {
            var produits = await _context.Produits.FindAsync(id);

            if (produits == null)
            {
                return NotFound();
            }

            return ProduitsDTO.ProduitsToDTO(produits);
        }

        //Get : api/Produits/prix/prixMin/prixMax
        // Fonctionnalité extra : chercher entre un intervale de 2 prix
        [HttpGet("prix/{prixMin}/{prixMax}")]
        public async Task<ActionResult<IEnumerable<ProduitsDTO>>> GetProduits(decimal prixMin, decimal prixMax)
        {
            return await _context.Produits.Where(p => p.PrixUnitaire >= prixMin && p.PrixUnitaire <= prixMax)
                .Select(x => ProduitsDTO.ProduitsToDTO(x)).ToListAsync();
        }

        // GET : api/Produits/categorie
        // Fonctionnalité extra : Chercher par catégorie
        [HttpGet("categorie/{categorie}")]
        public async Task<ActionResult<IEnumerable<ProduitsDTO>>> GetProduits(string categorie)
        {
            Console.WriteLine("\nCATEGORIE : " + categorie + "\n");
            return await _context.Produits.Where(p => p.Categorie == categorie)
                .Select(x => ProduitsDTO.ProduitsToDTO(x)).ToListAsync();
        }

        // PUT: api/Produits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduits(int id, ProduitsDTO produitsDTO)
        {
            if (id != produitsDTO.Id)
            {
                return BadRequest();
                Console.WriteLine("ID pas bonne");
            }

            var produit = await _context.Produits.FindAsync(id);
            Console.WriteLine(produit.Categorie);
            if(produit == null)
            {
                return NotFound();
            }

            produit = Produits.ProduitsDtoToProduits(produit, produitsDTO);
            Console.WriteLine("apreès: " + produit.Categorie);

            _context.Entry(produit).State = EntityState.Modified;

            try
            {
                Console.WriteLine("allo");
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ProduitsExists(id))
            {
                Console.WriteLine("erreur d'insertion");
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/Produits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProduitsDTO>> PostProduits(ProduitsDTO produitsDto)
        {
            var produit = new Produits();
            produit = Produits.ProduitsDtoToProduits(produit, produitsDto);

            _context.Produits.Add(produit);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetProduits), 
                new { id = produitsDto.Id }, ProduitsDTO.ProduitsToDTO(produit));
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
