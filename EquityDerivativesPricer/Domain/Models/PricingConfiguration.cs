using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EquityDerivativesPricer.Domain.Models
{
	public class PricingConfiguration
	{
		[Required]
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public NumericalMethod NumericalMethod { get; set; }
		public bool IsCalculationWithGreeks { get; set; } = true;
	}
}
