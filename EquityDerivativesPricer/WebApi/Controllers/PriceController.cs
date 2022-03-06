using EquityDerivativesPricer.Domain.Models;
using EquityDerivativesPricer.Domain.Services.Pricers.VanillaOptions;
using Microsoft.AspNetCore.Mvc;

namespace EquityDerivativesPricer.WebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PriceController : ControllerBase
	{
		private readonly ILogger<PriceController> _logger;
		private readonly IVanillaOptionPricerFactory _vanillaOptionPricerFactory;

		public PriceController(
			ILogger<PriceController> logger,
			IVanillaOptionPricerFactory vanillaOptionPricerFactory)
		{
			_logger = logger;
			_vanillaOptionPricerFactory = vanillaOptionPricerFactory;
		}

		[HttpPost("VanillaOption", Name = nameof(PriceVanillaOption))]
		public ActionResult<PricingResult> PriceVanillaOption([FromBody] VanillaOptionPricingRequest pricingRequest)
		{
			_logger.LogInformation("Pricing vanilla option started.");

			var pricer = _vanillaOptionPricerFactory.CreateVanillaOptionPricer(pricingRequest.PricingConfiguration.NumericalMethod);
			var pricingResult = pricer.Price(pricingRequest.PricingConfiguration, pricingRequest.Priceable);

			_logger.LogInformation("Pricing vanilla option finished.");

			return pricingResult;
		}
	}
}
