using DATN_Client.Areas.Customer.Component;
using Microsoft.AspNetCore.Mvc;

namespace DATN_Client.Areas.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
			if (Login.Roleuser == "Admin" || Login.Roleuser == "Staff")
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
