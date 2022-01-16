namespace EquityDerivativesPricer.Domain.Services.Calculators
{
	public interface IInterestRateCalculator
	{
		double GetAnnualRiskFreeRate();
	}
}
