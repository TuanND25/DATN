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
			_lstPrI_show_VM = await _client.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7264/api/ProductItem/show");
			_lstImg_PI = await _client.GetFromJsonAsync<List<Image_Join_ProductItem>>("https://localhost:7264/api/image/get_Image_Join_PI");
		}

		public async Task SearchPrd()
		{
			if (_searchProduct == null || _searchProduct.Trim() == string.Empty)
			{
				_toastService.ShowError("Vui lòng nhập từ khóa cần tìm kiếm");
				return;
			}
			_navigation.NavigateTo($"https://localhost:7075/search/{_searchProduct}", true);
		}
	}
}
