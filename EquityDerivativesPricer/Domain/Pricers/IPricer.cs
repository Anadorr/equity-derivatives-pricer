namespace EquityDerivativesPricer.Domain.Pricers
{
	public interface IPricer<T> where T : IPriceable
	{
		double PresentValue(NumericalMethod numericalMethod, T priceable, Stock underlyingStock);
	}
}
