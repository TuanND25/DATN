using DATN_Client.SessionService;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;

namespace DATN_Client.Areas.Customer.Component
{
	public partial class ShowCart
	{
		private HttpClient _client = new HttpClient();
		[Inject] private NavigationManager _navi { get; set; }
		[Inject] public IHttpContextAccessor _ihttpcontextaccessor { get; set; }
		[Inject] Blazored.Toast.Services.IToastService _toastService { get; set; } // Khai báo khi cần gọi ở code-behind
		private List<CartItems_VM> _lstCI = new();
		private List<Image_Join_ProductItem> _lstImg_PI = new();
		private List<Image_Join_ProductItem> _lstImg_PI_tam = new();
		private List<ProductItem_Show_VM> _lstPrI_show_VM = new();
		private ProductItem_Show_VM? _pi_s_vm = new();
		public int? _tongTien { get; set; } = 0;
		public string? _idUser { get; set; } = string.Empty;
		public static string? _note { get; set; } = string.Empty;

		protected override async Task OnInitializedAsync()
		{
			_idUser = _ihttpcontextaccessor.HttpContext.Session.GetString("UserId");
			_lstPrI_show_VM = await _client.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");
			if (_idUser == null) _lstCI = SessionServices.GetLstFromSession_LstCI(_ihttpcontextaccessor.HttpContext.Session, "_lstCI_Vanglai");
			else _lstCI = await _client.GetFromJsonAsync<List<CartItems_VM>>($"https://localhost:7141/api/CartItems/{_idUser}");
			_lstImg_PI = (await _client.GetFromJsonAsync<List<Image_Join_ProductItem>>("https://localhost:7141/api/Image/GetAllImage_PrductItem")).OrderBy(c => c.STT).ToList();
			foreach (var x in _lstCI)
			{
				_pi_s_vm = _lstPrI_show_VM.Where(c => c.Id == x.ProductItemId).FirstOrDefault();
				_tongTien += (x.Quantity * _pi_s_vm.PriceAfterReduction);
			}
			_note = string.Empty;
		}

		public async Task SL_Cong(CartItems_VM ci)
		{

			if (_idUser == null)
			{
				if (ci.Quantity == 99) return;
				ci.Quantity += 1;
				SessionServices.SetLstFromSession_LstCI(_ihttpcontextaccessor.HttpContext.Session, "_lstCI_Vanglai", _lstCI);
			}
			else
			{
				if (ci.Quantity == 99) return;
				ci.Quantity += 1;
				await _client.PutAsJsonAsync("https://localhost:7141/api/CartItems/update-CartItems", ci);
			}
			_tongTien = 0;
			foreach (var x in _lstCI)
			{
				_pi_s_vm = _lstPrI_show_VM.Where(c => c.Id == x.ProductItemId).FirstOrDefault();
				_tongTien += (x.Quantity * _pi_s_vm.PriceAfterReduction);
			}
		}

		public async Task SL_Tru(CartItems_VM ci)
		{
			if (_idUser == null)
			{
				if (ci.Quantity == 1) await DeleteCI(ci);
				ci.Quantity -= 1;
				SessionServices.SetLstFromSession_LstCI(_ihttpcontextaccessor.HttpContext.Session, "_lstCI_Vanglai", _lstCI);
			}
			else
			{
				if (ci.Quantity == 1) await DeleteCI(ci);
				ci.Quantity -= 1;
				await _client.PutAsJsonAsync("https://localhost:7141/api/CartItems/update-CartItems", ci);
			}
			await _client.PutAsJsonAsync("https://localhost:7141/api/CartItems/update-CartItems", ci);
			_tongTien = 0;
			foreach (var x in _lstCI)
			{
				_pi_s_vm = _lstPrI_show_VM.Where(c => c.Id == x.ProductItemId).FirstOrDefault();
				_tongTien += (x.Quantity * _pi_s_vm.PriceAfterReduction);
			}
		}

		public async Task DeleteCI(CartItems_VM ci)
		{
			if (_idUser == null)
			{
				_lstCI.Remove(ci);
				SessionServices.SetLstFromSession_LstCI(_ihttpcontextaccessor.HttpContext.Session, "_lstCI_Vanglai", _lstCI);
			}
			else
			{
				var delete = await _client.DeleteAsync($"https://localhost:7141/api/CartItems/delete-CartItems/{ci.Id}");
				_lstCI = await _client.GetFromJsonAsync<List<CartItems_VM>>($"https://localhost:7141/api/CartItems/{_idUser}");
			}
			_tongTien = 0;
			foreach (var x in _lstCI)
			{
				_pi_s_vm = _lstPrI_show_VM.Where(c => c.Id == x.ProductItemId).FirstOrDefault();
				_tongTien += (x.Quantity * _pi_s_vm.PriceAfterReduction);
			}
		}

		public async Task CreateBill()
		{
			if (_tongTien == 0)
			{
				_toastService.ShowError("Giỏ hàng không có sản phẩm nào vui lòng chọn thêm sản phẩm");
				return;
			}
			_navi.NavigateTo("https://localhost:7075/bill-info", true);
		}

		public async Task MuaHang()
		{
			_navi.NavigateTo("/all-product", true);
		}
	}
}