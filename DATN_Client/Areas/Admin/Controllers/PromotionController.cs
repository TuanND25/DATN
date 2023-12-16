using DATN_Client.Areas.Customer.Component;
using Microsoft.AspNetCore.Mvc;

namespace DATN_Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PromotionController : Controller
    {
        public static Guid _idPromotion { get; set; }
        [Route("promotion-management")]
        public IActionResult Index()
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
        [Route("promotion-management/add")]
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
        [Route("promotion-management/update")]
        public IActionResult Update(Guid id)
        {
            if (Login.Roleuser == "Admin")
            {
                _idPromotion = id;
                return View();
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
