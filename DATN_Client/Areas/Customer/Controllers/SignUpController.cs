using Microsoft.AspNetCore.Mvc;

namespace DATN_Client.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class SignUpController : Controller
    {
        [Route("signup")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
