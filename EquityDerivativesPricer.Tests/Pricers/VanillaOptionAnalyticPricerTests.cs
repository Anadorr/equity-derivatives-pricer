﻿using EquityDerivativesPricer.Domain;
using EquityDerivativesPricer.Domain.Calculators;
using EquityDerivativesPricer.Domain.Models;
using EquityDerivativesPricer.Domain.Pricers;
using EquityDerivativesPricer.Domain.SharedKernel;
using Moq;
using NUnit.Framework;

namespace EquityDerivativesPricer.Tests.VanillaOptions
{
	[TestFixture]
	public class VanillaOptionAnalyticPricerTests
	{
		private IVanillaOptionAnalyticPricer _pricer;

		private Mock<IInterestRateCalculator> _interestRateCalculator;

		[OneTimeSetUp]
		public void Init()
		{
			_interestRateCalculator = new Mock<IInterestRateCalculator>();

			_pricer = new VanillaOptionAnalyticPricer(_interestRateCalculator.Object);
		}

		[Test]
		public void PriceAtTheMoneyEuropeanCallOption_Ok()
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
				OptionStyle = OptionStyle.EUROPEAN,
				OptionType = OptionType.CALL,
				Strike = 100.0,
				Maturity = Maturity.Parse("1Y"),
				Underlying = underlying
			};

			_interestRateCalculator.Setup(x => x.GetAnnualRiskFreeRate()).Returns(0);

			var pricingConfig = new PricingConfiguration
			{
				NumericalMethod = NumericalMethod.Analytic,
				IsCalculationWithGreeks = true
			};

			// act
			var pricingResult = _pricer.Price(pricingConfig, vanillaOption);

			// assert
			Assert.AreEqual(7.4383, pricingResult.PresentValue, 0.0001);
			Assert.AreEqual(0.5147, pricingResult.Delta, 0.0001);
			Assert.AreEqual(0.0197, pricingResult.Gamma, 0.0001);
			Assert.AreEqual(39.4479, pricingResult.Vega, 0.0001);
			Assert.AreEqual(-3.4300, pricingResult.Theta, 0.0001);

			_interestRateCalculator.Verify(x => x.GetAnnualRiskFreeRate(), Times.Once);
		}

		[Test]
		public void PriceInTheMoneyEuropeanCallOption_Ok()
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
				OptionStyle = OptionStyle.EUROPEAN,
				OptionType = OptionType.CALL,
				Strike = 100.0,
				Maturity = Maturity.Parse("1Y"),
				Underlying = underlying
			};

			_interestRateCalculator.Setup(x => x.GetAnnualRiskFreeRate()).Returns(0);

			var pricingConfig = new PricingConfiguration
			{
				NumericalMethod = NumericalMethod.Analytic,
				IsCalculationWithGreeks = true
			};

			// act
			var pricingResult = _pricer.Price(pricingConfig, vanillaOption);

			// assert
			Assert.AreEqual(13.5156, pricingResult.PresentValue, 0.0001);
			Assert.AreEqual(0.6938, pricingResult.Delta, 0.0001);
			Assert.AreEqual(0.0156, pricingResult.Gamma, 0.0001);
			Assert.AreEqual(37.8229, pricingResult.Vega, 0.0001);
			Assert.AreEqual(-3.0191, pricingResult.Theta, 0.0001);

			_interestRateCalculator.Verify(x => x.GetAnnualRiskFreeRate(), Times.Once);
		}

		[Test]
		public void PriceOutTheMoneyEuropeanCallOption_Ok()
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
				OptionStyle = OptionStyle.EUROPEAN,
				OptionType = OptionType.CALL,
				Strike = 100.0,
				Maturity = Maturity.Parse("1Y"),
				Underlying = underlying
			};

			_interestRateCalculator.Setup(x => x.GetAnnualRiskFreeRate()).Returns(0);

			var pricingConfig = new PricingConfiguration
			{
				NumericalMethod = NumericalMethod.Analytic,
				IsCalculationWithGreeks = true
			};

			// act
			var pricingResult = _pricer.Price(pricingConfig, vanillaOption);

			// assert
			Assert.AreEqual(3.2974, pricingResult.PresentValue, 0.0001);
			Assert.AreEqual(0.3136, pricingResult.Delta, 0.0001);
			Assert.AreEqual(0.0196, pricingResult.Gamma, 0.0001);
			Assert.AreEqual(31.7280, pricingResult.Vega, 0.0001);
			Assert.AreEqual(-2.8906, pricingResult.Theta, 0.0001);

			_interestRateCalculator.Verify(x => x.GetAnnualRiskFreeRate(), Times.Once);
		}
	}
}
