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
    
    public class AdminController : Controller
    { 
        private readonly HastaneOtomasyonSistemiContext _context;

        public AdminController(HastaneOtomasyonSistemiContext context)
        {
            _context = context;
        }
        // GET: /Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Admin admin)
        {
            if (ModelState.IsValid) { 

                var loginAdmin = _context.Admin.FirstOrDefault(x => x.Email == admin.Email && x.Sifre == admin.Sifre);

                if (loginAdmin != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,admin.Email)
                    };
                    HttpContext.Session.SetString("Admin", loginAdmin.Email);
                    var useridenty = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(useridenty);
                    HttpContext.SignInAsync(principal);
                    ViewData["Eposta"] = loginAdmin.Email;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.UyariA = "E-postanız yada şifreniz yanlış";
                }
            }
            return View();

        }

        // GET: /Account/Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Admin", "Login"); // Çıkış yapıldıktan sonra yönlendirilecek sayfa
        }
        // GET: Admin
        public async Task<IActionResult> Index()
        {
            ViewBag.Admin = HttpContext.Session.GetString("Admin");
            return _context.Admin != null ? 
                          View(await _context.Admin.ToListAsync()) :
                          Problem("Entity set 'HastaneOtomasyonSistemiContext.Admin'  is null.");
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Admin == null)
            {
                return NotFound();
            }

            var admin = await _context.Admin
                .FirstOrDefaultAsync(m => m.Id == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Sifre")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Admin == null)
            {
                return NotFound();
            }

            var admin = await _context.Admin.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Sifre")] Admin admin)
        {
            if (id != admin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.Id))
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
            return View(admin);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Admin == null)
            {
                return NotFound();
            }

            var admin = await _context.Admin
                .FirstOrDefaultAsync(m => m.Id == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Admin == null)
            {
                return Problem("Entity set 'HastaneOtomasyonSistemiContext.Admin'  is null.");
            }
            var admin = await _context.Admin.FindAsync(id);
            if (admin != null)
            {
                _context.Admin.Remove(admin);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminExists(int id)
        {
          return (_context.Admin?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
