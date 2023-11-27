using DATN_Client.Areas.Customer.Controllers;
using DATN_Client.SessionService;
using DATN_Shared.ViewModel;
using DATN_Shared.ViewModel.Momo;
using Microsoft.AspNetCore.Components;
using System.Net.Http;

namespace DATN_Client.Areas.Customer.Component
{
	public partial class PaymentCallBack_Blazor
	{
		private HttpClient _client = new HttpClient();
		private MomoExecuteResponseModel _responseModel = new MomoExecuteResponseModel();
		private List<Bill_VM> _lstBill = new List<Bill_VM>();
		private List<CartItems_VM> _lstCI = new List<CartItems_VM>();
		private List<ProductItem_VM> _lstPrI_VM = new List<ProductItem_VM>();

		private ProductItem_Show_VM _pi_s_vm = new ProductItem_Show_VM();
		private ProductItem_VM _pi_vm = new ProductItem_VM();
		[Inject] Blazored.Toast.Services.IToastService _toastService { get; set; } // Khai báo khi cần gọi ở code-behind
		[Inject] public IHttpContextAccessor _ihttpcontextaccessor { get; set; }
		[Inject] private NavigationManager _navi { get; set; }
		public string? _iduser { get; set; }
		protected override async Task OnInitializedAsync()
		{
			_iduser = _ihttpcontextaccessor.HttpContext.Session.GetString("UserId");
			_responseModel = BanOnlineController._momoExecuteResponseModel;
			_lstPrI_VM = await _client.GetFromJsonAsync<List<ProductItem_VM>>("https://localhost:7141/api/productitem/get_all_productitem");
			if (_responseModel.Message.ToLower() == "success")
			{
				Create_Bill_With_Info._bill_validate_vm.Status = 1;
				var updateStatus = _client.PutAsJsonAsync("https://localhost:7141/api/Bill/Put-Bill", Create_Bill_With_Info._bill_validate_vm);
			}
		}
		public async Task BackToProduct()
		{
			_navi.NavigateTo("/all-product", true);
		}
		public async Task Bill()
		{
			_navi.NavigateTo("https://localhost:7075/Customer/UserManagement/BillByUser", true);
		}
	}
}