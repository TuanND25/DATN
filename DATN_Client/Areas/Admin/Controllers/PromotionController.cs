using DATN_Client.Areas.Customer.Component;
using Microsoft.AspNetCore.Mvc;

namespace DATN_Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PromotionController : Controller
    {
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
        public IActionResult Add()
        {
			if (Login.Roleuser == "Admin")
			{
				return View();
			}
			else
			{
				return Unauthorized();
			}
		}
        public IActionResult Update()
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
