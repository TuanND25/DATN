using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DATN_Client.Areas.Admin.Components
{
	public partial class User
	{
		HttpClient httpClient = new HttpClient();
		[Inject] NavigationManager navigationManager { get; set; }
		public List<User_VM> users { get; set; }
		public static List<AddressShip_VM> addressShips {get; set;}

		public  User_VM doituongtam = new User_VM();
		public string Message { get; set; } = string.Empty;

		public User_VM user_VM = new User_VM();
		public AddUserByAdmin user = new AddUserByAdmin();
		protected override async Task OnInitializedAsync()
		{
			users = await httpClient.GetFromJsonAsync<List<User_VM>>("https://localhost:7141/api/user/get-user");

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
			users = await httpClient.GetFromJsonAsync<List<User_VM>>("https://localhost:7141/api/user/get-user");
			users = users.Where(u=>user_VM.UserName==null|| user_VM.UserName==string.Empty|| u.UserName.Trim().ToLower().Contains(user_VM.UserName.Trim().ToLower())).ToList();
		}
		public async Task gandoituong(User_VM user)
		{
			doituongtam = user;
		}
    }
}
