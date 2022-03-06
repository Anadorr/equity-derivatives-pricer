using System.Runtime.Serialization;

namespace EquityDerivativesPricer.Domain.Models
{
	public enum NumericalMethod
	{
		[EnumMember(Value = "Analytic")]
		Analytic,
		[EnumMember(Value = "BinomialTree")]
		BinomialTree,
		[EnumMember(Value = "FiniteDifferences")]
		FiniteDifferences,
		[EnumMember(Value = "MonteCarlo")]
		MonteCarlo
	}
}
