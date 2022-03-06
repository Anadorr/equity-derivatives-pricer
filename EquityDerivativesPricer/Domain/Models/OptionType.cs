using System.Runtime.Serialization;

namespace EquityDerivativesPricer.Domain.Models
{
	public enum OptionType
	{
		[EnumMember(Value = "Call")]
		CALL,
		[EnumMember(Value = "Put")]
		PUT
	}
}
