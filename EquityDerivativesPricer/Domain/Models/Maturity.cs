using System.Text.Json.Serialization;

namespace EquityDerivativesPricer.Domain.Models
{
	public class Maturity
	{
		public int Length { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public Period Period { get; set; }

		public Maturity(
			int length,
			Period period)
		{
			Length = length;
			Period = period;
		}

		public static Maturity Parse(string maturity)
		{
			if (string.IsNullOrEmpty(maturity))
			{
				throw new ArgumentNullException(nameof(maturity));
			}

			if (!Maturity.TryParse(maturity, out var parsedMaturity))
			{
				throw new InvalidOperationException($"Cannot parse \"{maturity}\" into a Maturity object.");
			}

			return parsedMaturity!;
		}

		public static bool TryParse(string maturity, out Maturity? parsedMaturity)
		{
			var length = new string(maturity.Where(c => char.IsDigit(c)).ToArray());
			var tenor = new string(maturity.Where(c => !char.IsDigit(c)).ToArray())
				.ToUpper();

			if (tenor.Equals("ON"))
			{
				parsedMaturity = new Maturity(1, Period.D);
				return true;
			}

			if (length.Equals("12") && tenor.Equals("M"))
			{
				parsedMaturity = new Maturity(1, Period.Y);
				return true;
			}

			if (int.TryParse(length, out var parsedLength)
				&& Enum.TryParse(tenor, out Period parsedTenor))
			{
				parsedMaturity = new Maturity(parsedLength, parsedTenor);
				return true;
			}

			parsedMaturity = null;
			return false;
		}

		public Maturity ToDays()
		{
			return Period switch
			{
				Period.D => this,
				Period.W => new Maturity(Length * 7, Period.D),
				Period.M => new Maturity(Length * 30, Period.D),
				Period.Y => new Maturity(Length * 365, Period.D),
				_ => throw new NotImplementedException(),
			};
		}

		public double ToYearFraction()
		{
			return Period switch
			{
				Period.D => Length / 365.0,
				Period.W => Length * 7 / 365.0,
				Period.M => Length * 30 / 365.0,
				Period.Y => Length,
				_ => throw new NotImplementedException(),
			};
		}
	}
}
