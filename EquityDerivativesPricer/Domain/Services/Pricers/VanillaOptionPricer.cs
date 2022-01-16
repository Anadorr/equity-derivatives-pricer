using EquityDerivativesPricer.Domain.Models;

namespace EquityDerivativesPricer.Domain.Services.Pricers
{
	public class VanillaOptionPricer : IPricer<VanillaOption>
	{
		private readonly IVanillaOptionAnalyticPricer _vanillaOptionAnalyticPricer;
		private readonly IVanillaOptionBinomialPricer _vanillaOptionBinomialPricer;

		public VanillaOptionPricer(
			IVanillaOptionAnalyticPricer vanillaOptionAnalyticPricer,
			IVanillaOptionBinomialPricer vanillaOptionBinomialPricer)
		{
			_vanillaOptionAnalyticPricer = vanillaOptionAnalyticPricer;
			_vanillaOptionBinomialPricer = vanillaOptionBinomialPricer;
		}

		public PricingResult Price(PricingConfiguration pricingConfiguration, VanillaOption priceable)
		{
			return pricingConfiguration.NumericalMethod switch
			{
				NumericalMethod.Analytic => _vanillaOptionAnalyticPricer.Price(pricingConfiguration, priceable),
				NumericalMethod.BinomialTree => _vanillaOptionBinomialPricer.Price(pricingConfiguration, priceable),
				NumericalMethod.FiniteDifferences => throw new NotImplementedException(),
				NumericalMethod.MonteCarlo => throw new NotImplementedException(),
				_ => throw new NotImplementedException(),
			};
		}
	}
}
