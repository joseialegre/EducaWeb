using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class NovedadsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NovedadsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Novedads
        public async Task<IActionResult> Index()
        {
              return _context.Novedad != null ? 
                          View(await _context.Novedad.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Novedad'  is null.");
        }

        // GET: Novedads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Novedad == null)
            {
                return NotFound();
            }

            var novedad = await _context.Novedad
                .FirstOrDefaultAsync(m => m.Id == id);
            if (novedad == null)
            {
                return NotFound();
            }

            return View(novedad);
        }

        // GET: Novedads/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Novedads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description")] Novedad novedad)
        {
            if (ModelState.IsValid)
            {
                novedad.Publisher = User.Identity.Name;
                novedad.Date = DateTime.Now;

                _context.Add(novedad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(novedad);
        }

        // GET: Novedads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Novedad == null)
            {
                return NotFound();
            }

            var novedad = await _context.Novedad.FindAsync(id);
            if (novedad == null)
            {
                return NotFound();
            }
            return View(novedad);
        }

        // POST: Novedads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description")] Novedad novedad)
        {
            if (id != novedad.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(novedad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NovedadExists(novedad.Id))
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
            return View(novedad);
        }

        // GET: Novedads/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Novedad == null)
            {
                return NotFound();
            }

            var novedad = await _context.Novedad
                .FirstOrDefaultAsync(m => m.Id == id);
            if (novedad == null)
            {
                return NotFound();
            }

            return View(novedad);
        }

        // POST: Novedads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Novedad == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Novedad'  is null.");
            }
            var novedad = await _context.Novedad.FindAsync(id);
            if (novedad != null)
            {
                _context.Novedad.Remove(novedad);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NovedadExists(int id)
        {
          return (_context.Novedad?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
