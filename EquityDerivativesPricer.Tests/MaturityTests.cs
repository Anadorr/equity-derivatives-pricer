using EquityDerivativesPricer.Domain.SharedKernel;
using NUnit.Framework;

namespace EquityDerivativesPricer.Tests
{
	[TestFixture]
	public class MaturityTests
	{
		[SetUp]
		public void Setup()
		{
		}

		[TestCase("ON", 1, Tenor.D)]
		[TestCase("1D", 1, Tenor.D)]
		[TestCase("1W", 1, Tenor.W)]
		[TestCase("1M", 1, Tenor.M)]
		[TestCase("12M", 1, Tenor.Y)]
		[TestCase("1Y", 1, Tenor.Y)]
		[TestCase("1y", 1, Tenor.Y)]
		[TestCase("10y", 10, Tenor.Y)]
		public void Parse_Ok(string maturity, int length, Tenor period)
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