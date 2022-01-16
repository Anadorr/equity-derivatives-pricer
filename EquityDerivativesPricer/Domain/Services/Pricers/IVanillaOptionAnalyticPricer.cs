using EquityDerivativesPricer.Domain.Models;

namespace EquityDerivativesPricer.Domain.Services.Pricers
{
	public interface IVanillaOptionAnalyticPricer
	{
		PricingResult Price(PricingConfiguration configuration, VanillaOption vanillaOption);
	}
}
