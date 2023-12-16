using Microsoft.AspNetCore.Mvc;

namespace DATN_Client.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class ForgetPasswordController : Controller
	{
		[Route("forget-password")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
