using EquityDerivativesPricer.Domain.SharedKernel;
using NUnit.Framework;

namespace EquityDerivativesPricer.Tests.SharedKernel
{
	[TestFixture]
	public class MaturityTests
	{
		[OneTimeSetUp]
		public void Setup()
		{
		}

		[TestCase("ON", 1, Period.D)]
		[TestCase("1D", 1, Period.D)]
		[TestCase("1W", 1, Period.W)]
		[TestCase("1M", 1, Period.M)]
		[TestCase("12M", 1, Period.Y)]
		[TestCase("1Y", 1, Period.Y)]
		[TestCase("1y", 1, Period.Y)]
		[TestCase("10y", 10, Period.Y)]
		public void Parse_Ok(string maturity, int length, Period period)
		{
			var parsedMaturity = Maturity.Parse(maturity);

			Assert.That(parsedMaturity, Is.Not.Null);
			Assert.That(parsedMaturity.Length, Is.EqualTo(length));
			Assert.That(parsedMaturity.Period, Is.EqualTo(period));
		}

		[Test]
		public void Parse_EmptyString_ThrowsArgumentNullException()
		{
			var maturity = "";

			Assert.That(() => Maturity.Parse(maturity), Throws.ArgumentNullException);
		}

		[Test]
		public void Parse_EmptyLength_ThrowsInvalidOperationException()
		{
			var maturity = "Y";

			Assert.That(() => Maturity.Parse(maturity), Throws.InvalidOperationException);
		}

		[Test]
		public void Parse_EmptyPeriod_ThrowsInvalidOperationException()
		{
			var maturity = "12";

			Assert.That(() => Maturity.Parse(maturity), Throws.InvalidOperationException);
		}
	}
}