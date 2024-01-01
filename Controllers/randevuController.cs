using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HastaneOtomasyonSistemi.Data;
using HastaneOtomasyonSistemi.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Session;

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
            var hastaneOtomasyonSistemiContext = _context.Randevu.Include(r => r.doktor).Include(r => r.hastaneler).Include(r => r.il).Include(r => r.ilce).Include(r => r.poliklinik);
            return View(await hastaneOtomasyonSistemiContext.ToListAsync());
        }
        public async Task<IActionResult> DoktorRandevu()
        {
            var currentDoctor = HttpContext.Session.GetInt32("UserDoktor");
            var hastaneOtomasyonSistemiContext = _context.Randevu.Include(r => r.doktor).Include(r => r.hastaneler).Include(r => r.il).Include(r => r.ilce).Include(r => r.poliklinik).Where(r => r.doktor.Id == currentDoctor);
            return View(await hastaneOtomasyonSistemiContext.ToListAsync());
        }
        public async Task<IActionResult> HastaRandevu()
        {
            var currentHasta = HttpContext.Session.GetInt32("UserHasta");
            var hastaneOtomasyonSistemiContext = _context.Randevu.Include(r => r.doktor).Include(r => r.hastaneler).Include(r => r.il).Include(r => r.ilce).Include(r => r.poliklinik).Where(r => r.hastaId == currentHasta);
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
                .Include(r => r.hastaneler)
                .Include(r => r.il)
                .Include(r => r.ilce)
                .Include(r => r.poliklinik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }
        // GET: Randevu/Details/5
        public async Task<IActionResult> RandevuDetails(int? id)
        {
            if (id == null || _context.Randevu == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevu
                .Include(r => r.doktor)
                .Include(r => r.hastaneler)
                .Include(r => r.il)
                .Include(r => r.ilce)
                .Include(r => r.poliklinik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }


        // GET: Randevu/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Iller = new SelectList(_context.il, "Id", "ilAd");
            ViewBag.Ilceler = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "ilceAd");
            ViewBag.Hastaneler = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "HastaneAd");
            ViewBag.Poliklinikler = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "PoliklinikIsmi");
            ViewBag.Doktorlar = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "Ad");
            return View();
        }
        [HttpGet]
        public JsonResult GetIlceler(int ilId)
        {
            var ilceList = _context.ilce.Where(x => x.ilId == ilId)
                                           .Select(x => new { Value = x.Id, text = x.ilceAd })
                                           .ToList();
            ViewBag.Ilceler = new SelectList(ilceList, "Value", "text");
            if (ilceList.Any())
            {
                return Json(ilceList);
            }
            else
            {
                return Json(new SelectList(ilceList, "Id", "ilceAd"));
            }
        }

        public JsonResult GetHastane(int ilceId)
        {
            var hastaneList = _context.Hastaneler.Where(x => x.ilceId == ilceId)
                                           .Select(x => new { Value = x.Id, text = x.HastaneAd })
                                           .ToList();
            ViewBag.Hastaneler = new SelectList(hastaneList, "Value", "text");
            if (hastaneList.Any())
            {
                return Json(hastaneList);
            }
            else
            {
                return Json(new SelectList(hastaneList, "Id", "HastaneAd"));
            }
        }

        public JsonResult GetPoliklinik(int hastaneId)
        {
            var poliklinikList = _context.poliklinik.Where(x => x.hastaneId == hastaneId)
                                           .Select(x => new { Value = x.Id, text = x.PoliklinikIsmi })
                                           .ToList();
            ViewBag.Poliklinikler = new SelectList(poliklinikList, "Value", "text");
            if (poliklinikList.Any())
            {
                return Json(poliklinikList);
            }
            else
            {
                return Json(new SelectList(poliklinikList, "Id", "PoliklinikIsmi"));
            }
        }
        public JsonResult GetDoktor(int poliklinikId)
        {
            var doktorList = _context.Doktor.Where(x => x.poliklinikId == poliklinikId)
                                           .Select(x => new { Value = x.Id, text = x.Ad })
                                           .ToList();
            ViewBag.Doktorlar = new SelectList(doktorList, "Value", "text");
            if (doktorList.Any())
            {
                return Json(doktorList);
            }
            else
            {
                return Json(new SelectList(doktorList, "Id", "Ad"));
            }
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,hastaId,ilId,ilceId,hastaneId,RandevuTarihi,RandevuDurumu,doktorId,poliklinikId")] Randevu randevu)
        {
            int? userId = HttpContext.Session.GetInt32("UserHasta");

            if (userId.HasValue)
            {
                // Kullanıcı oturumu var, hastaId'yi userId'den al
                randevu.hastaId = userId.Value;
            }

            if (ModelState.IsValid)
            {
                _context.Add(randevu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(HastaRandevu));
            }
          
            ViewBag.Iller = new SelectList(_context.il, "Id", "ilAd",randevu.ilId);
            ViewBag.Ilceler = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "ilceAd");
            ViewBag.Hastaneler = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "HastaneAd");
            ViewBag.Poliklinikler = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "PoliklinikIsmi");
            ViewBag.Doktorlar = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "Ad");
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
            ViewBag.Iller = new SelectList(_context.il, "Id", "ilAd",randevu.ilId);
            return View(randevu);
        }

        // POST: Randevu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,hastaId,ilId,ilceId,hastaneId,RandevuTarihi,RandevuDurumu,doktorId,poliklinikId")] Randevu randevu)
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
            ViewData["hastaneId"] = new SelectList(_context.Hastaneler, "Id", "Id", randevu.hastaneId);
            ViewData["ilId"] = new SelectList(_context.il, "Id", "ilAd", randevu.ilId);
            ViewData["ilceId"] = new SelectList(_context.ilce, "Id", "Id", randevu.ilceId);
            ViewData["poliklinikId"] = new SelectList(_context.poliklinik, "Id", "PoliklinikIsmi", randevu.poliklinikId);
            return View(randevu);
        }
        // GET: Randevu/Edit/5
        public async Task<IActionResult> EditAdmin(int? id)
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
            ViewBag.Iller = new SelectList(_context.il, "Id", "ilAd", randevu.ilId);
            return View(randevu);
        }

        // POST: Randevu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAdmin(int id, [Bind("Id,hastaId,ilId,ilceId,hastaneId,RandevuTarihi,RandevuDurumu,doktorId,poliklinikId")] Randevu randevu)
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
            ViewData["hastaneId"] = new SelectList(_context.Hastaneler, "Id", "Id", randevu.hastaneId);
            ViewData["ilId"] = new SelectList(_context.il, "Id", "ilAd", randevu.ilId);
            ViewData["ilceId"] = new SelectList(_context.ilce, "Id", "Id", randevu.ilceId);
            ViewData["poliklinikId"] = new SelectList(_context.poliklinik, "Id", "PoliklinikIsmi", randevu.poliklinikId);
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
                .Include(r => r.hastaneler)
                .Include(r => r.il)
                .Include(r => r.ilce)
                .Include(r => r.poliklinik)
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
        public async Task<IActionResult> DeleteConfirmed2(int id)
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

        // GET: Randevu/Delete/5
        public async Task<IActionResult> DeleteHasta(int? id)
        {
            if (id == null || _context.Randevu == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevu
                .Include(r => r.doktor)
                .Include(r => r.hastaneler)
                .Include(r => r.il)
                .Include(r => r.ilce)
                .Include(r => r.poliklinik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        // POST: Randevu/Delete/5
        [HttpPost, ActionName("DeleteHasta")]
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
            return RedirectToAction(nameof(HastaRandevu));
        }

        private bool RandevuExists(int id)
        {
          return (_context.Randevu?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
