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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HastaneOtomasyonSistemi.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly HastaneOtomasyonSistemiContext _context;

            public ApiController(HastaneOtomasyonSistemiContext context)
            {
                _context = context;
            }

            // GET: api/hasta
            [HttpGet]
            public IActionResult GetHasta()
            {
                var hastaList = _context.Hasta.ToList();
                if (hastaList == null || !hastaList.Any())
                {
                    return NotFound("Hasta bulunamadı.");
                }
                return Ok(hastaList);
            }

            // GET: api/hasta/5
            [HttpGet("{id}")]
            public IActionResult GetHasta(int id)
            {
                var hasta = _context.Hasta.FirstOrDefault(h => h.Id == id);

                if (hasta == null)
                {
                    return NotFound("Hasta bulunamadı.");
                }

                return Ok(hasta);
            }

            // POST: api/hasta
            [HttpPost]
            public IActionResult PostHasta([FromBody] Hasta hasta)
            {
                if (hasta == null)
                {
                    return BadRequest("Geçersiz hasta bilgisi.");
                }

                _context.Hasta.Add(hasta);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetHasta), new { id = hasta.Id }, hasta);
            }

            // PUT: api/hasta/5
            [HttpPut("{id}")]
            public IActionResult PutHasta(int id, [FromBody] Hasta hasta)
            {
                if (hasta == null || id != hasta.Id)
                {
                    return BadRequest("Geçersiz hasta bilgisi.");
                }

                _context.Entry(hasta).State = EntityState.Modified;
                _context.SaveChanges();

                return NoContent();
            }

            // DELETE: api/hasta/5
            [HttpDelete("{id}")]
            public IActionResult DeleteHasta(int id)
            {
                var hasta = _context.Hasta.FirstOrDefault(h => h.Id == id);

                if (hasta == null)
                {
                    return NotFound("Hasta bulunamadı.");
                }

                _context.Hasta.Remove(hasta);
                _context.SaveChanges();

                return NoContent();
            }
     }
}

