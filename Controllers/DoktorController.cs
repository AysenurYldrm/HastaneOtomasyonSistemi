using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HastaneOtomasyonSistemi.Data;
using HastaneOtomasyonSistemi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;


namespace HastaneOtomasyonSistemi.Controllers
{
    public class DoktorController : Controller
    {
        private readonly HastaneOtomasyonSistemiContext _context;

        public DoktorController(HastaneOtomasyonSistemiContext context)
        {
            _context = context;
        }
		
	

		// GET: Doktor
		public async Task<IActionResult> Index()
        {
            var hastaneOtomasyonSistemiContext = _context.Doktor.Include(d => d.poliklinik);
            return View(await hastaneOtomasyonSistemiContext.ToListAsync());
        }

        // GET: Doktor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Doktor == null)
            {
                return NotFound();
            }

            var doktor = await _context.Doktor
                .Include(d => d.poliklinik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doktor == null)
            {
                return NotFound();
            }

            return View(doktor);
        }

        // GET: Doktor/DetailsDoktor/5
        public async Task<IActionResult> DetailsDoktor(int? id)
        {
            // Kullanıcının doktor kimliğini al
            int? userDoktorId = HttpContext.Session.GetInt32("UserDoktor");

            // Eğer kullanıcı kimliği yoksa veya doktor koleksiyonu boşsa NotFound döndür
            if (userDoktorId == null || !_context.Doktor.Any())
            {
                return NotFound();
            }

            //// Eğer detay gösterilecek doktor kimliği belirtilmemişse NotFound döndür
            //if (id == null)
            //{
            //    return NotFound();
            //}

            // Eğer userDoktorId değeri varsa ve id ile uyuşmuyorsa, id'yi userDoktorId ile güncelle
            if (userDoktorId != null && userDoktorId != id)
            {
                id = userDoktorId;
            }

            // Doktoru bul ve eğer yoksa NotFound döndür
            var doktor = await _context.Doktor
                .Include(d => d.poliklinik)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (doktor == null)
            {
                return NotFound();
            }

            return View(doktor);
        }


        // GET: Doktor/Create
        public IActionResult Create()
        {
            ViewData["poliklinikId"] = new SelectList(_context.poliklinik, "Id", "PoliklinikIsmi");
            return View();
        }

        // POST: Doktor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ad,soyAd,KimlikNo,Sifre,poliklinikId")] Doktor doktor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doktor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["poliklinikId"] = new SelectList(_context.poliklinik, "Id", "PoliklinikIsmi", doktor.poliklinikId);
            return View(doktor);
        }

        // GET: Doktor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Doktor == null)
            {
                return NotFound();
            }

            var doktor = await _context.Doktor.FindAsync(id);
            if (doktor == null)
            {
                return NotFound();
            }
            ViewData["poliklinikId"] = new SelectList(_context.poliklinik, "Id", "PoliklinikIsmi", doktor.poliklinikId);
            return View(doktor);
        }

        // POST: Doktor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ad,soyAd,KimlikNo,Sifre,poliklinikId")] Doktor doktor)
        {
            if (id != doktor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doktor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoktorExists(doktor.Id))
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
            ViewData["poliklinikId"] = new SelectList(_context.poliklinik, "Id", "PoliklinikIsmi", doktor.poliklinikId);
            return View(doktor);
        }

        // GET: Doktor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Doktor == null)
            {
                return NotFound();
            }

            var doktor = await _context.Doktor
                .Include(d => d.poliklinik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doktor == null)
            {
                return NotFound();
            }

            return View(doktor);
        }

        // POST: Doktor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Doktor == null)
            {
                return Problem("Entity set 'HastaneOtomasyonSistemiContext.Doktor'  is null.");
            }
            var doktor = await _context.Doktor.FindAsync(id);
            if (doktor != null)
            {
                _context.Doktor.Remove(doktor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoktorExists(int id)
        {
          return (_context.Doktor?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
