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
    public class RandevuController : Controller
    {
        private readonly HastaneOtomasyonSistemiContext _context;

        public RandevuController(HastaneOtomasyonSistemiContext context)
        {
            _context = context;
        }

        // GET: Randevu
        public async Task<IActionResult> Index()
        {
            var hastaneOtomasyonSistemiContext = _context.Randevu.Include(r => r.doktor).Include(r => r.hasta);
            return View(await hastaneOtomasyonSistemiContext.ToListAsync());
        }

        // GET: Randevu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Randevu == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevu
                .Include(r => r.doktor)
                .Include(r => r.hasta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        // GET: Randevu/Create
        public IActionResult Create()
        {
            ViewData["doktorId"] = new SelectList(_context.Doktor, "Id", "Ad");
            ViewData["hastaId"] = new SelectList(_context.Hasta, "Id", "Ad");
            return View();
        }

        // POST: Randevu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,hastaId,RandevuTarihi,RandevuDurumu,doktorId")] Randevu randevu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(randevu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["doktorId"] = new SelectList(_context.Doktor, "Id", "Ad", randevu.doktorId);
            ViewData["hastaId"] = new SelectList(_context.Hasta, "Id", "Ad", randevu.hastaId);
            return View(randevu);
        }

        // GET: Randevu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Randevu == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevu.FindAsync(id);
            if (randevu == null)
            {
                return NotFound();
            }
            ViewData["doktorId"] = new SelectList(_context.Doktor, "Id", "Ad", randevu.doktorId);
            ViewData["hastaId"] = new SelectList(_context.Hasta, "Id", "Ad", randevu.hastaId);
            return View(randevu);
        }

        // POST: Randevu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,hastaId,RandevuTarihi,RandevuDurumu,doktorId")] Randevu randevu)
        {
            if (id != randevu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(randevu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RandevuExists(randevu.Id))
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
            ViewData["doktorId"] = new SelectList(_context.Doktor, "Id", "Ad", randevu.doktorId);
            ViewData["hastaId"] = new SelectList(_context.Hasta, "Id", "Ad", randevu.hastaId);
            return View(randevu);
        }

        // GET: Randevu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Randevu == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevu
                .Include(r => r.doktor)
                .Include(r => r.hasta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        // POST: Randevu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Randevu == null)
            {
                return Problem("Entity set 'HastaneOtomasyonSistemiContext.Randevu'  is null.");
            }
            var randevu = await _context.Randevu.FindAsync(id);
            if (randevu != null)
            {
                _context.Randevu.Remove(randevu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RandevuExists(int id)
        {
          return (_context.Randevu?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
