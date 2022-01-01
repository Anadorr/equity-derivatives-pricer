namespace EquityDerivativesPricer.Domain.Calculators
{
	public interface IInterestRateCalculator
	{
		double GetAnnualRiskFreeRate();
	}
}
