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
			if (_iduser == null)
			{
				_lstCI = SessionServices.GetLstFromSession_LstCI(_ihttpcontextaccessor.HttpContext.Session, "_lstCI_Vanglai");
			}
			else _lstCI = await _client.GetFromJsonAsync<List<CartItems_VM>>($"https://localhost:7141/api/CartItems/{Create_Bill_With_Info._bill_vm.UserId}");
            if (_responseModel.Message.ToLower() == "success")
            {
				var codeToday = "B" + DateTime.Now.ToString().Substring(0, 10).Replace("/", "") + ".";
				_lstBill = (await _client.GetFromJsonAsync<List<Bill_VM>>("https://localhost:7141/api/Bill/get_alll_bill")).Where(c => c.BillCode.StartsWith(codeToday)).ToList();
				// Tạo mã bill dạng: B + ngày tháng năm tạo bill + số lớn nhất +1
				if (_lstBill.Count == 0) Create_Bill_With_Info._bill_vm.BillCode = codeToday + "1";
				else Create_Bill_With_Info._bill_vm.BillCode = 
						codeToday + _lstBill.Max(c => int.Parse(c.BillCode.Substring(10)) + 1).ToString();
				Create_Bill_With_Info._bill_vm.CreateDate = DateTime.Now;
                if (Create_Bill_With_Info._bill_vm.ToAddress == string.Empty) Create_Bill_With_Info._bill_vm.ToAddress = "...";
                var addBill = await _client.PostAsJsonAsync("https://localhost:7141/api/Bill/Post-Bill", Create_Bill_With_Info._bill_vm);
                if (addBill.IsSuccessStatusCode)
                {
					foreach (var x in _lstCI)
					{
						_pi_vm = _lstPrI_VM.Where(c => c.Id == x.ProductItemId).FirstOrDefault();
						BillItem_VM billItem_VM = new BillItem_VM();
						billItem_VM.Id = Guid.NewGuid();
						billItem_VM.BillId = Create_Bill_With_Info._bill_vm.Id;
						billItem_VM.ProductItemsId = x.ProductItemId;
						billItem_VM.Quantity = x.Quantity;
						billItem_VM.Price = _pi_vm.CostPrice;
						billItem_VM.Status = 1;
						_pi_vm.AvaiableQuantity -= x.Quantity;
						var a = await _client.PostAsJsonAsync("https://localhost:7141/api/BillItem/Post-BillItem", billItem_VM);
						var b = await _client.PutAsJsonAsync("https://localhost:7141/api/productitem/update_productitem", _pi_vm);
						var c = await _client.DeleteAsync($"https://localhost:7141/api/CartItems/delete-CartItems/{x.Id}");
					}
					_ihttpcontextaccessor.HttpContext.Session.Remove("_lstCI_Vanglai");
					_toastService.ShowSuccess("Đơn hàng đã được tạo thành công, để theo dõi đơn hàng hãy vào mục Lịch sử đơn hàng");
                    return;
				}
                _toastService.ShowError("Tạo đơn hàng thất bại");
            }
        }
        public async Task BackToProduct()
        {
            _navi.NavigateTo("https://localhost:7075/Customer/BanOnline/ShowProduct", true);
        }
        public async Task Bill()
        {
            _navi.NavigateTo("https://localhost:7075/Customer/UserManagement/BillByUser", true);
        }
    }
}