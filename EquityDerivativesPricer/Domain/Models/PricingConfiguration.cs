namespace EquityDerivativesPricer.Domain.Models
{
	public class PricingConfiguration
	{
		public NumericalMethod? NumericalMethod { get; set; }
		public bool IsCalculationWithGreeks { get; set; } = true;
	}
}
