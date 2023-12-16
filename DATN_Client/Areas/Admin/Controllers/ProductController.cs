using DATN_Client.Areas.Customer.Component;
using Microsoft.AspNetCore.Mvc;

namespace DATN_Client.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		public static Guid _productID { get;set; }
		[Route("product-manager")]
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
		[Route("product-manager/product-detail")]
		public IActionResult DetailProduct(Guid id)
		{
			_productID = id;
			if (Login.Roleuser == "Admin" )
			{
				_productID = id;
				return View();
			}
			else
			{
				return Unauthorized();
			}
			
           
		}
	}
}
