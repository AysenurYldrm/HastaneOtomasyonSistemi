using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HastaneOtomasyonSistemi.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "E-posta adresi giriniz")]
        [DisplayName("E-posta Adresi")]
        [EmailAddress(ErrorMessage = "Geçersiz e-posta adresi formatı")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Metin 8 ile 16 karakter arasında olmalıdır.")]
        [Required(ErrorMessage = "Şifre giriniz")]
        [DisplayName("Şifre")]
        public string Sifre { get; set; }

    }
}
