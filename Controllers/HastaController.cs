using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HastaneOtomasyonSistemi.Data;
using HastaneOtomasyonSistemi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HastaneOtomasyonSistemi.Controllers
{
    public class HastaController : Controller
    {
        private readonly HastaneOtomasyonSistemiContext _context;

        public HastaController(HastaneOtomasyonSistemiContext context)
        {
            _context = context;
        }
	

		// GET: Hasta
		public async Task<IActionResult> Index()
        {
              return _context.Hasta != null ? 
                          View(await _context.Hasta.ToListAsync()) :
                          Problem("Entity set 'HastaneOtomasyonSistemiContext.Hasta'  is null.");
        }

        // GET: Hasta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Hasta == null)
            {
                return NotFound();
            }

            var hasta = await _context.Hasta
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hasta == null)
            {
                return NotFound();
            }

            return View(hasta);
        }

        // GET: Doktor/DetailsDoktor/5
        public async Task<IActionResult> HastaDetails(int? id)
        {
            // Kullanıcının doktor kimliğini al
            int? userHastaId = HttpContext.Session.GetInt32("UserHasta");

            // Eğer kullanıcı kimliği yoksa veya doktor koleksiyonu boşsa NotFound döndür
            if (userHastaId == null || !_context.Hasta.Any())
            {
                return NotFound();
            }

            //// Eğer detay gösterilecek doktor kimliği belirtilmemişse NotFound döndür
            //if (id == null)
            //{
            //    return NotFound();
            //}

            // Eğer userDoktorId değeri varsa ve id ile uyuşmuyorsa, id'yi userDoktorId ile güncelle
            if (userHastaId != null && userHastaId != id)
            {
                id = userHastaId;
            }

            // Doktoru bul ve eğer yoksa NotFound döndür
            var hasta = await _context.Hasta
             .FirstOrDefaultAsync(m => m.Id == id);

            if (hasta == null)
            {
                return NotFound();
            }

            return View(hasta);
        }
        // GET: Hasta/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hasta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ad,soyAd,Sifre,KimlikNo,DogumTarihi")] Hasta hasta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hasta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hasta);
        }

        // GET: Hasta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Hasta == null)
            {
                return NotFound();
            }

            var hasta = await _context.Hasta.FindAsync(id);
            if (hasta == null)
            {
                return NotFound();
            }
            return View(hasta);
        }

        // POST: Hasta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ad,soyAd,Sifre,KimlikNo,DogumTarihi")] Hasta hasta)
        {
            if (id != hasta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hasta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HastaExists(hasta.Id))
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
            return View(hasta);
        }

        // GET: Hasta/Edit/5
        public async Task<IActionResult> EditAdmin(int? id)
        {
            if (id == null || _context.Hasta == null)
            {
                return NotFound();
            }

            var hasta = await _context.Hasta.FindAsync(id);
            if (hasta == null)
            {
                return NotFound();
            }
            return View(hasta);
        }

        // POST: Hasta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAdmin(int id, [Bind("Id,Ad,soyAd,Sifre,KimlikNo,DogumTarihi")] Hasta hasta)
        {
            if (id != hasta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hasta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HastaExists(hasta.Id))
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
            return View(hasta);
        }

        // GET: Hasta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Hasta == null)
            {
                return NotFound();
            }

            var hasta = await _context.Hasta
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hasta == null)
            {
                return NotFound();
            }

            return View(hasta);
        }

        // POST: Hasta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Hasta == null)
            {
                return Problem("Entity set 'HastaneOtomasyonSistemiContext.Hasta'  is null.");
            }
            var hasta = await _context.Hasta.FindAsync(id);
            if (hasta != null)
            {
                _context.Hasta.Remove(hasta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HastaExists(int id)
        {
          return (_context.Hasta?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
