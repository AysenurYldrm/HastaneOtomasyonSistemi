using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HastaneOtomasyonSistemi.Models;

namespace HastaneOtomasyonSistemi.Data
{
    public class HastaneOtomasyonSistemiContext : DbContext
    {
        public HastaneOtomasyonSistemiContext (DbContextOptions<HastaneOtomasyonSistemiContext> options)
            : base(options)
        {
        }

        public DbSet<HastaneOtomasyonSistemi.Models.Doktor>? Doktor { get; set; }

        public DbSet<HastaneOtomasyonSistemi.Models.Hasta>? Hasta { get; set; }

        public DbSet<HastaneOtomasyonSistemi.Models.randevu>? randevu { get; set; }

        public DbSet<HastaneOtomasyonSistemi.Models.Admin>? Admin { get; set; }

        public DbSet<HastaneOtomasyonSistemi.Models.poliklinik>? poliklinik { get; set; }
    }
}
