using EquityDerivativesPricer.Domain.Models;

namespace EquityDerivativesPricer.Domain.Services.Pricers
{
	public interface IVanillaOptionFiniteDifferencePricer
	{
		PricingResult Price(PricingConfiguration configuration, VanillaOption vanillaOption);
	}
}
