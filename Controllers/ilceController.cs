using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HastaneOtomasyonSistemi.Data;
using HastaneOtomasyonSistemi.Models;

namespace HastaneOtomasyonSistemi.Controllers
{
    public class ilceController : Controller
    {
        private readonly HastaneOtomasyonSistemiContext _context;

        public ilceController(HastaneOtomasyonSistemiContext context)
        {
            _context = context;
        }

        // GET: ilce
        public async Task<IActionResult> Index()
        {
            var hastaneOtomasyonSistemiContext = _context.ilce.Include(i => i.il);
            return View(await hastaneOtomasyonSistemiContext.ToListAsync());
        }

        // GET: ilce/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ilce == null)
            {
                return NotFound();
            }

            var ilce = await _context.ilce
                .Include(i => i.il)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ilce == null)
            {
                return NotFound();
            }

            return View(ilce);
        }

        // GET: ilce/Create
        public IActionResult Create()
        {
            ViewData["ilId"] = new SelectList(_context.il, "Id", "ilAd");
            return View();
        }

        // POST: ilce/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ilceAd,ilId")] ilce ilce)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ilce);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ilId"] = new SelectList(_context.il, "Id", "ilAd", ilce.ilId);
            return View(ilce);
        }

        // GET: ilce/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ilce == null)
            {
                return NotFound();
            }

            var ilce = await _context.ilce.FindAsync(id);
            if (ilce == null)
            {
                return NotFound();
            }
            ViewData["ilId"] = new SelectList(_context.il, "Id", "Id", ilce.ilId);
            return View(ilce);
        }

        // POST: ilce/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ilceAd,ilId")] ilce ilce)
        {
            if (id != ilce.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ilce);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ilceExists(ilce.Id))
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
            ViewData["ilId"] = new SelectList(_context.il, "Id", "Id", ilce.ilId);
            return View(ilce);
        }

        // GET: ilce/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ilce == null)
            {
                return NotFound();
            }

            var ilce = await _context.ilce
                .Include(i => i.il)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ilce == null)
            {
                return NotFound();
            }

            return View(ilce);
        }

        // POST: ilce/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ilce == null)
            {
                return Problem("Entity set 'HastaneOtomasyonSistemiContext.ilce'  is null.");
            }
            var ilce = await _context.ilce.FindAsync(id);
            if (ilce != null)
            {
                _context.ilce.Remove(ilce);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ilceExists(int id)
        {
          return (_context.ilce?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
