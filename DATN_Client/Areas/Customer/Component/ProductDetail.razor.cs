using Blazored.SessionStorage;
using DATN_Client.Areas.Customer.Controllers;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;

namespace DATN_Client.Areas.Customer.Component
{
	public partial class ProductDetail
	{
		HttpClient _client = new HttpClient();
		[Inject] NavigationManager _navigation { get; set; }
		[Inject] Blazored.SessionStorage.ISessionStorageService _SessionStorageService { get; set; }
		List<ProductItem_Show_VM> _lstPrI_show_VM = new List<ProductItem_Show_VM>();
		List<Image_Join_ProductItem> _lstImg_PI = new List<Image_Join_ProductItem>();
		List<Image_Join_ProductItem> _lstImg_PI_tam = new List<Image_Join_ProductItem>();
		List<Products_VM> _lstP = new List<Products_VM>();
		List<string> _lstColor = new List<string>();
		List<string> _lstSize = new List<string>();
		ProductItem_Show_VM _pi_S_VM = new ProductItem_Show_VM();
		User_VM _user = new User_VM();
		public string _path_Tam { get; set; }
		public string _nameP { get; set; }
		public string _nameCate { get; set; }
		public int? _giaMin { get; set; }
		public int? _giaMax { get; set; }
		public string _gia { get; set; }
		public int _soLuong { get; set; } = 1;
		public string _chonMau { get; set; }
		public string _chonSize { get; set; }
		protected override async Task OnInitializedAsync()
		{
			_lstP = await _client.GetFromJsonAsync<List<Products_VM>>("https://localhost:7141/api/product/get_allProduct");
			_lstPrI_show_VM = (await _client.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show")).Where(c => c.ProductId == BanOnlineController._idP).ToList();
			_lstImg_PI = (await _client.GetFromJsonAsync<List<Image_Join_ProductItem>>("https://localhost:7141/api/Image/GetAllImage_PrductItem")).Where(c => c.ProductId == BanOnlineController._idP).ToList();
			_lstImg_PI_tam = _lstImg_PI; // Ảnh tạm
			_path_Tam = _lstImg_PI_tam.OrderBy(c => c.STT).Select(c => c.PathImage).FirstOrDefault();
			_nameP = _lstP.Where(c => c.Id == BanOnlineController._idP).Select(c => c.Name).FirstOrDefault();
			_nameCate = _lstPrI_show_VM.Select(c => c.CategoryName).FirstOrDefault();
			_giaMin = _lstPrI_show_VM.Min(c => c.CostPrice);
			_giaMax = _lstPrI_show_VM.Max(c => c.CostPrice);
			_gia = _giaMin < _giaMax ? _giaMin?.ToString("#,##0") + "đ - " + _giaMax?.ToString("#,##0") + "đ" : _giaMax?.ToString("#,##0") + "đ";
			_lstColor = _lstPrI_show_VM.Select(c => c.ColorName).Distinct().ToList();
			_lstSize = _lstPrI_show_VM.Select(c => c.SizeName).Distinct().ToList();
			//_user = (await _client.GetFromJsonAsync<List<User_VM>>("https://localhost:7141/api/user/get-user")).FirstOrDefault(c => c.Id == _idUser);
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
		public async Task ChonMau(string mau)
		{
			_chonMau = mau;
			_lstImg_PI = (await _client.GetFromJsonAsync<List<Image_Join_ProductItem>>("https://localhost:7141/api/Image/GetAllImage_PrductItem")).Where(c => c.ProductId == BanOnlineController._idP).ToList();
			_lstImg_PI_tam.Clear();
			var lst_chonmau = _lstPrI_show_VM.Where(c => c.ColorName == mau).ToList();
			foreach (var x in lst_chonmau)
			{
				var a = _lstImg_PI.Where(c => c.ProductItemId == x.Id);
				_lstImg_PI_tam.AddRange(a);
			}
			_path_Tam = _lstImg_PI_tam.OrderBy(c => c.STT).Select(c => c.PathImage).FirstOrDefault();
			_gia = _giaMin < _giaMax ? _giaMin?.ToString("#,##0") + "đ - " + _giaMax?.ToString("#,##0") + "đ" : _giaMax?.ToString("#,##0") + "đ";
			_chonSize = string.Empty;
			_lstSize = lst_chonmau.Select(c => c.SizeName).Distinct().ToList();
		}
		public async Task ChonSize(string size)
		{
			_chonSize = size;
			_pi_S_VM = _lstPrI_show_VM.Where(c => c.ColorName == _chonMau && c.SizeName == _chonSize).FirstOrDefault();
			_gia = _pi_S_VM.CostPrice?.ToString("#,##0") + "đ";
		}
		public async Task ThemVaoGiohang()
		{
			var iduser = Guid.Parse(await _SessionStorageService.GetItemAsStringAsync("session"));
			var x = _pi_S_VM;
			CartItems_VM cartItems = new CartItems_VM();
			cartItems.Id = Guid.NewGuid();
			cartItems.UserId = iduser;
			cartItems.ProductItemId = _pi_S_VM.Id;
			cartItems.Quantity = _soLuong;
			cartItems.Status = 1;
			var request = await _client.PostAsJsonAsync("https://localhost:7141/api/CartItems/add-CartItems",cartItems);
		}
	}
}
