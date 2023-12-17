using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HastaneOtomasyonSistemi.Models
{
    public class poliklinik
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Poliklinik")]
        public string PoliklinikIsmi { get; set; }
        //public List<Doktor> klinikler { get; set; }
    }
}
