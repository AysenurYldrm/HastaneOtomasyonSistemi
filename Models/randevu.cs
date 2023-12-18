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
        [DisplayName("hasta")]
       
        public int hastaId { get; set; }
        [ForeignKey("hastaId")]
        [ValidateNever]
        public Hasta? hasta { get; set; }

        [Required]
        [time(480, 1020)]
        [DisplayName("Randevu Tarihi")]
        public DateTime RandevuTarihi { get; set; }


        [DisplayName("doktor")]
        public int doktorId { get; set; }

        [ForeignKey("doktorId")]
        [ValidateNever]
        public Doktor? doktor { get; set; }

    }
}
