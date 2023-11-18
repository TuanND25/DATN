using Microsoft.AspNetCore.Mvc;

namespace DATN_Client.Areas.Admin.Controllers
{
	public class ColorController : Controller
	{
		[Area("Admin")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
