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
            return View();
        }
		public IActionResult LoginSignUp()
		{
			return View();
		}

     
        public IActionResult SignUp()
        {
            return View();
        }


    }
}
