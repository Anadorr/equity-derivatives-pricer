using EquityDerivativesPricer.Domain.Calculators;
using EquityDerivativesPricer.Domain.SharedKernel;
using MathNet.Numerics.Distributions;

namespace EquityDerivativesPricer.Domain.Pricers
{
	public class VanillaOptionPricer : IPricer<VanillaOption>
	{
		private readonly IInterestRateCalculator _interestRateCalculator;

		public VanillaOptionPricer(IInterestRateCalculator interestRateCalculator)
		{
			_interestRateCalculator = interestRateCalculator;
		}

		public double PresentValue(
			NumericalMethod numericalMethod,
			VanillaOption option,
			Stock underlyingStock)
		{
			if (option.OptionStyle == OptionStyle.EUROPEAN)
			{
				return numericalMethod switch
				{
					NumericalMethod.Analytic => GetAnalyticEuropeanOptionPresentValue(option.OptionType,
						option.Maturity,
						option.Strike,
						underlyingStock),
					_ => throw new NotImplementedException()
				};
			}

			// American option
			return numericalMethod switch
			{
				NumericalMethod.Analytic => throw new InvalidOperationException($"The {NumericalMethod.Analytic} numerical method is not available for American vanilla options."),
				NumericalMethod.BinomialTree => GetBinomialTreeAmericanOptionPresentValue(option.OptionType,
					option.Maturity,
					option.Strike,
					underlyingStock),
				_ => throw new NotImplementedException(),
			};
		}

		private double GetAnalyticEuropeanOptionPresentValue(
			OptionType optionType,
			Maturity maturity,
			double strike,
			Stock underlyingStock)
		{
			var riskFreeInterestRate = _interestRateCalculator.GetAnnualRiskFreeRate();
			var annualVolatility = underlyingStock.AnnualVolatility;
			var spotPrice = underlyingStock.SpotPrice;
			var annualDividendYield = underlyingStock.AnnualDividendYield;
			// We assume for now that we price the option at t = 0, so that timeToMaturity = maturity of the option.
			var timeToMaturity = maturity.ToYearFraction();

			var multiplier = optionType switch
			{
				OptionType.CALL => 1,
				OptionType.PUT => -1,
				_ => throw new NotImplementedException(),
			};

			var d_1 = (Math.Log(spotPrice / strike) + (riskFreeInterestRate - annualDividendYield + annualVolatility * annualVolatility / 2) * timeToMaturity) / (annualVolatility * Math.Sqrt(timeToMaturity));
			var d_2 = d_1 - annualVolatility * Math.Sqrt(timeToMaturity);
			return multiplier * (spotPrice * Math.Exp(-annualDividendYield * timeToMaturity) * Normal.CDF(0, 1, multiplier * d_1) - strike * Normal.CDF(0, 1, multiplier * d_2) * Math.Exp(-riskFreeInterestRate * timeToMaturity));
		}

		private double GetBinomialTreeAmericanOptionPresentValue(
			OptionType optionType,
			Maturity maturity,
			double strike,
			Stock underlyingStock)
		{
			var multiplier = optionType switch
			{
				OptionType.CALL => 1,
				OptionType.PUT => -1,
				_ => throw new NotImplementedException(),
			};

			var riskFreeInterestRate = _interestRateCalculator.GetAnnualRiskFreeRate();
			var annualVolatility = underlyingStock.AnnualVolatility;
			var timeToMaturity = maturity.ToYearFraction();
			var annualDividendYield = underlyingStock.AnnualDividendYield;
			var spotPrice = underlyingStock.SpotPrice;

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

			return p[0];
		}
	}
}
