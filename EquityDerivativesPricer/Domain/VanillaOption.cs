using EquityDerivativesPricer.Domain.SharedKernel;

namespace EquityDerivativesPricer.Domain
{
	public class VanillaOption : IPriceable
	{
		public OptionStyle OptionStyle { get; set; }
		public OptionType OptionType { get; set; }
		public double Strike { get; set; }
		public Maturity Maturity { get; set; }
		public Underlying Underlying { get; set; }
	}
}
