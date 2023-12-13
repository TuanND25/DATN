using DATN_Client.Areas.Customer.Component;
using Microsoft.AspNetCore.Mvc;

namespace DATN_Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BillManagementController : Controller
    {
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
        public IActionResult Details()
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
