using System.Text.RegularExpressions;
using DATN_Client.Areas.Customer.Component;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Twilio.Rest.Chat.V2.Service.User;

namespace DATN_Client.Areas.Admin.Components
{
	public partial class User
	{
		[Inject] Blazored.Toast.Services.IToastService _toastService { get; set; } // Khai báo khi cần gọi ở code-behind
		HttpClient httpClient = new HttpClient();
		[Inject] NavigationManager navigationManager { get; set; }
		public List<AddUserByAdmin> users { get; set; }
		public static List<AddressShip_VM> addressShips {get; set;}

		public  AddUserByAdmin doituongtam = new AddUserByAdmin();
		public string Message { get; set; } = string.Empty;

		public User_VM user_VM = new User_VM();
		public AddUserByAdmin userbyadmin = new AddUserByAdmin();
		public AddUserByAdmin userbyadmin1 = new AddUserByAdmin();
		protected override async Task OnInitializedAsync()
		{
			users = await httpClient.GetFromJsonAsync<List<AddUserByAdmin>>("https://localhost:7141/api/user/get-user");

			addressShips = await httpClient.GetFromJsonAsync<List<AddressShip_VM>>("https://localhost:7141/api/AddressShip");
        }
		public async Task GetAddressShipByUser(Guid Id)
		{

			addressShips = addressShips.Where(a => a.UserId == Id).ToList();
			navigationManager.NavigateTo("https://localhost:7075/Admin/Addresss",true);
		}
        public async Task ChangeStatusUser(AddUserByAdmin user)
        {
			user_VM.Id = user.id;
			user_VM.Name = user.name;
			user_VM.UserName = user.username;
			user_VM.PhoneNumber = user.phonenumber;
			user_VM.Status = 0;
			user_VM.Email = user.email;
			user_VM.Role = user.role;
			user_VM.Sex = user.sex;
			user = null;
            var a= await httpClient.PutAsJsonAsync<User_VM>("https://localhost:7141/api/user/update-status-user", user_VM);
			navigationManager.NavigateTo("https://localhost:7075/Admin/User", true);
		}
		
		public async Task SearchByUsername()
		{
			users = await httpClient.GetFromJsonAsync<List<AddUserByAdmin>>("https://localhost:7141/api/user/get-user");
			users = users.Where(u=>user_VM.UserName==null|| user_VM.UserName==string.Empty|| u.username.Trim().ToLower().Contains(user_VM.UserName.Trim().ToLower())).ToList();
		}
		public async Task gandoituong(AddUserByAdmin user)
		{
			doituongtam = user;
		}
		public async Task AddUserVsStaff()
		{
			if (userbyadmin.username == string.Empty || userbyadmin.email == string.Empty || userbyadmin.phonenumber == string.Empty || userbyadmin.password == string.Empty || userbyadmin.name == string.Empty || userbyadmin.role == string.Empty)
			{
				_toastService.ShowError("Vui lòng điền đầy đủ thông tin");
				return;
			}
			Regex phoneNumberRegex = new Regex(@"^0\d{9}$");
			if (!phoneNumberRegex.IsMatch(userbyadmin.phonenumber))
			{
				_toastService.ShowError("Số điện thoại không hợp lệ");
				return;

			}
			Regex emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
			if (!emailRegex.IsMatch(userbyadmin.email))
			{
				_toastService.ShowError("Email không hợp lệ");
				return;
			}
			var response = await httpClient.PostAsJsonAsync<AddUserByAdmin>("https://localhost:7141/api/user/add-employee-admin", userbyadmin);
			var result = response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode)
			{
				_toastService.ShowSuccess(result.Result);
				await Task.Delay(2000);
				navigationManager.NavigateTo("https://localhost:7075/Admin/User",true);
			}
			else
			{
				_toastService.ShowError(result.Result);
			}
		}
		public async Task LoadFormUser(AddUserByAdmin GetValueFromList)
		{
			userbyadmin1.id = GetValueFromList.id;
			userbyadmin1.name = GetValueFromList.name;
			userbyadmin1.username = GetValueFromList.username;
			userbyadmin1.phonenumber = GetValueFromList.phonenumber;
			userbyadmin1.email = GetValueFromList.email;
			userbyadmin1.sex = GetValueFromList.sex;
			userbyadmin1.status = GetValueFromList.status;
			userbyadmin1.password = GetValueFromList.password;	
			userbyadmin1.role = GetValueFromList.role;	
		}
		public async Task UpdateUser()
		{
			if (userbyadmin1.username == string.Empty || userbyadmin1.email == string.Empty  || userbyadmin1.name == string.Empty || userbyadmin1.role== string.Empty)
			{
				_toastService.ShowError("Vui lòng điền đầy đủ thông tin");
				return;
			}
			var response = await httpClient.PutAsJsonAsync<AddUserByAdmin>("https://localhost:7141/api/user/update-user", userbyadmin1);
			var result = response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode)
			{
				_toastService.ShowSuccess(result.Result);
				await Task.Delay(2000);
				navigationManager.NavigateTo("https://localhost:7075/Admin/User",true);
			}
			else
			{
				_toastService.ShowError(result.Result);
			}
		}
		
	}
}
