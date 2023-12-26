using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HastaneOtomasyonSistemi.Models
{
    public class Randevu
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Hasta")]
        public int hastaId { get; set; }

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

        [Required]
        [time(480, 1020)]
        [DisplayName("Randevu Tarihi")]
        public DateTime RandevuTarihi { get; set; }

		[DisplayName("Randevu Durumu")]
		public string RandevuDurumu { get; set; }

		[DisplayName("Doktor")]
        public int doktorId { get; set; }

        [ForeignKey("doktorId")]
        [ValidateNever]
        public Doktor? doktor { get; set; }
        //public List<poliklinik>? poliklinikler { get; set; }

        [DisplayName("Poliklinik")]
        public int poliklinikId { get; set; }

        [ForeignKey("poliklinikId")]
        [ValidateNever]
        public poliklinik? poliklinik { get; set; }

    }
}
