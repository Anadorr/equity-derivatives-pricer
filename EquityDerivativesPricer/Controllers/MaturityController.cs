using EquityDerivativesPricer.Domain.SharedKernel;
using Microsoft.AspNetCore.Mvc;

namespace EquityDerivativesPricer.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class MaturityController : Controller
	{
		[HttpGet(Name = "GetMaturity")]
		public Maturity Get()
		{
			return Maturity.Parse("12M");
		}
	}
}
