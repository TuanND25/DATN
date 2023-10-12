using DATN_Shared.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DATN_Client.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;
        public UserController(HttpClient httpClient)
        {
            _httpClient= httpClient;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Getuser()
        {
         
            var response = await _httpClient.GetAsync("https://localhost:7141/api/getuser");
            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var users =JsonSerializer.Deserialize<List<Products>>(json, options);
            ViewBag.user = users ;
            return View();
        }
    }
}
