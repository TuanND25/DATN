using Microsoft.AspNetCore.Mvc;

namespace DATN_Client.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductItemController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
