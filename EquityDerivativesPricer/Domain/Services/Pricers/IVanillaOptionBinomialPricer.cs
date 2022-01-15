using EquityDerivativesPricer.Domain.Models;

namespace EquityDerivativesPricer.Domain.Services.Pricers
{
	public interface IVanillaOptionBinomialPricer
	{
		PricingResult Price(PricingConfiguration config, VanillaOption option);
	}
}
