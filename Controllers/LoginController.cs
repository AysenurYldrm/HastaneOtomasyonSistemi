using HastaneOtomasyonSistemi.Data;
using Microsoft.AspNetCore.Mvc;
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
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly HastaneOtomasyonSistemiContext _context;

        public LoginController(HastaneOtomasyonSistemiContext context)
        {
            _context = context;
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Doktor doktor)
        {
            if (ModelState.IsValid)
            {

                var loginDoktor = _context.Doktor.FirstOrDefault(x => x.KimlikNo == doktor.KimlikNo && x.Sifre == doktor.Sifre);

                if (loginDoktor != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,doktor.KimlikNo)
                    };
                    var useridenty = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(useridenty);
                    HttpContext.SignInAsync(principal);

                    return RedirectToAction("Index", "Doktor");
                }
                else
                {
                    ViewBag.Uyari = "E-postanız ya da şifreniz yanlış";
                }
            }
            return View();

        }

        // GET: /Account/Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Home"); // Çıkış yapıldıktan sonra yönlendirilecek sayfa
        }
    }
}
