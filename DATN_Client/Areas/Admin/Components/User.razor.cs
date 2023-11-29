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

		public  User_VM doituongtam = new User_VM();
		public string Message { get; set; } = string.Empty;

		public User_VM user_VM = new User_VM();
		public AddUserByAdmin userbyadmin = new AddUserByAdmin();
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
        public async Task ChangeStatusUser()
        {
			doituongtam.Status = 0;
            var a= await httpClient.PutAsJsonAsync<User_VM>("https://localhost:7141/api/user/update-user", doituongtam);
        }
		
		public async Task SearchByUsername()
		{
			users = await httpClient.GetFromJsonAsync<List<AddUserByAdmin>>("https://localhost:7141/api/user/get-user");
			users = users.Where(u=>user_VM.UserName==null|| user_VM.UserName==string.Empty|| u.username.Trim().ToLower().Contains(user_VM.UserName.Trim().ToLower())).ToList();
		}
		public async Task gandoituong(User_VM user)
		{
			doituongtam = user;
		}
		public async Task AddUserVsStaff()
		{
			if (userbyadmin.username == string.Empty || userbyadmin.email == string.Empty || userbyadmin.phonenumber == string.Empty || userbyadmin.password == string.Empty || userbyadmin.confirmpassword == string.Empty || userbyadmin.name == string.Empty || userbyadmin.role == string.Empty)
			{
				_toastService.ShowError("Vui lòng điền đầy đủ thông tin");
				return;
			}
			var response = await httpClient.PostAsJsonAsync<AddUserByAdmin>("https://localhost:7141/api/user/add-employee-admin", userbyadmin);
			var result = response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode)
			{
				_toastService.ShowSuccess(result.Result);
				Task.Delay(2000);
				navigationManager.NavigateTo("https://localhost:7075/Admin/User");
			}
			else
			{
				_toastService.ShowError(result.Result);
			}
		}
		public async Task LoadFormUser(AddUserByAdmin GetValueFromList)
		{
			userbyadmin.id = GetValueFromList.id;
			userbyadmin.name = GetValueFromList.name;
			userbyadmin.username = GetValueFromList.username;
			userbyadmin.phonenumber = GetValueFromList.phonenumber;
			userbyadmin.email = GetValueFromList.email;
			userbyadmin.sex = GetValueFromList.sex;
			userbyadmin.status = GetValueFromList.status;
			userbyadmin.password = GetValueFromList.password;	
			userbyadmin.confirmpassword= GetValueFromList.confirmpassword;
			userbyadmin.role = GetValueFromList.role;	
							

		}
		public async Task UpdateUser()
		{
			if (userbyadmin.username == string.Empty || userbyadmin.email == string.Empty || userbyadmin.phonenumber == string.Empty || userbyadmin.password == string.Empty || userbyadmin.confirmpassword == string.Empty || userbyadmin.name == string.Empty || userbyadmin.role== string.Empty)
			{
				_toastService.ShowError("Vui lòng điền đầy đủ thông tin");
				return;
			}
			var response = await httpClient.PutAsJsonAsync<AddUserByAdmin>("https://localhost:7141/api/user/update-user", userbyadmin);
			var result = response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode)
			{
				_toastService.ShowSuccess(result.Result);
				Task.Delay(2000);
				navigationManager.NavigateTo("https://localhost:7075/Admin/User");
			}
			else
			{
				_toastService.ShowError(result.Result);
			}
		}
		
	}
}
