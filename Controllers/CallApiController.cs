using HastaneOtomasyonSistemi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HastaneOtomasyonSistemi.Controllers
{
    public class CallApiController : Controller
    {
        public async Task<IActionResult> ApiList()
        {
          
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://localhost:7116/api/Api");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var Hasta = JsonConvert.DeserializeObject<List<Hasta>>(jsonResponse);

            return View(Hasta);
        }


    }
}
