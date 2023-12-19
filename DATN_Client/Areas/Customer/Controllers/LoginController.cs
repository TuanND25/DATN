using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication;
using DATN_Client.Areas.Customer.Component;

namespace DATN_Client.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;
        public LoginController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [Route("login")]
        [HttpGet]
        public IActionResult Login()
        {
			HttpContext.Session.SetString($"{Guid.NewGuid()}", JsonConvert.SerializeObject(Guid.NewGuid()));
			return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> Login(LoginUser user)
        //{
        //    var loginUserJson = JsonConvert.SerializeObject(user);
        //    var stringContent = new StringContent(loginUserJson, Encoding.UTF8, "application/json");
        //    var response = await _httpClient.PostAsync($"https://localhost:7141/api/user/login", stringContent);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var token = await response.Content.ReadAsStringAsync();
        //        var handler = new JwtSecurityTokenHandler();
        //        var jwt = handler.ReadJwtToken(token);
        //        var claims = new List<Claim>
        //        {
        //            new Claim("Id", jwt.Claims.FirstOrDefault(u => u.Type == "Id").Value),
        //            new Claim(ClaimTypes.NameIdentifier, jwt.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value),
        //            new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Role).Value)
        //        };
        //        var identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
        //        var principal = new ClaimsPrincipal(identity);

        //        //var data = claims.Select(c => c.Value).ToArray();
        //        //var id = data[0];
        //        await HttpContext.SignInAsync(principal);
        //        //HttpContext.Session.SetString("UserId", jwt.Claims.FirstOrDefault(u => u.Type == "Id").Value);
        //        //HttpContext.Session.SetString("Token", token);
        //        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //        var responseAuthorize = await _httpClient.GetAsync("https://localhost:7141/api/user/get-user");
        //        if (principal.IsInRole("Admin"))
        //        {
        //            return RedirectToAction("Index", "Home", new { Area = "Admin" });
        //        }
        //        else
        //        {
        //            return RedirectToAction("Index", "Home", new { Area = "Customer" });
        //        }
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        public async Task<IActionResult> LogOut()
        {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("Token");
            DATN_Client.Areas.Customer.Component.Login.UserNameShowHome = null;
			DATN_Client.Areas.Customer.Component.Login.Roleuser = null;
			return RedirectToAction("Index", "Home", new { Area = "Customer" });
           
        }

        public async Task<IActionResult> forget()
        {
            return View();
        }
    }
}
