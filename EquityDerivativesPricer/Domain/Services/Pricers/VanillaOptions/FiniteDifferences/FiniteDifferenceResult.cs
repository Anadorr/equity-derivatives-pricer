namespace EquityDerivativesPricer.Domain.Services.Pricers.VanillaOptions.FiniteDifferences
{
	public class FiniteDifferenceResult
	{
		public double[,] OptionPrices { get; set; }
		public int NumberOfTimeSteps { get; set; }
		public int NumberOfSpaceSteps { get; set; }
	}
}
