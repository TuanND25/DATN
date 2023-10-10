using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DATN_Client.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;
        public LoginController(HttpClient httpClient)
        {
            _httpClient= httpClient;
        }
      
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUser user)
        {

            // convert user to json
            var loginUserJson = JsonConvert.SerializeObject(user);

            //convert to string content
            var stringContent = new StringContent(loginUserJson, Encoding.UTF8, "application/json");

            // send request to api login

            var response = await _httpClient.PostAsync($"https://localhost:7141/api/user/login/", stringContent);


            if (response.IsSuccessStatusCode)
            {

                var token = await response.Content.ReadAsStringAsync();

                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, jwt.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value));
                identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Role).Value));
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal);

                var check = User.Identity.IsAuthenticated;



                var resquest = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7141/api/getuser");

                // Thêm mã token vào tiêu đề HTTP của yêu cầu
                resquest.Headers.Add("Authorization", $"Bearer {token}");
                var reponse = await _httpClient.SendAsync(resquest);

                // Kiểm tra phản hồi từ API
                if (reponse.IsSuccessStatusCode)
                {
                    // Mã token hợp lệ
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Mã token không hợp lệ
                    ViewBag.Message = await reponse.Content.ReadAsStringAsync();
                    return View();
                }

            }
            else
            {
                ViewBag.Message = await response.Content.ReadAsStringAsync();
                return View();
            }


        }
    }
}
