using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;

namespace DATN_Client.Areas.Customer.Component
{
	public partial class ShowCart
	{
		HttpClient _client = new HttpClient();
		[Inject] NavigationManager _navi { get; set; }
		[Inject] Blazored.SessionStorage.ISessionStorageService _SessionStorageService { get; set; }

		List<CartItems_VM> _lstCI = new List<CartItems_VM>();
		List<Image_VM> _lstImg = new List<Image_VM>();
		List<ProductItem_Show_VM> _lstPrI_show_VM = new List<ProductItem_Show_VM>();
		ProductItem_Show_VM _pi_s_vm = new ProductItem_Show_VM();
		public int? _tongTien { get; set; } = 0;
		public string? _idUser { get; set; } = string.Empty;
		public static string? _note { get; set; } = string.Empty;
		protected override async Task OnInitializedAsync()
		{
			_idUser = await _SessionStorageService.GetItemAsStringAsync("session");
			if (_idUser == null) return;
			_lstCI = await _client.GetFromJsonAsync<List<CartItems_VM>>($"https://localhost:7141/api/CartItems/{_idUser}");
			_lstImg = (await _client.GetFromJsonAsync<List<Image_VM>>("https://localhost:7141/api/Image")).OrderBy(c => c.STT).ToList();
			_lstPrI_show_VM = await _client.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");
			foreach (var x in _lstCI)
			{
				_pi_s_vm = _lstPrI_show_VM.Where(c => c.Id == x.ProductItemId).FirstOrDefault();
				_tongTien += (x.Quantity * _pi_s_vm.CostPrice);
			}
			_note = string.Empty;
		}
		public async Task SL_Cong(CartItems_VM ci)
		{
			if (ci.Quantity == 99) return;
			ci.Quantity += 1;
			await _client.PutAsJsonAsync("https://localhost:7141/api/CartItems/update-CartItems", ci);
			_tongTien = 0;
			foreach (var x in _lstCI)
			{
				_pi_s_vm = _lstPrI_show_VM.Where(c => c.Id == x.ProductItemId).FirstOrDefault();
				_tongTien += (x.Quantity * _pi_s_vm.CostPrice);
			}
		}
		public async Task SL_Tru(CartItems_VM ci)
		{
			if (ci.Quantity == 1) return;
			ci.Quantity -= 1;
			await _client.PutAsJsonAsync("https://localhost:7141/api/CartItems/update-CartItems", ci);
			_tongTien = 0;
			foreach (var x in _lstCI)
			{
				_pi_s_vm = _lstPrI_show_VM.Where(c => c.Id == x.ProductItemId).FirstOrDefault();
				_tongTien += (x.Quantity * _pi_s_vm.CostPrice);
			}
		}
		public async Task DeleteCI(CartItems_VM ci)
		{
			var delete = await _client.DeleteAsync($"https://localhost:7141/api/CartItems/delete-CartItems/{ci.Id}");
			string id = await _SessionStorageService.GetItemAsStringAsync("session");
			_lstCI = await _client.GetFromJsonAsync<List<CartItems_VM>>($"https://localhost:7141/api/CartItems/{id}");
			_tongTien = 0;
			foreach (var x in _lstCI)
			{
				_pi_s_vm = _lstPrI_show_VM.Where(c => c.Id == x.ProductItemId).FirstOrDefault();
				_tongTien += (x.Quantity * _pi_s_vm.CostPrice);
			}
		}
		public async Task CreateBill()
		{
			if (_tongTien == 0) return;
			_navi.NavigateTo("https://localhost:7075/bill-info", true);
		}
		public async Task MuaHang()
		{
			_navi.NavigateTo("https://localhost:7075/Customer/BanOnline/ShowProduct", true);
		}
	}
}
