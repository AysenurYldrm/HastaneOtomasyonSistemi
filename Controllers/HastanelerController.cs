using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HastaneOtomasyonSistemi.Data;
using HastaneOtomasyonSistemi.Models;
//using HastaneOtomasyonSistemi.Migrations;

namespace HastaneOtomasyonSistemi.Controllers
{
    public class HastanelerController : Controller
    {
        private readonly HastaneOtomasyonSistemiContext _context;

        public HastanelerController(HastaneOtomasyonSistemiContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //var hastaneler = _context.Hastaneler.Include(h => h.il.ilceler).ToList();
            var hastaneler = _context.Hastaneler.Include(h => h.il).Include(h => h.ilce).ToList();
            return View(hastaneler);
        }
        // GET: Hastaneler
        //public async Task<IActionResult> Index()
        //{
        //    var hastaneOtomasyonSistemiContext = _context.Hastaneler.Include(h => h.il).Include(h => h.ilce).Include(h => h.poliklinikler);
        //    return View(await hastaneOtomasyonSistemiContext.ToListAsync());
        //}

        // GET: Hastaneler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Hastaneler == null)
            {
                return NotFound();
            }

            var hastaneler = await _context.Hastaneler
                .Include(h => h.il)
                .Include(h => h.ilce)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hastaneler == null)
            {
                return NotFound();
            }

            return View(hastaneler);
        }


        //GET: Hastane/Create
        public ActionResult Create()
        {
            ViewBag.Iller = new SelectList(_context.il, "Id", "ilAd");

            return View();
        }
        [HttpGet]
     
        public JsonResult GetIlceler(int ilId)
        {
            var ilceList = _context.ilce.Where(x => x.ilId == ilId)
                                           .Select(x => new { Value = x.Id, text = x.ilceAd })
                                           .ToList();

            if (ilceList.Any())
            {
                return Json(ilceList);
            }
            else
            {
                return Json(new SelectList(ilceList, "Id", "ilceAd"));
            }
        }


        // POST: Hastaneler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HastaneAd,ilId,ilceId")] Hastaneler hastaneler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hastaneler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ilId = new SelectList(_context.il, "Id", "ilAd", hastaneler.ilId);
            return View(hastaneler);
        }

        // GET: Hastaneler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Hastaneler == null)
            {
                return NotFound();
            }

            var hastaneler = await _context.Hastaneler.FindAsync(id);
            if (hastaneler == null)
            {
                return NotFound();
            }
            ViewBag.Iller = _context.il.ToList();
            //ViewData["ilId"] = new SelectList(_context.il, "Id", "ilAd", hastaneler.ilId);
            //ViewData["ilceId"] = new SelectList(_context.ilce, "Id", "Id", hastaneler.ilceId);
            return View(hastaneler);
        }

        // POST: Hastaneler/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HastaneAd,ilId,ilceId")] Hastaneler hastaneler)
        {
            if (id != hastaneler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hastaneler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HastanelerExists(hastaneler.Id))
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
            ViewBag.Iller = _context.il.ToList();
            //ViewData["ilId"] = new SelectList(_context.il, "Id", "ilAd", hastaneler.ilId);
            //ViewData["ilceId"] = new SelectList(_context.ilce, "Id", "Id", hastaneler.ilceId);
            return View(hastaneler);
        }

        // GET: Hastaneler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Hastaneler == null)
            {
                return NotFound();
            }

            var hastaneler = await _context.Hastaneler
                .Include(h => h.il)
                .Include(h => h.ilce)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hastaneler == null)
            {
                return NotFound();
            }

            return View(hastaneler);
        }

        // POST: Hastaneler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Hastaneler == null)
            {
                return Problem("Entity set 'HastaneOtomasyonSistemiContext.Hastaneler'  is null.");
            }
            var hastaneler = await _context.Hastaneler.FindAsync(id);
            if (hastaneler != null)
            {
                _context.Hastaneler.Remove(hastaneler);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HastanelerExists(int id)
        {
          return (_context.Hastaneler?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
