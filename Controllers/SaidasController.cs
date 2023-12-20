using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PDV_Consultor.Context;
using PDV_Consultor.Models;

namespace PDV_Consultor.Controllers
{
    public class SaidasController : Controller
    {
        private readonly AppDbContext _context;

        public SaidasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Saidas
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        {
            var saidas = await _context.Saidas.Include(s => s.ProdutoItem).ToListAsync();

            // Se startDate não foi especificada, define-a como o primeiro dia do mês atual
            if (!startDate.HasValue)
            {
                startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }

            // Se endDate não foi especificada, define-a como o último dia do mês atual
            if (!endDate.HasValue)
            {
                endDate = startDate.Value.AddMonths(1).AddDays(-1);
            }

            // Filtra por intervalo de datas
            saidas = saidas.Where(s => s.DataSaida >= startDate.Value && s.DataSaida <= endDate.Value).ToList();
            ViewData["StartDate"] = startDate;
            ViewData["EndDate"] = endDate;

            return View(saidas);
        }


        // GET: Saidas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Saidas == null)
            {
                return NotFound();
            }

            var saida = await _context.Saidas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saida == null)
            {
                return NotFound();
            }

            return View(saida);
        }

        // GET: Saidas/Create
        public IActionResult Create()
        {
            var serialNumbersWithProduct = _context.ProdutoItems.Where(p => p.Ativo == true)
        .Select(pi => new SelectListItem
        {
            Value = pi.SerialNumber,
            Text = $"{pi.Produto.Nome} - {pi.SerialNumber}"
        })
        .ToList();

            ViewData["SerialNumber"] = serialNumbersWithProduct;

            return View();
        }

        // POST: Saidas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SerialNumber,DataSaida,NomeCliente,Preco,ClienteNovo,Troca")] Saida saida)
        {
            saida.Preco = saida.Preco / 100;
            _context.Add(saida);
            await _context.SaveChangesAsync();

            var produtoItem = await _context.ProdutoItems.FirstOrDefaultAsync(pi=> pi.SerialNumber == saida.SerialNumber);

            if (produtoItem != null)
            {
                produtoItem.Ativo = false; // Marcar a propriedade Ativo como false
                await _context.SaveChangesAsync(); // Salvar as alterações no ProdutoItem
            }

            return RedirectToAction(nameof(Index));

        }

        // GET: Saidas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Saidas == null)
            {
                return NotFound();
            }

            var saida = await _context.Saidas.FindAsync(id);
            if (saida == null)
            {
                return NotFound();
            }
            return View(saida);
        }

        // POST: Saidas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SerialNumber,DataSaida,NomeCliente,Preco,ClienteNovo,Troca")] Saida saida)
        {
            if (id != saida.Id)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(saida);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaidaExists(saida.Id))
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

        // GET: Saidas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Saidas == null)
            {
                return NotFound();
            }

            var saida = await _context.Saidas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saida == null)
            {
                return NotFound();
            }

            return View(saida);
        }

        // POST: Saidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Saidas == null)
            {
                return Problem("Entity set 'AppDbContext.Saidas'  is null.");
            }
            var saida = await _context.Saidas.FindAsync(id);
            if (saida != null)
            {
                _context.Saidas.Remove(saida);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaidaExists(int id)
        {
            return (_context.Saidas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
