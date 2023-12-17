using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using DATN_Shared.ViewModel;

namespace DATN_Client.Areas.Customer.Component
{
	public partial class SearchProduct
	{
		HttpClient _client = new HttpClient();
		[Inject] NavigationManager _navigation { get; set; }
		[Inject] private IToastService _toastService { get; set; }
		List<ProductItem_Show_VM> _lstPrI_show_VM = new List<ProductItem_Show_VM>();
		List<Image_Join_ProductItem> _lstImg_PI = new List<Image_Join_ProductItem>();
		private string? _searchProduct { get; set; }
		protected override async Task OnInitializedAsync()
		{
            _lstPrI_show_VM = (await _client.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show")).Where(c => c.Status == 1).ToList();
            _lstImg_PI = await _client.GetFromJsonAsync<List<Image_Join_ProductItem>>("https://localhost:7141/api/Image/GetAllImage_PrductItem");
        }

		public async Task SearchPrd()
		{
			if (_searchProduct == null || _searchProduct.Trim() == string.Empty)
			{
				_toastService.ShowError("Vui lòng nhập từ khóa cần tìm kiếm");
				return;
			}
			_navigation.NavigateTo($"/search-product?search={_searchProduct}", true);
		}

		public async void NavProductItem(Guid Id)
		{
			_navigation.NavigateTo($"/product-detail/{Id}", true);
		}
	}
}
