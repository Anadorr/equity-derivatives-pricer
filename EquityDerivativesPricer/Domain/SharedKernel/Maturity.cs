using System.Text.Json.Serialization;

namespace EquityDerivativesPricer.Domain.SharedKernel
{
	public class Maturity
	{
		public int Length { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public Tenor Period { get; set; }

		public Maturity(
			int length,
			Tenor period)
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
				throw new FormatException($"Cannot parse {maturity} into a Maturity object.");
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
				parsedMaturity = new Maturity(1, Tenor.D);
				return true;
			}

			if (length.Equals("12") && tenor.Equals("M"))
			{
				parsedMaturity = new Maturity(1, Tenor.Y);
				return true;
			}

			if (int.TryParse(length, out var parsedLength)
				&& Enum.TryParse(tenor, out Tenor parsedTenor))
			{
				parsedMaturity = new Maturity(parsedLength, parsedTenor);
				return true;
			}

			parsedMaturity = null;
			return false;
		}
	}
}
