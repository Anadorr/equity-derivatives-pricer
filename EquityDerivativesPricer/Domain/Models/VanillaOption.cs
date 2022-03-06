using System.Text.Json.Serialization;

namespace EquityDerivativesPricer.Domain.Models
{
	public class VanillaOption : IPriceable
	{
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public OptionStyle OptionStyle { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public OptionType OptionType { get; set; }

		public double Strike { get; set; }
		public Maturity Maturity { get; set; }
		public Underlying Underlying { get; set; }
	}
}
