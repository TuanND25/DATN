using DATN_Client.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace DATN_Client.Controllers
{
	public class HomeController : Controller
	{
		HttpClient _client = new HttpClient();
		public static List<Categories_VM> _lstCate = new();
		public HomeController()
		{

		}
		public static string XoaDau(string text)
		{
			string normalizedString = text.Normalize(NormalizationForm.FormD);
			StringBuilder stringBuilder = new StringBuilder();

			foreach (char c in normalizedString)
			{
				UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
				if (unicodeCategory != UnicodeCategory.NonSpacingMark)
				{
					stringBuilder.Append(c);
				}
			}

			return stringBuilder.ToString().Normalize(NormalizationForm.FormC).ToLower();
		}

		public static void SetTenKhongDau(List<Categories_VM> lst)
		{
			// Dictionary để theo dõi số lần xuất hiện của mỗi chuỗi TenKhongDau
			Dictionary<string, int> countDictionary = new Dictionary<string, int>();

			foreach (var x in lst)
			{
				string tenKhongDau = XoaDau(x.Name).Replace(" ", "-");

				// Kiểm tra xem chuỗi đã xuất hiện trước đó chưa
				if (countDictionary.ContainsKey(tenKhongDau))
				{
					// Nếu đã xuất hiện, thì thêm số thứ tự vào cuối chuỗi
					countDictionary[tenKhongDau]++;
					x.TenKhongDau = $"{tenKhongDau}-{countDictionary[tenKhongDau]}";
				}
				else
				{
					// Nếu chưa xuất hiện, thêm vào từ điển với số thứ tự là 1
					countDictionary.Add(tenKhongDau, 1);
					x.TenKhongDau = tenKhongDau;
				}
			}
		}
		public async Task<IActionResult> Index()
		{			
			return RedirectToAction("Index", "Home", new { Area = "Customer" });
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