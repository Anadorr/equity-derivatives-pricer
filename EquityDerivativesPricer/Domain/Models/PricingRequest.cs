namespace EquityDerivativesPricer.Domain.Models
{
	public class VanillaOptionPricingRequest : IPricingRequest<VanillaOption>
	{
		public VanillaOption Priceable { get; set; } = new VanillaOption();
		public PricingConfiguration PricingConfiguration { get; set; } = new PricingConfiguration();
	}
}
