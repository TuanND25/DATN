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
			return View();
			if (Login.Roleuser == "Admin" )
			{
				return View();
			}
			else
			{
				return Unauthorized();
			}
		}
		[Route("product/{Id}")]
		public IActionResult DetailProduct(Guid Id)
		{
			_productID = Id;
			return View();
			if (Login.Roleuser == "Admin" )
			{
				_productID = Id;
				return View();
			}
			else
			{
				return Unauthorized();
			}
			
           
		}
	}
}
