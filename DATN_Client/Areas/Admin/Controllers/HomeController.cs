using DATN_Client.Areas.Customer.Component;
using Microsoft.AspNetCore.Mvc;

namespace DATN_Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
		
        public IActionResult Index()
        {
            return View();
            if (Login.Roleuser == "Admin" || Login.Roleuser== "Staff")
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
