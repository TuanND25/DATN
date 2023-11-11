using Microsoft.AspNetCore.Mvc;

namespace DATN_Client.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class UserManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Address()
        {
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }
        public IActionResult BillByUser()
        {
            return View();
        }
        public IActionResult BilllItemByUser()
        {
            return View();
        }
    }
}
