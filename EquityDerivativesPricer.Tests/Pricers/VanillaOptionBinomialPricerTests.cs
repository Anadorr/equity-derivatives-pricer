using EquityDerivativesPricer.Domain.Models;
using EquityDerivativesPricer.Domain.Services.Calculators;
using EquityDerivativesPricer.Domain.Services.Pricers;
using Moq;
using NUnit.Framework;

namespace EquityDerivativesPricer.Tests.VanillaOptions
{
	[TestFixture]
	public class VanillaOptionBinomialPricerTests
	{
		private IVanillaOptionBinomialPricer _pricer;

		private Mock<IInterestRateCalculator> _interestRateCalculator;

		[OneTimeSetUp]
		public void Init()
		{
			_interestRateCalculator = new Mock<IInterestRateCalculator>();

			_pricer = new VanillaOptionBinomialPricer(_interestRateCalculator.Object);
		}

		[Test]
		public void PriceAtTheMoneyAmericanCallOption_Ok()
		{
			_interestRateCalculator.Reset();

			// arrange
			var underlying = new Underlying
			{
				Name = "AAPL",
				SpotPrice = 100.0,
				AnnualDividendYield = 0.01,
				AnnualVolatility = 0.2,
			};

			var vanillaOption = new VanillaOption
			{
				OptionStyle = OptionStyle.AMERICAN,
				OptionType = OptionType.CALL,
				Strike = 100.0,
				Maturity = Maturity.Parse("1Y"),
				Underlying = underlying
			};

			_interestRateCalculator.Setup(x => x.GetAnnualRiskFreeRate()).Returns(0);

			var pricingConfig = new PricingConfiguration
			{
				NumericalMethod = NumericalMethod.BinomialTree,
				IsCalculationWithGreeks = true
			};

			// act
			var pricingResult = _pricer.Price(pricingConfig, vanillaOption);

			// assert
			Assert.AreEqual(7.5106, pricingResult.PresentValue, 0.0001);

			_interestRateCalculator.Verify(x => x.GetAnnualRiskFreeRate(), Times.Once);
		}

		[Test]
		public void PriceInTheMoneyAmericanCallOption_Ok()
		{
			_interestRateCalculator.Reset();

			// arrange
			var underlying = new Underlying
			{
				Name = "AAPL",
				SpotPrice = 110.0,
				AnnualDividendYield = 0.01,
				AnnualVolatility = 0.2,
			};

			var vanillaOption = new VanillaOption
			{
				OptionStyle = OptionStyle.AMERICAN,
				OptionType = OptionType.CALL,
				Strike = 100.0,
				Maturity = Maturity.Parse("1Y"),
				Underlying = underlying
			};

			_interestRateCalculator.Setup(x => x.GetAnnualRiskFreeRate()).Returns(0);

			var pricingConfig = new PricingConfiguration
			{
				NumericalMethod = NumericalMethod.BinomialTree,
				IsCalculationWithGreeks = true
			};

			// act
			var pricingResult = _pricer.Price(pricingConfig, vanillaOption);

			// assert
			Assert.AreEqual(13.6906, pricingResult.PresentValue, 0.0001);

			_interestRateCalculator.Verify(x => x.GetAnnualRiskFreeRate(), Times.Once);
		}

		[Test]
		public void PriceOutTheMoneyAmericanCallOption_Ok()
		{
			_interestRateCalculator.Reset();

			// arrange
			var underlying = new Underlying
			{
				Name = "AAPL",
				SpotPrice = 90.0,
				AnnualDividendYield = 0.01,
				AnnualVolatility = 0.2,
			};

			var vanillaOption = new VanillaOption
			{
				OptionStyle = OptionStyle.AMERICAN,
				OptionType = OptionType.CALL,
				Strike = 100.0,
				Maturity = Maturity.Parse("1Y"),
				Underlying = underlying
			};

			_interestRateCalculator.Setup(x => x.GetAnnualRiskFreeRate()).Returns(0);

			var pricingConfig = new PricingConfiguration
			{
				NumericalMethod = NumericalMethod.BinomialTree,
				IsCalculationWithGreeks = true
			};

			// act
			var pricingResult = _pricer.Price(pricingConfig, vanillaOption);

			// assert
			Assert.AreEqual(3.3232, pricingResult.PresentValue, 0.0001);

			_interestRateCalculator.Verify(x => x.GetAnnualRiskFreeRate(), Times.Once);
		}

