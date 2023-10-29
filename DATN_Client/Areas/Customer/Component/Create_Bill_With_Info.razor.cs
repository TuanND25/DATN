using DATN_Shared.ViewModel;
using DATN_Shared.ViewModel.Momo;
using DATN_Shared.ViewModel.Momo.Order;
using Microsoft.AspNetCore.Components;

namespace DATN_Client.Areas.Customer.Component
{
	public partial class Create_Bill_With_Info
	{
		HttpClient _httpClient = new HttpClient();
		[Inject] NavigationManager _navi { get; set; }
		[Inject] Blazored.SessionStorage.ISessionStorageService _SessionStorageService { get; set; }
		List<User_VM> _lstUser = new List<User_VM>();
		List<CartItems_VM> _lstCI = new List<CartItems_VM>();
		List<Image_VM> _lstImg = new List<Image_VM>();
		List<ProductItem_Show_VM> _lstPrI_show_VM = new List<ProductItem_Show_VM>();
		Bill_VM _bill_vm = new Bill_VM();
		User_VM _user_vm = new User_VM();
		ProductItem_Show_VM _pi_s_vm = new ProductItem_Show_VM();
		OrderInfoModel _ord = new OrderInfoModel();

		public string _UserName { get; set; }
		public string _Email { get; set; }
		public string _sdt { get; set; }
		public int? _tongTienHang { get; set; } = 0;
		protected override async Task OnInitializedAsync()
		{
			_bill_vm.UserId = Guid.Parse(await _SessionStorageService.GetItemAsStringAsync("session"));
			_lstUser = await _httpClient.GetFromJsonAsync<List<User_VM>>("https://localhost:7141/api/user/get-user");
			_user_vm = _lstUser.Where(c => c.Id == _bill_vm.UserId).FirstOrDefault();
			_lstImg = (await _httpClient.GetFromJsonAsync<List<Image_VM>>("https://localhost:7141/api/Image")).OrderBy(c => c.STT).ToList();
			_lstCI = await _httpClient.GetFromJsonAsync<List<CartItems_VM>>($"https://localhost:7141/api/CartItems/{_bill_vm.UserId}");
			_lstPrI_show_VM = await _httpClient.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");
			_UserName = _user_vm.UserName;
			_bill_vm.NumberPhone = _user_vm.PhoneNumber;
			_bill_vm.PaymentMethodId = Guid.Parse("7485dc76-780a-4008-9ddf-f583ab97a30d");
			foreach (var x in _lstCI)
			{
				_pi_s_vm = _lstPrI_show_VM.Where(c => c.Id == x.ProductItemId).FirstOrDefault();
				_tongTienHang += (x.Quantity * _pi_s_vm.CostPrice);
			}
		}
		//public string FullName { get; set; }
		//public string OrderId { get; set; }
		//public string OrderInfo { get; set; }
		//public int Amount { get; set; }
		public async Task Btn_DatHang()
		{
			_ord.OrderId = Guid.NewGuid().ToString();
			_ord.FullName = _UserName;
			_ord.OrderInfo = _bill_vm.Note;
			_ord.Amount = _tongTienHang;
			var reponse = await _httpClient.PostAsJsonAsync("https://localhost:7141/api/Momo/CreatePaymentAsync", _ord);
			var reponse2 = await reponse.Content.ReadFromJsonAsync<MomoCreatePaymentResponseModel>();
			_navi.NavigateTo($"{reponse2.PayUrl}", true);
		}
	}
}
