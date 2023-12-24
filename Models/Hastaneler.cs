using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HastaneOtomasyonSistemi.Models
{
    public class Hastaneler
    {
        [Key]
        public int Id { get; set; }
        public string HastaneAd { get; set; }

        [DisplayName("il")]
        public int ilId { get; set; }
        [ForeignKey("ilId")]
        [ValidateNever]
        public il? il { get; set; }

        [DisplayName("ilce")]
        public int ilceId { get; set; }
        [ForeignKey("ilceId")]
        [ValidateNever]
        public ilce? ilce { get; set; }

        public List<poliklinik>? poliklinikler { get; set; }
    }
}
