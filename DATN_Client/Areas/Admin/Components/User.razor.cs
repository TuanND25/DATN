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
		public string Message { get; set; } = string.Empty;

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
        public async Task ChangeStatusUser(User_VM user)
        {
            user.Status = 0;
            var a= await httpClient.PutAsJsonAsync<User_VM>("https://localhost:7141/api/user/update-user", user);
        }
    }
}
