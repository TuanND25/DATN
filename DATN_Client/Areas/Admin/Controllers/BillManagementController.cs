using DATN_Client.Areas.Customer.Component;
using Microsoft.AspNetCore.Mvc;

namespace DATN_Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BillManagementController : Controller
    {
        public static Guid _billId { get; set; }
        [Route("bill-management")]
        public IActionResult Index()
        {
            //return View();
            if (Login.Roleuser == "Admin" || Login.Roleuser == "Staff")
            {
                return View();
            }
            else
            {
                return Unauthorized();
            }
        }
        [Route("bill-management/bill-detail")]
        public IActionResult Details(Guid BillId)
        {
            _billId = BillId;
            //return View();
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
