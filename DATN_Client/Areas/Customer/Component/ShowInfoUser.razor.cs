using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using DATN_Shared.ViewModel.DiaChi;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;

namespace DATN_Client.Areas.Customer.Component
{
	public partial class ShowInfoUser
	{
		private HttpClient _client = new HttpClient();

		private User User_VM = new User();
		private List<User> _lstUser_VM = new List<User>();
		[Inject] public IHttpContextAccessor _ihttpcontextaccessor { get; set; }
		[Inject] private NavigationManager _navigationManager { get; set; }

		public string Message { get; set; } = string.Empty;

		[Inject] private Blazored.Toast.Services.IToastService _toastService { get; set; } // Khai báo khi cần gọi ở code-behind
		private bool isModalOpenUpdateUser = false;

		protected override async Task OnInitializedAsync()
		{
			//var token = _ihttpcontextaccessor.HttpContext.Session.GetString("Token"); // Gọi token
			//_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); // Xác thực

			try
			{
				var a = Guid.Parse(_ihttpcontextaccessor.HttpContext.Session.GetString("UserId"));
				//var a = Guid.Parse("a4c10abe-eec2-40e6-9b6c-cf1221e9da78");
				User_VM = await _client.GetFromJsonAsync<User>($"https://localhost:7141/api/user/get_user_by_id/{a}");
			}
			catch (Exception)
			{
				_navigationManager.NavigateTo("/home", true);
			}
		}

		public async Task UpdateUser()
		{
			User_VM user_VM = new User_VM();
			user_VM.UserName = User_VM.UserName;
			user_VM.Id = User_VM.Id;
			user_VM.PhoneNumber = User_VM.PhoneNumber;
			user_VM.Status = User_VM.Status;
			user_VM.Role = user_VM.Role;
			user_VM.Name = User_VM.Name;
			user_VM.Sex= User_VM.Sex;
			user_VM.Email = User_VM.Email;
			var a = await _client.PutAsJsonAsync($"https://localhost:7141/api/user/update-user-customer", user_VM);
			if (a.IsSuccessStatusCode)
			{
				Login.UserNameShowHome = LayChuCuoiName(User_VM.Name);
				ClosePopup("UpdateUser");
				_toastService.ShowSuccess("Cập nhật thông tin người dùng thành công");
				_navigationManager.NavigateTo("https://localhost:7075/account/info", true);
			}
			else
			{
				_toastService.ShowError("Cập nhật thông tin người dùng thất bại");
			}
		}

		public async Task LoadUser(Guid Id)
		{
			OpenPopup("UpdateUser");
			var a = await _client.GetFromJsonAsync<User>($"https://localhost:7141/api/user/get_user_by_id/{Id}");
			User_VM.Id = a.Id;
			User_VM.Name = a.Name;
			User_VM.Email = a.Email;
			User_VM.PhoneNumber = a.PhoneNumber;
			User_VM.Sex = a.Sex;
			User_VM.UserName = a.UserName;
		}

		private void SetModalState(bool isOpen, string modalType)
		{
			switch (modalType)
			{
				case "UpdateUser":
					isModalOpenUpdateUser = isOpen;
					break;

				default:
					break;
			}
		}

		private void OpenPopup(string modalType)
		{
			SetModalState(true, modalType);
		}

		private void ClosePopup(string modalType)
		{
			SetModalState(false, modalType);
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
    }
}