using EquityDerivativesPricer.Domain;
using EquityDerivativesPricer.Domain.Calculators;
using EquityDerivativesPricer.Domain.Pricers;
using EquityDerivativesPricer.Domain.SharedKernel;
using Moq;
using NUnit.Framework;

namespace EquityDerivativesPricer.Tests.VanillaOptions
{
	[TestFixture]
	public class AmericanOptionPricerTests
	{
		private IPricer<VanillaOption> _pricer;

		private Mock<IInterestRateCalculator> _interestRateCalculator;

		[OneTimeSetUp]
		public void Init()
		{
			_interestRateCalculator = new Mock<IInterestRateCalculator>();

			_pricer = new VanillaOptionPricer(_interestRateCalculator.Object);
		}

		[Test]
		public void PriceAtTheMoneyAmericanCallOption_BinomialTree_Ok()
		{
			_interestRateCalculator.Reset();

			// arrange
			var numericalMethod = NumericalMethod.BinomialTree;

			var optionStyle = OptionStyle.AMERICAN;
			var optionType = OptionType.CALL;
			var strike = 100.0;
			var maturity = Maturity.Parse("1Y");

			var vanillaOption = new VanillaOption(optionStyle, optionType, strike, maturity);

			var underlyingStock = new Stock
			{
				Name = "AAPL",
				SpotPrice = 100.0,
				AnnualDividendYield = 0.01,
				AnnualVolatility = 0.2,
			};

			_interestRateCalculator.Setup(x => x.GetAnnualRiskFreeRate()).Returns(0);

			// act
			var pv = _pricer.PresentValue(numericalMethod, vanillaOption, underlyingStock);

			// assert
			Assert.AreEqual(7.5040, pv, 0.0001);

			_interestRateCalculator.Verify(x => x.GetAnnualRiskFreeRate(), Times.Once);
		}

		[Test]
		public void PriceInTheMoneyAmericanCallOption_BinomialTree_Ok()
		{
			_interestRateCalculator.Reset();

			// arrange
			var numericalMethod = NumericalMethod.BinomialTree;

			var optionStyle = OptionStyle.AMERICAN;
			var optionType = OptionType.CALL;
			var strike = 100.0;
			var maturity = Maturity.Parse("1Y");

			var vanillaOption = new VanillaOption(optionStyle, optionType, strike, maturity);

			var underlyingStock = new Stock
			{
				Name = "AAPL",
				SpotPrice = 110.0,
				AnnualDividendYield = 0.01,
				AnnualVolatility = 0.2,
			};

			_interestRateCalculator.Setup(x => x.GetAnnualRiskFreeRate()).Returns(0);

			// act
			var pv = _pricer.PresentValue(numericalMethod, vanillaOption, underlyingStock);

			// assert
			Assert.AreEqual(13.6841, pv, 0.0001);

			_interestRateCalculator.Verify(x => x.GetAnnualRiskFreeRate(), Times.Once);
		}

		[Test]
		public void PriceOutTheMoneyAmericanCallOption_BinomialTree_Ok()
		{
			_interestRateCalculator.Reset();

			// arrange
			var numericalMethod = NumericalMethod.BinomialTree;

			var optionStyle = OptionStyle.AMERICAN;
			var optionType = OptionType.CALL;
			var strike = 100.0;
			var maturity = Maturity.Parse("1Y");

			var vanillaOption = new VanillaOption(optionStyle, optionType, strike, maturity);

			var underlyingStock = new Stock
			{
				Name = "AAPL",
				SpotPrice = 90.0,
				AnnualDividendYield = 0.01,
				AnnualVolatility = 0.2,
			};

			_interestRateCalculator.Setup(x => x.GetAnnualRiskFreeRate()).Returns(0);

			// act
			var pv = _pricer.PresentValue(numericalMethod, vanillaOption, underlyingStock);

			// assert
			Assert.AreEqual(3.3271, pv, 0.0001);

			_interestRateCalculator.Verify(x => x.GetAnnualRiskFreeRate(), Times.Once);
		}
	}
}
