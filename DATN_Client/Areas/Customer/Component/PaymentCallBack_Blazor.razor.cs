using DATN_Client.Areas.Customer.Controllers;
using DATN_Client.SessionService;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using DATN_Shared.ViewModel.Momo;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;

namespace DATN_Client.Areas.Customer.Component
{
	public partial class PaymentCallBack_Blazor
	{
		private HttpClient _client = new HttpClient();
		private MomoExecuteResponseModel _responseModel = new MomoExecuteResponseModel();
		[Inject] Blazored.Toast.Services.IToastService _toastService { get; set; } // Khai báo khi cần gọi ở code-behind
		[Inject] public IHttpContextAccessor _ihttpcontextaccessor { get; set; }
		[Inject] private NavigationManager _navi { get; set; }
		private Guid _billid {  get; set; }
		private string _iduser { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
		{
			_iduser = _ihttpcontextaccessor.HttpContext.Session.GetString("UserId");
			_responseModel = BanOnlineController._momoExecuteResponseModel;
			if (_responseModel.Message.ToLower() == "success")
			{
				Create_Bill_With_Info._bill_validate_vm.Status = 2;
				var updateStatus = _client.PutAsJsonAsync("https://localhost:7141/api/Bill/Put-Bill", Create_Bill_With_Info._bill_validate_vm);
			}
			if (string.IsNullOrEmpty(_iduser))
			{
				Create_Bill_With_Info._bill_validate_vm.Status = 0;
				Create_Bill_With_Info._bill_validate_vm.CanelBy = Create_Bill_With_Info._bill_validate_vm.UserId.ToString();
                var updateStatus = _client.PutAsJsonAsync("https://localhost:7141/api/Bill/Put-Bill", Create_Bill_With_Info._bill_validate_vm);
			}
			_billid = Create_Bill_With_Info._bill_validate_vm.Id;
			Create_Bill_With_Info._bill_validate_vm = new();
        }
		public async Task BackToProduct()
		{
			_navi.NavigateTo("/all-product", true);
		}
		public async Task Bill()
		{
			_navi.NavigateTo($"/account/bill-history/bill-detail?billid={_billid}", true);
		}
	}
}