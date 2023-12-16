using DATN_Client.Areas.Customer.Controllers;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using DATN_Shared.ViewModel.Momo;
using DATN_Shared.ViewModel.Momo.Order;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace DATN_Client.Areas.Customer.Component
{
	public partial class BilllItemByUser
	{
		[Inject] private NavigationManager _navigationManager { get; set; }
		private HttpClient _httpClient = new();
		[Inject] private Blazored.Toast.Services.IToastService _toastService { get; set; }

		private Bill_ShowModel _bill_ShowModel = new();

		private BillItem_VM _billItem = new();
		private List<BillDetailShow> _lstBillItems = new();
		private List<AddressShip_VM> _lstAddressGetById = new();
		[Inject] public IHttpContextAccessor _ihttpcontextaccessor { get; set; }
		private OrderInfoModel _ord = new();
		private Bill_DataAnotation_VM _bill_vm = new();
		//private User _user = new();
		private int _tienGiamVoucher { get; set; } = 0;
		private int _tienGiamDiemDung { get; set; } = 0;
		protected override async Task OnInitializedAsync()
		{
			try
			{
				//var iduser = _ihttpcontextaccessor.HttpContext.Session.GetString("UserId");
				//_user = await _httpClient.GetFromJsonAsync<User>($"https://localhost:7141/api/user/get_user_by_id/{iduser}");
				var a = await _httpClient.GetFromJsonAsync<List<Bill_ShowModel>>("https://localhost:7141/api/Bill/get_alll_bill");
				_bill_ShowModel = a.FirstOrDefault(x => x.Id == UserManagementController._billId);
				_lstBillItems = await _httpClient.GetFromJsonAsync<List<BillDetailShow>>($"https://localhost:7141/api/BillItem/getbilldetail/{UserManagementController._billId}");
				_bill_vm = await _httpClient.GetFromJsonAsync<Bill_DataAnotation_VM>($"https://localhost:7141/api/Bill/get_bill_by_id/{UserManagementController._billId}");
				if (_bill_vm.VoucherId != null)
				{
					var vch = await _httpClient.GetFromJsonAsync<Voucher_VM>($"https://localhost:7141/api/Voucher/ID?Id={_bill_vm.VoucherId}");
					_tienGiamVoucher = _bill_vm.TotalAmount * vch.Percent / 100 ?? 0;
					if (_tienGiamVoucher > vch.Maximum_Reduction)_tienGiamVoucher = vch.Maximum_Reduction;
				}
				_tienGiamDiemDung = (await _httpClient.GetFromJsonAsync<HistoryConsumerPoint_VM>($"https://localhost:7141/api/HistoryConsumerPoint/Get-HistoryConsumerPointBy-BillId/{_bill_vm.Id}") ?? new()).Point;
				var c = Guid.Parse(_ihttpcontextaccessor.HttpContext.Session.GetString("UserId"));
				//var c = Guid.Parse("F3C0CEA4-8F18-4990-908A-5DF1169F87A2");
				var d = await _httpClient.GetFromJsonAsync<List<AddressShip_VM>>($"https://localhost:7141/api/AddressShip/get_address_by_UserID/{c}");
				_lstAddressGetById = d.OrderByDescending(x => x.Status).ToList();
			}
			catch (Exception)
			{
				_navigationManager.NavigateTo("/home", true);
			}
		}

		public async Task ChonDiaChiTuList(AddressShip_VM addressShip_VM)
		{
			_bill_ShowModel.Recipient = addressShip_VM.Recipient;
			_bill_ShowModel.PhoneNumber = addressShip_VM.NumberPhone;
			_bill_ShowModel.Province = addressShip_VM.Province;
			_bill_ShowModel.District = addressShip_VM.District;
			_bill_ShowModel.WardName = addressShip_VM.WardName;

			var a = await _httpClient.PutAsJsonAsync("https://localhost:7141/api/Bill/Put-Bill", _bill_ShowModel);
			if (a.IsSuccessStatusCode)
			{
				_toastService.ShowSuccess("Thay đổi địa chỉ thành công");
			}
			else
			{
				_toastService.ShowError("Thay đổi địa chỉ thất bại");
			}
		}

		public async Task HuyDonHang()
		{
			_bill_ShowModel.Status = 0;
			var a = await _httpClient.PutAsJsonAsync("https://localhost:7141/api/Bill/Put-Bill", _bill_ShowModel);
			if (a.IsSuccessStatusCode)
			{
				_toastService.ShowSuccess("Huỷ đơn hàng thành công");
			}
			else
			{
				_toastService.ShowError("Huỷ đơn hàng thất bại");
			}
		}

		private async Task ThanhToanHdMomo()
		{
			Create_Bill_With_Info._bill_validate_vm = _bill_vm;
			//lấy user
			User user = await _httpClient.GetFromJsonAsync<User>($"https://localhost:7141/api/user/get_user_by_id/{Create_Bill_With_Info._bill_validate_vm.UserId}");
			// convert b_vm sang Create_Bill_With_Info._bill_validate_vm
			// gửi yêu cầu thanh toán momo
			_ord.OrderId = Guid.NewGuid().ToString();
			if (user.Name == null) _ord.FullName = "Không có thông tin khách hàng";
			else _ord.FullName = user.Name;
			_ord.OrderInfo = Create_Bill_With_Info._bill_validate_vm.Note + $". Mã hóa đơn: {Create_Bill_With_Info._bill_validate_vm.BillCode}";
			_ord.Amount = Create_Bill_With_Info._bill_validate_vm.TotalAmount;
			var reponse1 = await _httpClient.PostAsJsonAsync("https://localhost:7141/api/Momo/CreatePaymentAsync", _ord);
			var reponse2 = await reponse1.Content.ReadFromJsonAsync<MomoCreatePaymentResponseModel>();
			_navigationManager.NavigateTo($"{reponse2.PayUrl}", true);
			return;
		}
	}
}