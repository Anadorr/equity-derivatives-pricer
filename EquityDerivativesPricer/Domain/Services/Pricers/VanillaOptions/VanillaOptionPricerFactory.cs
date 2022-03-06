using EquityDerivativesPricer.Domain.Models;
using EquityDerivativesPricer.Domain.Services.Pricers.VanillaOptions.Analytic;
using EquityDerivativesPricer.Domain.Services.Pricers.VanillaOptions.Binomial;
using EquityDerivativesPricer.Domain.Services.Pricers.VanillaOptions.FiniteDifferences;

namespace EquityDerivativesPricer.Domain.Services.Pricers.VanillaOptions
{
	public class VanillaOptionPricerFactory : IVanillaOptionPricerFactory
	{
		private readonly IVanillaOptionAnalyticPricer _vanillaOptionAnalyticPricer;
		private readonly IVanillaOptionBinomialPricer _vanillaOptionBinomialPricer;
		private readonly IVanillaOptionFiniteDifferencePricer _vanillaOptionFiniteDifferencePricer;

		public VanillaOptionPricerFactory(
			IVanillaOptionAnalyticPricer vanillaOptionAnalyticPricer,
			IVanillaOptionBinomialPricer vanillaOptionBinomialPricer,
			IVanillaOptionFiniteDifferencePricer vanillaOptionFiniteDifferencePricer)
		{
			_vanillaOptionAnalyticPricer = vanillaOptionAnalyticPricer;
			_vanillaOptionBinomialPricer = vanillaOptionBinomialPricer;
			_vanillaOptionFiniteDifferencePricer = vanillaOptionFiniteDifferencePricer;
		}

		public IPricer<VanillaOption> CreateVanillaOptionPricer(NumericalMethod numericalMethod)
		{
			return numericalMethod switch
			{
				NumericalMethod.Analytic => _vanillaOptionAnalyticPricer,
				NumericalMethod.BinomialTree => _vanillaOptionBinomialPricer,
				NumericalMethod.FiniteDifferences => _vanillaOptionFiniteDifferencePricer,
				NumericalMethod.MonteCarlo => throw new NotImplementedException(),
				_ => throw new NotImplementedException()
			};
		}
	}
}
