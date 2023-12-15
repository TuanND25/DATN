using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;

namespace DATN_Client.Areas.Customer.Component
{
	public partial class BillByUser
	{
		[Inject] private NavigationManager _navigationManager { get; set; }
		private HttpClient _httpClient = new HttpClient();
		[Inject] private Blazored.Toast.Services.IToastService _toastService { get; set; }
		[Inject] public IHttpContextAccessor _ihttpcontextaccessor { get; set; }
		private List<Bill_VM> _lstBills = new List<Bill_VM>();
		private User _user = new User();
		private List<BillDetailShow> _listBillItem = new List<BillDetailShow>();
		private List<PaymentMethod_VM> _lstPayM = new();
		private bool isLoader = false;

		protected override async Task OnInitializedAsync()
		{
			isLoader = true;
			//var a = _ihttpcontextaccessor.HttpContext.Session.GetString("UserId");
			var a = Guid.Parse("05CE1969-7D23-4E5F-90ED-940F161F902A");
			var b = Convert.ToString(a);
			if (!string.IsNullOrEmpty(b))
			{
				_user = await _httpClient.GetFromJsonAsync<User>($"https://localhost:7141/api/user/get_user_by_id/{a}");
				_lstBills = await _httpClient.GetFromJsonAsync<List<Bill_VM>>($"https://localhost:7141/api/Bill/get_bill_by_user/{a}");
				_lstBills = _lstBills.OrderByDescending(x => x.CreateDate).ToList();
				_listBillItem = await _httpClient.GetFromJsonAsync<List<BillDetailShow>>($"https://localhost:7141/api/BillItem/get_alll_billItem_by_UserId/{a}");
				_lstPayM = await _httpClient.GetFromJsonAsync<List<PaymentMethod_VM>>("https://localhost:7141/api/paymentMethod/get_all_paymentMethod");
			}
			else _navigationManager.NavigateTo("/home", true);
			isLoader = false;
		}

		private async Task NavBillItem(Guid billid)
		{
			_navigationManager.NavigateTo($"/account/bill-history/bill-detail?billid={billid}", true);
		}

		private List<BillDetailShow> LoadBillItemByBill(Guid Id)
		{
			var b = _listBillItem.Where(x => x.BillID == Id).ToList();
			return b;
		}

		private async Task<string> LayTenPTTT(Guid id)
		{
			var pttt = await _httpClient.GetFromJsonAsync<PaymentMethod_VM>($"https://localhost:7141/api/paymentMethod/get_all_paymentMethod_ById/{id}");
			return pttt.Name;
		}

		private async Task ThanhToanHdMomo()
		{
		}
	}
}