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
        public int _soLuong { get; set; }
        protected override async Task OnInitializedAsync()
		{
			string id = await _SessionStorageService.GetItemAsStringAsync("session");
			_lstCI = await _client.GetFromJsonAsync<List<CartItems_VM>>($"https://localhost:7141/api/CartItems/{id}");
			_lstImg = (await _client.GetFromJsonAsync<List<Image_VM>>("https://localhost:7141/api/Image")).OrderBy(c=>c.STT).ToList();
			_lstPrI_show_VM = await _client.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");
		}
		public async Task SL_Cong(CartItems_VM ci)
		{
			if (ci.Quantity == 99) return;
			ci.Quantity +=1;
			await _client.PutAsJsonAsync("https://localhost:7141/api/CartItems/update-CartItems", ci);
		}
		public async Task SL_Tru(CartItems_VM ci)
		{
			if (ci.Quantity == 1) return;
			ci.Quantity -= 1;
			await _client.PutAsJsonAsync("https://localhost:7141/api/CartItems/update-CartItems", ci);
		}
		public async Task DeleteCI(CartItems_VM ci)
		{
			var x = await _client.DeleteAsync($"https://localhost:7141/api/CartItems/delete-CartItems/{ci.Id}");
			string id = await _SessionStorageService.GetItemAsStringAsync("session");
			_lstCI = await _client.GetFromJsonAsync<List<CartItems_VM>>($"https://localhost:7141/api/CartItems/{id}");
		}
	}
}
