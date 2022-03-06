namespace EquityDerivativesPricer.Domain.Models
{
	public interface IPricingRequest<T> where T : IPriceable
	{
		public T Priceable { get; set; }
		public PricingConfiguration PricingConfiguration { get; set; }
	}
}