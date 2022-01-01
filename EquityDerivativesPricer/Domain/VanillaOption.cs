using EquityDerivativesPricer.Domain.SharedKernel;

namespace EquityDerivativesPricer.Domain
{
	public class VanillaOption : IPriceable
	{
		public OptionStyle OptionStyle { get; set; }
		public OptionType OptionType { get; set; }
		public double Strike { get; set; }
		public Maturity Maturity { get; set; }

		public VanillaOption(
			OptionStyle optionStyle,
			OptionType optionType,
			double strike,
			Maturity maturity)
		{
			OptionStyle = optionStyle;
			OptionType = optionType;
			Strike = strike;
			Maturity = maturity;
		}
	}
}