		[Test]
		public void PriceHullAmericanCallOption_Ok()
		{
			// Options, Futures and Other Derivatives, Hull, p. 312
			_interestRateCalculator.Reset();

			// arrange
			var underlying = new Underlying
			{
				Name = "AAPL",
				SpotPrice = 50.0,
				AnnualDividendYield = 0,
				AnnualVolatility = 0.3,
			};

			var vanillaOption = new VanillaOption
			{
				OptionStyle = OptionStyle.AMERICAN,
				OptionType = OptionType.PUT,
				Strike = 52.0,
				Maturity = Maturity.Parse("2Y"),
				Underlying = underlying
			};

			_interestRateCalculator.Setup(x => x.GetAnnualRiskFreeRate()).Returns(0.05);

			var pricingConfig = new PricingConfiguration
			{
				NumericalMethod = NumericalMethod.BinomialTree,
				IsCalculationWithGreeks = true
			};

			// act
			var pricingResult = _pricer.Price(pricingConfig, vanillaOption);

			// assert
			Assert.AreEqual(7.4709, pricingResult.PresentValue, 0.0001);

			_interestRateCalculator.Verify(x => x.GetAnnualRiskFreeRate(), Times.Once);
		}

		[Test]
		public void PriceHullEuropeanCallOption_Ok()
		{
			// Options, Futures and Other Derivatives, Hull, p. 312
			_interestRateCalculator.Reset();

			// arrange
			var underlying = new Underlying
			{
				Name = "AAPL",
				SpotPrice = 50.0,
				AnnualDividendYield = 0,
				AnnualVolatility = 0.3,
			};

			var vanillaOption = new VanillaOption
			{
				OptionStyle = OptionStyle.EUROPEAN,
				OptionType = OptionType.PUT,
				Strike = 52.0,
				Maturity = Maturity.Parse("2Y"),
				Underlying = underlying
			};

			_interestRateCalculator.Setup(x => x.GetAnnualRiskFreeRate()).Returns(0.05);

			var pricingConfig = new PricingConfiguration
			{
				NumericalMethod = NumericalMethod.BinomialTree,
				IsCalculationWithGreeks = true
			};

			// act
			var pricingResult = _pricer.Price(pricingConfig, vanillaOption);

			// assert
			Assert.AreEqual(6.7568, pricingResult.PresentValue, 0.0001);

			_interestRateCalculator.Verify(x => x.GetAnnualRiskFreeRate(), Times.Once);
		}

		[Test]
		public void PriceHullEuropeanCallOption_WithContinuousDividendYield_Ok()
		{
			// Options, Futures and Other Derivatives, Hull, p. 313
			_interestRateCalculator.Reset();

			// arrange
			var underlying = new Underlying
			{
				Name = "STOCK_INDEX",
				SpotPrice = 810.0,
				AnnualDividendYield = 0.02,
				AnnualVolatility = 0.2,
			};

			var vanillaOption = new VanillaOption
			{
				OptionStyle = OptionStyle.EUROPEAN,
				OptionType = OptionType.CALL,
				Strike = 800.0,
				Maturity = Maturity.Parse("6M"),
				Underlying = underlying
			};

			_interestRateCalculator.Setup(x => x.GetAnnualRiskFreeRate()).Returns(0.05);

			var pricingConfig = new PricingConfiguration
			{
				NumericalMethod = NumericalMethod.BinomialTree,
				IsCalculationWithGreeks = true
			};

			// act
			var pricingResult = _pricer.Price(pricingConfig, vanillaOption);

			// assert
			Assert.AreEqual(55.8754, pricingResult.PresentValue, 0.0001);

			_interestRateCalculator.Verify(x => x.GetAnnualRiskFreeRate(), Times.Once);
		}

		[Test]
		public void PriceHullAmericanPutOption_Ok()
		{
			// Options, Futures and Other Derivatives, Hull, p. 475
			_interestRateCalculator.Reset();

			// arrange
			var underlying = new Underlying
			{
				Name = "AAPL",
				SpotPrice = 50.0,
				AnnualDividendYield = 0,
				AnnualVolatility = 0.4,
			};

			var vanillaOption = new VanillaOption
			{
				OptionStyle = OptionStyle.AMERICAN,
				OptionType = OptionType.PUT,
				Strike = 50.0,
				Maturity = Maturity.Parse("5M"),
				Underlying = underlying
			};

			_interestRateCalculator.Setup(x => x.GetAnnualRiskFreeRate()).Returns(0.1);

			var pricingConfig = new PricingConfiguration
			{
				NumericalMethod = NumericalMethod.BinomialTree,
				IsCalculationWithGreeks = true
			};

			// act
			var pricingResult = _pricer.Price(pricingConfig, vanillaOption);

			// assert
			Assert.AreEqual(4.2590, pricingResult.PresentValue, 0.0001);

			_interestRateCalculator.Verify(x => x.GetAnnualRiskFreeRate(), Times.Once);
		}
	}
}