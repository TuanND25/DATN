using Microsoft.AspNetCore.Mvc;

namespace DATN_Client.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class AboutController : Controller
	{
		[Route("about")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
