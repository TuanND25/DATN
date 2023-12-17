using DATN_Client.SessionService;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace DATN_Client.Areas.Customer.Component
{
	public partial class Login
	{
		[Inject] Blazored.Toast.Services.IToastService _toastService { get; set; } // Khai báo khi cần gọi ở code-behind
		HttpClient _httpClient = new HttpClient();
		public string Message { get; set; } = null;

		[Inject] public IHttpContextAccessor iHttpContext { get; set; }
		[Inject] NavigationManager navigationManager { get; set; }
		public List<User> Users { get; set; }
		LoginUser loginUser = new LoginUser();
		public string Idsession { get; set; } = string.Empty;
		public static string Roleuser { get; set; } = string.Empty;
		public static string UserNameShowHome { get; set; }
		public static bool _chaoLogin { get; set; } = false;
		public string _check { get; set; } = "password";
		
		protected override async Task OnInitializedAsync()
		{
			var idUser = iHttpContext.HttpContext.Session.GetString("UserId");
			if (!string.IsNullOrEmpty(idUser))
			{
				if (Roleuser == "Admin") navigationManager.NavigateTo("/admin", true);
				if (Roleuser == "Staff") navigationManager.NavigateTo("https://localhost:7075/Admin/SellStalls", true);
				if (Roleuser == "User") navigationManager.NavigateTo("/admin", true);
			}
		}
		public async Task login()
		{
			if (loginUser.UserName == string.Empty || loginUser.Password == string.Empty)
			{
				_toastService.ShowError("Vui lòng điền đầy đủ thông tin");
				return;

			}
			var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
			if (hasSymbols.IsMatch(loginUser.UserName))
			{
				_toastService.ShowError("Tên đăng nhập không được chứa các ký tự đặc biệt");
				return;
			}
			if (hasSymbols.IsMatch(loginUser.Password))
			{
				_toastService.ShowError("Mật khẩu không được chứa các ký tự đặc biệt");
				return;
			}


			var response = await _httpClient.PostAsJsonAsync<LoginUser>("https://localhost:7141/api/user/login/", loginUser);
			var result = response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode)
			{
				var token = await response.Content.ReadAsStringAsync();
				var handler = new JwtSecurityTokenHandler();
				var jwt = handler.ReadJwtToken(token);
				// tạo đối tượng xác thực
				var claims = new List<Claim>
				{
					new Claim("Id", jwt.Claims.FirstOrDefault(u => u.Type == "Id").Value),
					new Claim(ClaimTypes.NameIdentifier, jwt.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value),
					new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Role).Value)
				};
				var identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
				var principal = new ClaimsPrincipal(identity);

				var data = claims.Select(c => c.Value).ToArray();
				var id = data[0];
				Roleuser = data[2];
				iHttpContext.HttpContext.Session.SetString("UserId", id);
				iHttpContext.HttpContext.Session.SetString("Token", token);
               


                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
				var responseAuthorize = await _httpClient.GetAsync("https://localhost:7141/api/user/get-user");
				if (principal.IsInRole("Staff"))
				{
					navigationManager.NavigateTo("https://localhost:7075/Admin/SellStalls", true);
				}
				if (principal.IsInRole("Admin"))
				{
					navigationManager.NavigateTo("https://localhost:7075/Admin", true);
				}
				else
				{

					var iduser = Guid.Parse(iHttpContext.HttpContext.Session.GetString("UserId"));
					var user = await _httpClient.GetFromJsonAsync<User_VM>($"https://localhost:7141/api/user/get_user_by_id/{iduser}");
					UserNameShowHome = LayChuCuoiName(user.Name);
					_chaoLogin = true;
					iHttpContext.HttpContext.Session.Remove("_lstCI_Vanglai");
					_toastService.ShowSuccess("Chào người dùng");
					await Task.Delay(2000);
					navigationManager.NavigateTo("/home", true);
				}
			}
			else
			{
				_toastService.ShowError(result.Result);
			}
		}

		private string LayChuCuoiName(string input)
		{
			input = input.Trim();
			// Kiểm tra xem chuỗi có khoảng trắng không
			int lastSpaceIndex = input.LastIndexOf(' ');

			// Nếu có khoảng trắng, lấy phần cuối cùng
			if (lastSpaceIndex >= 0 && lastSpaceIndex < input.Length - 1)
			{
				return input.Substring(lastSpaceIndex + 1);
			}
			else
			{
				// Nếu không có khoảng trắng, trả về toàn bộ chuỗi
				return input;
			}
		}

		private void CheckPass(ChangeEventArgs e)
		{
			if ((bool)e.Value == false) _check = "text";
			else _check = "password";
		}
	}
}
