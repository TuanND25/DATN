using DATN_Client.Areas.Customer.Component;
using Microsoft.AspNetCore.Mvc;

namespace DATN_Client.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CategoriesController : Controller
    {       
		[Route("category-management")]
		public IActionResult Index()
        {
			if (Login.Roleuser == "Admin" )
			{
				return View();
			}
			else
			{
				return Unauthorized();
			}
		}
    }
}
