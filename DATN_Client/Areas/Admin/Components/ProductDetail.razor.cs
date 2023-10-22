using DATN_Client.Areas.Admin.Controllers;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;

namespace DATN_Client.Areas.Admin.Components
{
	public partial class ProductDetail
	{
		HttpClient _client = new HttpClient();
		[Inject] NavigationManager _navigation { get; set; }
		List<ProductItem_Show_VM> _lstPrI_show_VM = new List<ProductItem_Show_VM>();
		List<Image_Join_ProductItem> _lstImg_PI = new List<Image_Join_ProductItem>();
		List<Products_VM> _lstP = new List<Products_VM>();
		public string _path_Tam { get; set; }
		public string _nameP { get; set; }
		public string _nameCate { get; set; }
		public int? _giaMin { get; set; }
		public int? _giaMax { get; set; }
		public string _gia { get; set; }
		public int _soLuong { get; set; } = 1;
		protected override async Task OnInitializedAsync()
		{
			_lstP = await _client.GetFromJsonAsync<List<Products_VM>>("https://localhost:7141/api/product/get_allProduct");
			_lstPrI_show_VM = (await _client.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show")).Where(c => c.ProductId == TestController._idP).ToList();
			_lstImg_PI = (await _client.GetFromJsonAsync<List<Image_Join_ProductItem>>("https://localhost:7141/api/Image/GetAllImage_PrductItem")).Where(c => c.ProductId == TestController._idP).ToList();
			_path_Tam = _lstImg_PI.OrderBy(c => c.STT).Select(c => c.PathImage).FirstOrDefault();
			_nameP = _lstP.Where(c => c.Id == TestController._idP).Select(c => c.Name).FirstOrDefault();
			_nameCate = _lstPrI_show_VM.Select(c => c.CategoryName).FirstOrDefault();
			_giaMin = _lstPrI_show_VM.Min(c => c.CostPrice);
			_giaMax = _lstPrI_show_VM.Max(c => c.CostPrice);
			_gia = _giaMin<_giaMax ? _giaMin?.ToString("#,##0") + "đ - " + _giaMax?.ToString("#,##0") +"đ" : _giaMax?.ToString("#,##0") + "đ";
		}
		public async Task LoadAnh(Guid ID)
		{
			_path_Tam = _lstImg_PI.FirstOrDefault(c => c.Id == ID).PathImage;
		}
		public async Task SL_Cong()
		{
			if (_soLuong == 99) return;
			_soLuong += 1;
		}
		public async Task SL_Tru()
		{
			if (_soLuong == 1) return;
			_soLuong -= 1;
		}
	}
}
