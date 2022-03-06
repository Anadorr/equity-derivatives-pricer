using EquityDerivativesPricer.Domain.Models;

namespace EquityDerivativesPricer.Domain.Services.Pricers.VanillaOptions
{
	public interface IVanillaOptionPricerFactory
	{
		IPricer<VanillaOption> CreateVanillaOptionPricer(NumericalMethod numericalMethod);
	}
}