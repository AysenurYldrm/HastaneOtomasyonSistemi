using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HastaneOtomasyonSistemi.Models
{
	public class il
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[DisplayName("Şehir")]
		public string ilAd { get; set; }
		public List<ilce>? ilceler {  get; set; }
	}
}
