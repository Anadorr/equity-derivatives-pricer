namespace EquityDerivativesPricer.Domain.Services.Pricers.VanillaOptions.Binomial
{
	public class BinomialTreeResult
	{
		public double[,] OptionPrices { get; set; }
		public double DeltaT { get; set; }
		public double U { get; set; }
		public double D { get; set; }
		public int NumberOfTimeSteps { get; set; }
	}
}
