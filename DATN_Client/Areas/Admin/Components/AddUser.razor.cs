using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;

namespace DATN_Client.Areas.Admin.Components
{
	public partial class AddUser
	{
		[Inject] Blazored.Toast.Services.IToastService _toastService { get; set; } // Khai báo khi cần gọi ở code-behind
		[Inject] NavigationManager navigationManager { get; set; }

		HttpClient _httpClient = new HttpClient();
		AddUserByAdmin user = new AddUserByAdmin();

		

		
	}
}
