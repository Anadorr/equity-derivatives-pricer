using EquityDerivativesPricer.Domain.Calculators;
using EquityDerivativesPricer.Domain.Models;

namespace EquityDerivativesPricer.Domain.Pricers
{
	public class VanillaOptionBinomialPricer : IVanillaOptionBinomialPricer
	{
		private readonly IInterestRateCalculator _interestRateCalculator;

		public VanillaOptionBinomialPricer(IInterestRateCalculator interestRateCalculator)
		{
			_interestRateCalculator = interestRateCalculator;
		}

		public PricingResult Price(PricingConfiguration config, VanillaOption option)
		{
			return option.OptionStyle == OptionStyle.EUROPEAN
				? throw new InvalidOperationException(
					$"The \"{NumericalMethod.Analytic}\" numerical method is not available for American vanilla options.")

				: PriceAmericanOption(config, option);
		}

		public PricingResult PriceAmericanOption(PricingConfiguration config, VanillaOption option)
		{
			var pricingResult = new PricingResult { };

			var multiplier = option.OptionType switch
			{
				OptionType.CALL => 1,
				OptionType.PUT => -1,
				_ => throw new NotImplementedException()
			};

			var riskFreeInterestRate = _interestRateCalculator.GetAnnualRiskFreeRate();
			var annualVolatility = option.Underlying.AnnualVolatility;
			var annualDividendYield = option.Underlying.AnnualDividendYield;
			var spotPrice = option.Underlying.SpotPrice;

			var strike = option.Strike;
			var timeToMaturity = option.Maturity.ToYearFraction();

			var n = 150;
			var deltaT = timeToMaturity / n;
			var u = Math.Exp(annualVolatility * Math.Sqrt(deltaT));
			var d = 1 / u;

			var q = (Math.Exp((riskFreeInterestRate - annualDividendYield) * deltaT) - d) / (u - d);

			// Stock prices
			var s = new double[n + 1];

			// Option prices
			var p = new double[n + 1];

			// Initialize prices at maturity (= payoffs)
			for (var j = 0; j < n + 1; j++)
			{
				s[j] = spotPrice * Math.Pow(u, j) * Math.Pow(d, n - j);
				p[j] = Math.Max(multiplier * (s[j] - strike), 0);
			}

			// Backward recursion through the tree
			for (var i = n - 1; i >= 0; i--)
			{
				for (var j = 0; j < i + 1; j++)
				{
					p[j] = (q * p[j + 1] + (1 - q) * p[j]) * Math.Exp(-riskFreeInterestRate * deltaT);
					p[j] = Math.Max(multiplier * (spotPrice * Math.Pow(u, j) * Math.Pow(d, i - j) - strike), p[j]);
				}
			}

			pricingResult.PresentValue = p[0];

			// TODO: Implement greeks calculation for american options

			return pricingResult;
		}
	}
}
