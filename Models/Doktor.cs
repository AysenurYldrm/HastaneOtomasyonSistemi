using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HastaneOtomasyonSistemi.Models
{
    public class Doktor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Ad")]
        public string Ad { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Soyad")]
        public string soyAd { get; set; }

        [Required(ErrorMessage = "TC kimlik numarası giriniz")]
        [DisplayName("TC Kimlik Numarası")]
        [StringLength(11, ErrorMessage = "TC kimlik numarası 11 karakter uzunluğunda olmalıdır")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "TC kimlik numarası sadece rakamlardan  ve 11 karakterden oluşmalıdır")]
        public string KimlikNo { get; set; }

        [DataType(DataType.Password)]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Metin 8 ile 16 karakter arasında olmalıdır.")]
        [Required]
        [DisplayName("Şifre")]
        public string Sifre { get; set; }

		[DisplayName("Poliklinik")]
		public int poliklinikId { get; set; }

        [ForeignKey("poliklinikId")]
        [ValidateNever]
        public poliklinik? poliklinik { get; set; }

    }
}
