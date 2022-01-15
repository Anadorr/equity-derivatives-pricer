namespace EquityDerivativesPricer.Domain.Models
{
	public class PricingResult
	{
		public double PresentValue { get; set; }
		public double? Delta { get; set; }
		public double? Gamma { get; set; }
		public double? Vega { get; set; }
		public double? Theta { get; set; }
	}
}
