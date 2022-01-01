using Microsoft.AspNetCore.Mvc;

namespace EquityDerivativesPricer.Controllers
{
	public class VanillaOptionController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
