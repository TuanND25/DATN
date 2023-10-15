using Microsoft.AspNetCore.Mvc;

namespace DATN_Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BillManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }
    }
}
