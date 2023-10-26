using DATN_Client.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DATN_Client.Controllers
{
	public class HomeController : Controller
	{

		public HomeController()
		{

		}

		public IActionResult Index()
		{
			return RedirectToAction("ShowProduct", "BanOnline", new { Area = "Customer" });
		}

		public IActionResult Privacy()
		{
			return View();
		}

        public IActionResult LogOut()
		{
			return RedirectToAction("Index");
		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}