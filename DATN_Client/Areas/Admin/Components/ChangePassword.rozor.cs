using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;

namespace DATN_Client.Areas.Admin.Components
{
	public partial class ChangePassword
	{
		public ChangePassword_VM changePassword = new ChangePassword_VM();

		[Inject] NavigationManager navigationManager { get; set; }


	}
}
