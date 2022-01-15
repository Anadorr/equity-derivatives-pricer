using EquityDerivativesPricer.Domain.Models;

namespace EquityDerivativesPricer.Domain.Pricers
{
	public interface IVanillaOptionBinomialPricer
	{
		PricingResult Price(PricingConfiguration config, VanillaOption option);
	}
}
