namespace EquityDerivativesPricer.Domain.Services.Pricers
{
	public class FiniteDifferenceResult
	{
		public double[,] OptionPrices { get; set; }
		public int NumberOfTimeSteps { get; set; }
		public int NumberOfSpaceSteps { get; set; }
	}
}
