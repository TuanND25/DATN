using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using DATN_Shared.ViewModel.Momo;
using DATN_Shared.ViewModel.Momo.Order;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace DATN_Client.Areas.Customer.Component
{
	public partial class BillByUser
	{
		[Inject] private NavigationManager _navigationManager { get; set; }
		private HttpClient _httpClient = new();
		[Inject] private Blazored.Toast.Services.IToastService _toastService { get; set; }
		[Inject] public IHttpContextAccessor _ihttpcontextaccessor { get; set; }
		private List<Bill_VM> _lstBills = new();
		private User _user = new();
		private List<BillDetailShow> _listBillItem = new();
		private List<PaymentMethod_VM> _lstPayM = new();
		private OrderInfoModel _ord = new();
		private bool isLoader = false;

		protected override async Task OnInitializedAsync()
		{
			isLoader = true;
			var a = _ihttpcontextaccessor.HttpContext.Session.GetString("UserId");
			//var a = Guid.Parse("05CE1969-7D23-4E5F-90ED-940F161F902A");
			if (!string.IsNullOrEmpty(a))
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

		private async Task ThanhToanHdMomo(Bill_VM b_vm)
		{
			// convert b_vm sang Create_Bill_With_Info._bill_validate_vm
			string json = JsonSerializer.Serialize(b_vm);
			Create_Bill_With_Info._bill_validate_vm = JsonSerializer.Deserialize<Bill_DataAnotation_VM>(json);
			// gửi yêu cầu thanh toán momo
			_ord.OrderId = Guid.NewGuid().ToString();
			if (_user.Name == null) _ord.FullName = "Không có thông tin khách hàng";
			else _ord.FullName = _user.Name;
			_ord.OrderInfo = b_vm.Note + $". Mã hóa đơn: {b_vm.BillCode}";
			_ord.Amount = b_vm.TotalAmount;
			var reponse1 = await _httpClient.PostAsJsonAsync("https://localhost:7141/api/Momo/CreatePaymentAsync", _ord);
			var reponse2 = await reponse1.Content.ReadFromJsonAsync<MomoCreatePaymentResponseModel>();
			_navigationManager.NavigateTo($"{reponse2.PayUrl}", true);
			return;
		}
	}
}