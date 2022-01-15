using EquityDerivativesPricer.Domain.Models;

namespace EquityDerivativesPricer.Domain.Services.Pricers
{
	public interface IPricer<T> where T : IPriceable
	{
		PricingResult Price(PricingConfiguration pricingConfiguration, T priceable);
	}
}
