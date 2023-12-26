using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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

        [DisplayName("İl")]
   
        public int ilId { get; set; }
        [ForeignKey("ilId")]
        [ValidateNever]
        public il? il { get; set; }

        [DisplayName("İlce")]
   
        public int ilceId { get; set; }
        [ForeignKey("ilceId")]
        [ValidateNever]
        public ilce? ilce { get; set; }

        [DisplayName("Hastane")]
      
        public int hastaneId { get; set; }

        [ForeignKey("hastaneId")]
        [ValidateNever]
        public Hastaneler? hastaneler { get; set; }

        public List<Doktor> doktorlar { get; set; }
    }
}
