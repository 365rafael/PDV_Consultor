using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PDV_Consultor.Context;
using PDV_Consultor.Models;

namespace PDV_Consultor.Controllers
{
    public class ProdutoItemsController : Controller
    {
        private readonly AppDbContext _context;

        public ProdutoItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ProdutoItems
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ProdutoItems.Include(p => p.Produto).Where(p => p.Ativo == true);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ProdutoItems/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.ProdutoItems == null)
            {
                return NotFound();
            }

            var produtoItem = await _context.ProdutoItems
                .Include(p => p.Produto)
                .FirstOrDefaultAsync(m => m.SerialNumber == id);
            if (produtoItem == null)
            {
                return NotFound();
            }

            return View(produtoItem);
        }

        // GET: ProdutoItems/Create
        public IActionResult Create()
        {
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ID", "Nome");
            return View();
        }

        // POST: ProdutoItems/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SerialNumber,ProdutoId, Preco")] ProdutoItem produtoItem)
        {
            produtoItem.Preco = produtoItem.Preco / 100;
            produtoItem.Ativo = true;
            _context.Add(produtoItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: ProdutoItems/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ProdutoItems == null)
            {
                return NotFound();
            }

            var produtoItem = await _context.ProdutoItems.FindAsync(id);
            if (produtoItem == null)
            {
                return NotFound();
            }
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ID", "ID", produtoItem.ProdutoId);
            return View(produtoItem);
        }

        // POST: ProdutoItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("SerialNumber,ProdutoId,Preco")] ProdutoItem produtoItem)
        {
            if (id != produtoItem.SerialNumber)
            {
                return NotFound();
            }

            if (id != null)
            {
                try
                {
                    produtoItem.Preco = produtoItem.Preco / 100;
                    _context.Update(produtoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoItemExists(produtoItem.SerialNumber))
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
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ID", "ID", produtoItem.ProdutoId);
            return View(produtoItem);
        }

        // GET: ProdutoItems/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.ProdutoItems == null)
            {
                return NotFound();
            }

            var produtoItem = await _context.ProdutoItems
                .Include(p => p.Produto)
                .FirstOrDefaultAsync(m => m.SerialNumber == id);
            if (produtoItem == null)
            {
                return NotFound();
            }

            return View(produtoItem);
        }

        // POST: ProdutoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.ProdutoItems == null)
            {
                return Problem("Entity set 'AppDbContext.ProdutoItems'  is null.");
            }
            var produtoItem = await _context.ProdutoItems.FindAsync(id);
            if (produtoItem != null)
            {
                _context.ProdutoItems.Remove(produtoItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoItemExists(string id)
        {
          return (_context.ProdutoItems?.Any(e => e.SerialNumber == id)).GetValueOrDefault();
        }
    }
}
