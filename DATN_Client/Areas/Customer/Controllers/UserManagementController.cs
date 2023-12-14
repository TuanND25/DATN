using Microsoft.AspNetCore.Mvc;

namespace DATN_Client.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class UserManagementController : Controller
    {
        public static Guid _billId { get; set; }
        [Route("account/info")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("account/user-address")]
        public IActionResult Address()
        {
            return View();
        }
        [Route("account/change-password")]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [Route("account/bill-history")]
        public IActionResult BillByUser()
        {
            return View();
        }
		[Route("account/bill-history/bill-detail")]
		public IActionResult BilllItemByUser(Guid billid)
        {
            _billId = billid;
            return View();
        }
    }
}
