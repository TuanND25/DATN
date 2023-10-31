using Microsoft.AspNetCore.Mvc;

namespace DATN_Client.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ShowInfoUser : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
