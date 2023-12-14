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
			var a = await _client.PutAsJsonAsync($"https://localhost:7141/api/user/update-user", User_VM);
			if (a.IsSuccessStatusCode)
			{
				ClosePopup("UpdateUser");
				_toastService.ShowSuccess("Cập nhật thông tin người dùng thành công");
			}
			else
			{
				_toastService.ShowSuccess("Cập nhật thông tin người dùng thành công");
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
	}
}