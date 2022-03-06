using System.Runtime.Serialization;

namespace EquityDerivativesPricer.Domain.Models
{
	public enum OptionStyle
	{
		[EnumMember(Value = "American")]
		AMERICAN,
		[EnumMember(Value = "European")]
		EUROPEAN
	}
}
