using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HastaneOtomasyonSistemi.Models
{
    public class Hasta
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Adınızı giriniz")]
        [DisplayName("Ad")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyadınızı giriniz")]
        [DisplayName("Soyad")]
        public string soyAd { get; set; }

        [DataType(DataType.Password)]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Metin 8 ile 16 karakter arasında olmalıdır.")]
        [Required(ErrorMessage = "Şifre giriniz")]
        [DisplayName("Şifre")]
        public string Sifre { get; set; }

        [Required(ErrorMessage = "TC kimlik numarası giriniz")]
        [DisplayName("TC Kimlik Numarası")]
        [StringLength(11, ErrorMessage = "TC kimlik numarası 11 karakter uzunluğunda olmalıdır")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "TC kimlik numarası sadece rakamlardan  ve 11 karakterden oluşmalıdır")]
        public string KimlikNo { get; set; }

        [Required(ErrorMessage = "Doğum tarihi giriniz")]
        [AfterCurrentDate(ErrorMessage = "Geçerli bir tarih seçiniz.")]
        [DisplayName("Doğum Tarihiniz")]
        [DataType(DataType.Date)]
        public DateTime DogumTarihi { get; set; }
        Nullable<int> randevular { get; set; }
    }
}
