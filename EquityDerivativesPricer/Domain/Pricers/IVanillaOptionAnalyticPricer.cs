using EquityDerivativesPricer.Domain.Models;

namespace EquityDerivativesPricer.Domain.Pricers
{
	public interface IVanillaOptionAnalyticPricer
	{
		PricingResult Price(PricingConfiguration configuration, VanillaOption vanillaOption);
	}
}
