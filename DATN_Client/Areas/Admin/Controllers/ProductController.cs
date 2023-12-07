using Microsoft.AspNetCore.Mvc;

namespace DATN_Client.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		public static Guid _productID { get;set; }
		public IActionResult Index()
		{
			return View();
		}
		[Route("product/{Id}")]
		public IActionResult DetailProduct(Guid Id)
		{
			_productID = Id;
            return View();
		}
	}
}
