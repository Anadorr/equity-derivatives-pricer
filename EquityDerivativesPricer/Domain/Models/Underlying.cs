namespace EquityDerivativesPricer.Domain.Models
{
	/// <summary>
	/// A class to model an equity stock.
	/// </summary>
	public class Underlying
	{
		public AssetClass AssetClass => AssetClass.Equity;
		public string Name { get; set; }
		public double AnnualVolatility { get; set; }
		public double SpotPrice { get; set; }
		public double AnnualDividendYield { get; set; }
	}
}
