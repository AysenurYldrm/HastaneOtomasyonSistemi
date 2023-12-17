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
    public class randevuController : Controller
    {
        private readonly HastaneOtomasyonSistemiContext _context;

        public randevuController(HastaneOtomasyonSistemiContext context)
        {
            _context = context;
        }

        // GET: randevu
        public async Task<IActionResult> Index()
        {
            var hastaneOtomasyonSistemiContext = _context.randevu.Include(r => r.doktor).Include(r => r.hasta);
            return View(await hastaneOtomasyonSistemiContext.ToListAsync());
        }

        // GET: randevu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.randevu == null)
            {
                return NotFound();
            }

            var randevu = await _context.randevu
                .Include(r => r.doktor)
                .Include(r => r.hasta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        // GET: randevu/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Doktor, "Id", "Ad");
            ViewData["Id"] = new SelectList(_context.Hasta, "Id", "Ad");
            return View();
        }

        // POST: randevu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,hastaId,RandevuTarihi,doktorId")] randevu randevu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(randevu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Doktor, "Id", "Ad", randevu.Id);
            ViewData["Id"] = new SelectList(_context.Hasta, "Id", "Ad", randevu.Id);
            return View(randevu);
        }

        // GET: randevu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.randevu == null)
            {
                return NotFound();
            }

            var randevu = await _context.randevu.FindAsync(id);
            if (randevu == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Doktor, "Id", "Ad", randevu.Id);
            ViewData["Id"] = new SelectList(_context.Hasta, "Id", "Ad", randevu.Id);
            return View(randevu);
        }

        // POST: randevu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,hastaId,RandevuTarihi,doktorId")] randevu randevu)
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
                    if (!randevuExists(randevu.Id))
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
            ViewData["Id"] = new SelectList(_context.Doktor, "Id", "Ad", randevu.Id);
            ViewData["Id"] = new SelectList(_context.Hasta, "Id", "Ad", randevu.Id);
            return View(randevu);
        }

        // GET: randevu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.randevu == null)
            {
                return NotFound();
            }

            var randevu = await _context.randevu
                .Include(r => r.doktor)
                .Include(r => r.hasta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        // POST: randevu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.randevu == null)
            {
                return Problem("Entity set 'HastaneOtomasyonSistemiContext.randevu'  is null.");
            }
            var randevu = await _context.randevu.FindAsync(id);
            if (randevu != null)
            {
                _context.randevu.Remove(randevu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool randevuExists(int id)
        {
          return (_context.randevu?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
