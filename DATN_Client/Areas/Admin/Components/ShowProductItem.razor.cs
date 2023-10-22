using DATN_Client.Areas.Admin.Controllers;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace DATN_Client.Areas.Admin.Components
{
	public partial class ShowProductItem
	{
		HttpClient _client = new HttpClient();
		[Inject] NavigationManager _navigation { get; set; }
		[Inject] TestController _testCTRL { get; set; }
		List<ProductItem_Show_VM> _lstPrI_show_VM = new List<ProductItem_Show_VM>();
		List<Image_Join_ProductItem> _lstImg_PI = new List<Image_Join_ProductItem>();
		List<Products_VM> _lstP = new List<Products_VM>();

		protected override async Task OnInitializedAsync()
		{
			_lstPrI_show_VM = await _client.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");
			_lstImg_PI = await _client.GetFromJsonAsync<List<Image_Join_ProductItem>>("https://localhost:7141/api/Image/GetAllImage_PrductItem");
			_lstP = await _client.GetFromJsonAsync<List<Products_VM>>("https://localhost:7141/api/product/get_allProduct");
		}
		public async Task Detail_P(Guid id)
		{
			_testCTRL.DetailProduct(id);
			_navigation.NavigateTo($"https://localhost:7075/Admin/Test/DetailProduct?id={id}", true);
		}
	}
}
