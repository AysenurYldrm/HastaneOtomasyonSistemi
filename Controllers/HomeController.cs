using HastaneOtomasyonSistemi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using HastaneOtomasyonSistemi.Data;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.AspNetCore.Session;

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
        }

		public ActionResult HomeLogin()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		public ActionResult HomeLogin(Doktor doktor, Hasta hasta)
		{
			var loginHasta = _context.Hasta.FirstOrDefault(y => y.KimlikNo == hasta.KimlikNo && y.Sifre == hasta.Sifre);

			var loginDoktor = _context.Doktor.FirstOrDefault(x => x.KimlikNo == doktor.KimlikNo && x.Sifre == doktor.Sifre);

			if (!string.IsNullOrEmpty(hasta.KimlikNo) || !string.IsNullOrEmpty(hasta.Sifre))
			{ 
				if (loginHasta != null)
				{
					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name,hasta.KimlikNo)
                    };
					HttpContext.Session.SetInt32("UserHasta", loginHasta.Id);

                    var useridenty = new ClaimsIdentity(claims, "Login");
					ClaimsPrincipal principal = new ClaimsPrincipal(useridenty);
                    HttpContext.SignInAsync(principal);
					return RedirectToAction("HastaDetails", "Hasta");
				}
				else
				{
					ViewBag.UyariH = "E-postanız ya da şifreniz yanlış";
				}
			}
			if (!string.IsNullOrEmpty(doktor.KimlikNo) || !string.IsNullOrEmpty(doktor.Sifre))
			{
				if (loginDoktor != null)
				{
					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name,doktor.KimlikNo)
					};
                    HttpContext.Session.SetInt32("UserDoktor", loginDoktor.Id);

                    var useridenty = new ClaimsIdentity(claims, "Login");
					ClaimsPrincipal principal = new ClaimsPrincipal(useridenty);
					HttpContext.SignInAsync(principal);
					return RedirectToAction("DetailsDoktor", "Doktor");
				}
				else
				{
					ViewBag.UyariD = "E-postanız ya da şifreniz yanlış";
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