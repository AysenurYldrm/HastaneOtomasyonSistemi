using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HastaneOtomasyonSistemi.Models
{
	public class ilce
	{
		[Key]
		public int Id { get; set; }
		
		public string ilceAd { get; set; } //= string.Empty;

		[DisplayName("İl")]
		public int ilId { get; set; }

		[ForeignKey("ilId")]
		[ValidateNever]
		public il? il { get; set; }
		public List<Hastaneler>? hastaneler { get; set; }
	}
}
