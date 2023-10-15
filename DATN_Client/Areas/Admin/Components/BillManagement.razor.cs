using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DATN_Client.Areas.Admin.Components
{
	public partial class BillManagement
	{
		HttpClient _client = new HttpClient();
		List<Bill_ShowModel> _lstbill = new List<Bill_ShowModel>();
        public static Bill_ShowModel _billModel= new Bill_ShowModel();
		[Inject]
		public NavigationManager _navigationManager { get; set; }
		protected override async Task OnInitializedAsync()
		{
			_lstbill = await _client.GetFromJsonAsync<List<Bill_ShowModel>>("https://localhost:7141/api/Bill/get_alll_bill");
		}
		public async Task ClickDetailBill(Bill_ShowModel bill_ShowModel)
		{
			_billModel = bill_ShowModel;
			_navigationManager.NavigateTo("https://localhost:7075/Admin/BillManagement/Details",true);

        }


	}
}
