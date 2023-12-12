using DATN_Shared.ViewModel;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using DATN_Client.Areas.Customer.Component;

namespace DATN_Client.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
		private HttpClient _httpClient;
		public HomeController(HttpClient httpClient)
		{
			_httpClient= httpClient;
		}
        public IActionResult Index()
        {
			HttpContext.Session.SetString($"{Guid.NewGuid()}", JsonConvert.SerializeObject(Guid.NewGuid()));
			
			return View();
        }
	}
}
