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
    public class poliklinikController : Controller
    {
        private readonly HastaneOtomasyonSistemiContext _context;

        public poliklinikController(HastaneOtomasyonSistemiContext context)
        {
            _context = context;
        }

        // GET: poliklinik
        public async Task<IActionResult> Index()
        {
            var hastaneOtomasyonSistemiContext = _context.poliklinik.Include(p => p.hastaneler);
            return View(await hastaneOtomasyonSistemiContext.ToListAsync());
        }

        // GET: poliklinik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.poliklinik == null)
            {
                return NotFound();
            }

            var poliklinik = await _context.poliklinik
                .Include(p => p.hastaneler)
                .Include(p => p.il)
                .Include(p => p.ilce)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (poliklinik == null)
            {
                return NotFound();
            }

            return View(poliklinik);
        }


        // GET: poliklinik/Create
        public ActionResult Create()
        {
            ViewBag.Iller = new SelectList(_context.il, "Id", "ilAd");
            ViewBag.Ilceler = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "ilceAd");
            ViewBag.Hastaneler = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "HastaneAd");
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

        // POST: poliklinik/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Id,PoliklinikIsmi,ilId,ilceId,hastaneId")] poliklinik poliklinik)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(poliklinik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Iller = new SelectList(_context.il, "Id", "ilAd",poliklinik.ilId);
            ViewBag.Ilceler = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "ilceAd");
            ViewBag.Hastaneler = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "HastaneAd");
            return View(poliklinik);
        }

        // GET: poliklinik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.poliklinik == null)
            {
                return NotFound();
            }

            var poliklinik = await _context.poliklinik.FindAsync(id);
            if (poliklinik == null)
            {
                return NotFound();
            }
            ViewData["hastaneId"] = new SelectList(_context.Hastaneler, "Id", "Id", poliklinik.hastaneId);
            ViewData["ilId"] = new SelectList(_context.il, "Id", "ilAd", poliklinik.ilId);
            ViewData["ilceId"] = new SelectList(_context.ilce, "Id", "Id", poliklinik.ilceId);
            return View(poliklinik);
        }

        // POST: poliklinik/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PoliklinikIsmi,ilId,ilceId,hastaneId")] poliklinik poliklinik)
        {
            if (id != poliklinik.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(poliklinik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!poliklinikExists(poliklinik.Id))
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
            ViewData["hastaneId"] = new SelectList(_context.Hastaneler, "Id", "Id", poliklinik.hastaneId);
            ViewData["ilId"] = new SelectList(_context.il, "Id", "ilAd", poliklinik.ilId);
            ViewData["ilceId"] = new SelectList(_context.ilce, "Id", "Id", poliklinik.ilceId);
            return View(poliklinik);
        }

        // GET: poliklinik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.poliklinik == null)
            {
                return NotFound();
            }

            var poliklinik = await _context.poliklinik
                .Include(p => p.hastaneler)
                .Include(p => p.il)
                .Include(p => p.ilce)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (poliklinik == null)
            {
                return NotFound();
            }

            return View(poliklinik);
        }

        // POST: poliklinik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.poliklinik == null)
            {
                return Problem("Entity set 'HastaneOtomasyonSistemiContext.poliklinik'  is null.");
            }
            var poliklinik = await _context.poliklinik.FindAsync(id);
            if (poliklinik != null)
            {
                _context.poliklinik.Remove(poliklinik);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool poliklinikExists(int id)
        {
          return (_context.poliklinik?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
