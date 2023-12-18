using HastaneOtomasyonSistemi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using HastaneOtomasyonSistemi.Data;

namespace HastaneOtomasyonSistemi.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger; 
        private readonly HastaneOtomasyonSistemiContext _context;

        public HomeController(HastaneOtomasyonSistemiContext context)
        {
            _context = context;
        }

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }// GET: /Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(Doktor doktor)
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
                    await HttpContext.SignInAsync(principal);

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}