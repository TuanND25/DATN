using DATN_Client.Areas.Customer.Controllers;
using DATN_Client.SessionService;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.JSInterop;

namespace DATN_Client.Areas.Customer.Component
{
	public partial class ProductDetail
	{
		private HttpClient _client = new HttpClient();
		[Inject] private NavigationManager _navigation { get; set; }
		[Inject] public IHttpContextAccessor _ihttpcontextaccessor { get; set; }
		private List<ProductItem_Show_VM> _lstPrI_show_VM = new();
		private List<Image_Join_ProductItem> _lstImg_PI = new();
		private List<Image_Join_ProductItem> _lstImg_PI_tam = new();
		private List<CartItems_VM> _lstCI = new();
		private List<string> _lstColor = new();
		private List<string> _lstSize = new();
		private ProductItem_Show_VM _pi_S_VM = new();
		private Products_VM _p_VM = new();
		private User_VM _user = new();
		public string _path_Tam { get; set; }
		public string _nameP { get; set; }
		public string _nameCate { get; set; }
		public int? _giaMin { get; set; }
		public int? _giaMax { get; set; }
		public string _gia { get; set; }
		public string _giaBanDau { get; set; }
		public int _soLuong { get; set; } = 1;
		public int? _soluongton { get; set; } = 0;
		public int? _percent { get; set; } = 0;
		public string _chonMau { get; set; } = string.Empty;
		public string _chonSize { get; set; } = string.Empty;
		public string? _iduser { get; set; }
		[Inject] Blazored.Toast.Services.IToastService _toastService { get; set; } // Khai báo khi cần gọi ở code-behind
		private ISession? _ss { get; set; }
		List<string> _lstSizeSample = new() { "XS", "S", "M", "L", "XL", "2XL", "3XL", "4XL", "5XL" };
		private ElementReference myTextarea;
		protected override async Task OnInitializedAsync()
		{
			_ss = _ihttpcontextaccessor.HttpContext.Session;
			_iduser = (_ss.GetString("UserId"));
			_lstPrI_show_VM = (await _client.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show")).Where(c => c.ProductId == BanOnlineController._idP).ToList();
			_p_VM = await _client.GetFromJsonAsync<Products_VM>($"https://localhost:7141/api/product/get_product_byid/{BanOnlineController._idP}");
			_lstImg_PI = (await _client.GetFromJsonAsync<List<Image_Join_ProductItem>>("https://localhost:7141/api/Image/GetAllImage_PrductItem")).Where(c => c.ProductId == BanOnlineController._idP).ToList();
			_lstImg_PI_tam = _lstImg_PI; // Ảnh tạm
			_path_Tam = _lstImg_PI_tam.OrderBy(c => c.STT).Select(c => c.PathImage).FirstOrDefault();
			_nameCate = _lstPrI_show_VM.Select(c => c.CategoryName).FirstOrDefault();
			_giaMin = _lstPrI_show_VM.Min(c => c.PriceAfterReduction);
			_giaMax = _lstPrI_show_VM.Max(c => c.PriceAfterReduction);
			_gia = _giaMin < _giaMax ? _giaMin?.ToString("#,##0") + "đ - " + _giaMax?.ToString("#,##0") + "đ" : _giaMax?.ToString("#,##0") + "đ";
			_lstColor = _lstPrI_show_VM.Select(c => c.ColorName).Distinct().OrderBy(c => c).ToList();
			_lstSize = _lstPrI_show_VM.Select(c => c.SizeName).Distinct().ToList();
			_lstSize = _lstSize.OrderBy(c => _lstSizeSample.IndexOf(c)).ToList();
			//_chonMau = _lstColor.FirstOrDefault();
			//await ChonMau(_chonMau);			
		}		

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			await _JsRuntime.InvokeVoidAsync("autoResizeTextarea");
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
			if (_chonMau == mau) return;
			if (string.IsNullOrEmpty(_chonSize))
			{
				_chonMau = mau;
				var lsttam = new List<Image_Join_ProductItem>();
				var lst_chonmau = _lstPrI_show_VM.Where(c => c.ColorName == mau).ToList();
				_soluongton = 0;
				foreach (var x in lst_chonmau)
				{
					var a = _lstImg_PI.Where(c => c.ProductItemId == x.Id);
					lsttam.AddRange(a);
					_soluongton += x.AvaiableQuantity;
				}
				_lstImg_PI_tam = lsttam;
				_path_Tam = lsttam.OrderBy(c => c.STT).Select(c => c.PathImage).FirstOrDefault();
				_gia = _giaMin < _giaMax ? _giaMin?.ToString("#,##0") + "đ - " + _giaMax?.ToString("#,##0") + "đ" : _giaMax?.ToString("#,##0") + "đ";
			}
			else
			{
				_chonMau = mau;
				var lsttam = new List<Image_Join_ProductItem>();
				var lst_chonmau = _lstPrI_show_VM.Where(c => c.ColorName == mau).ToList();
				_soluongton = 0;
				foreach (var x in lst_chonmau)
				{
					var a = _lstImg_PI.Where(c => c.ProductItemId == x.Id);
					lsttam.AddRange(a);
				}
				_lstImg_PI_tam = lsttam;
				_path_Tam = lsttam.OrderBy(c => c.STT).Select(c => c.PathImage).FirstOrDefault();
				_pi_S_VM = _lstPrI_show_VM.Where(c => c.ColorName == _chonMau && c.SizeName == _chonSize).FirstOrDefault();
				if (_pi_S_VM == null)
				{
					_gia = "0đ";
					_soluongton = 0;
					_percent = 0;
					_toastService.ShowError("Biến thể không tồn tại, vui lòng chọn biến thể khác");
					return;
				}
				var prmi = await _client.GetFromJsonAsync<PromotionItem_VM>($"https://localhost:7141/api/PromotionItem/getPromotionItem_Percent_by_productItemID/{_pi_S_VM.Id}");
				_percent = prmi.Percent;
				_gia = _pi_S_VM.PriceAfterReduction?.ToString("#,##0") + "đ";
				_giaBanDau = _pi_S_VM.CostPrice?.ToString("#,##0") + "đ";
				_soluongton = 0;
				_soluongton = _pi_S_VM.AvaiableQuantity;
			}
		}

		public async Task ChonSize(string size)
		{
			_chonSize = size;
			if (_chonMau == string.Empty) return;
			_pi_S_VM = _lstPrI_show_VM.Where(c => c.ColorName == _chonMau && c.SizeName == _chonSize).FirstOrDefault();
			if (_pi_S_VM == null)
			{
				_gia = "0đ";
				_soluongton = 0;
				_percent = 0;
				_toastService.ShowError("Biến thể không tồn tại, vui lòng chọn biến thể khác");
				return;
			}
			var prmi = await _client.GetFromJsonAsync<PromotionItem_VM>($"https://localhost:7141/api/PromotionItem/getPromotionItem_Percent_by_productItemID/{_pi_S_VM.Id}");
			_percent = prmi.Percent;
			_gia = _pi_S_VM.PriceAfterReduction?.ToString("#,##0") + "đ";
			_giaBanDau = _pi_S_VM.CostPrice?.ToString("#,##0") + "đ";
			_soluongton = 0;
			_soluongton = _pi_S_VM.AvaiableQuantity;
		}

		public async Task ThemVaoGiohang()
		{
			if (string.IsNullOrEmpty(_chonSize) || string.IsNullOrEmpty(_chonMau))
			{
				_toastService.ShowError("Vui lòng chọn biến thể");
				return;
			}
			if (_pi_S_VM == null)
			{
				_toastService.ShowError("Biến thể không tồn tại, vui lòng chọn biến thể khác");
				return;
			}
			// call api kiểm tra số lượng ngay khi bấm thêm giỏ
			var checkSl = await _client.GetFromJsonAsync<ProductItem_VM>($"https://localhost:7141/api/productitem/get_all_productitem_byID/{_pi_S_VM.Id}");
			// ko phải vãng lai
			if (_iduser != null)
			{
				// lấy giỏ
				_lstCI = await _client.GetFromJsonAsync<List<CartItems_VM>>($"https://localhost:7141/api/CartItems/{_iduser}");
				if (checkSl.AvaiableQuantity == 0)
				{
					_toastService.ShowError("Số lượng tồn kho không đủ");
					return;
				}
				// có trong giỏ thì cộng
				if (_lstCI.Any(c => c.ProductItemId == _pi_S_VM.Id))
				{
					CartItems_VM ci = _lstCI.FirstOrDefault(c => c.ProductItemId == _pi_S_VM.Id);
					if (ci.Quantity + _soLuong > checkSl.AvaiableQuantity)
					{
						_toastService.ShowError("Số lượng tồn kho không đủ");
						return;
					}
					ci.Quantity += _soLuong;
					var request1 = await _client.PutAsJsonAsync("https://localhost:7141/api/CartItems/update-CartItems", ci);
					if (request1.IsSuccessStatusCode) _toastService.ShowSuccess("Sản phẩm đã được thêm vào giỏ hàng của bạn");
					return;
				}
				// ko thì add
				CartItems_VM cartItems = new CartItems_VM();
				cartItems.Id = Guid.NewGuid();
				cartItems.UserId = Guid.Parse(_iduser);
				cartItems.ProductItemId = _pi_S_VM.Id;
				cartItems.Quantity = _soLuong;
				cartItems.Status = 1;
				var request2 = await _client.PostAsJsonAsync("https://localhost:7141/api/CartItems/add-CartItems", cartItems);
				if (request2.IsSuccessStatusCode) _toastService.ShowSuccess("Sản phẩm đã được thêm vào giỏ hàng của bạn");
			}
			// vãng lai
			if (_iduser == null)
			{
				// lấy giỏ session
				_lstCI = SessionServices.GetLstFromSession_LstCI(_ss, "_lstCI_Vanglai");
				if (checkSl.AvaiableQuantity == 0)
				{
					_toastService.ShowError("Số lượng tồn kho không đủ");
					return;
				}
				// có trong giỏ thì cộng
				if (_lstCI.Any(c => c.ProductItemId == _pi_S_VM.Id))
				{
					CartItems_VM ci = _lstCI.FirstOrDefault(c => c.ProductItemId == _pi_S_VM.Id);
					if (ci.Quantity + _soLuong > checkSl.AvaiableQuantity)
					{
						_toastService.ShowError("Số lượng tồn kho không đủ");
						return;
					}
					ci.Quantity += _soLuong;
					var luuss1 = SessionServices.SetLstFromSession_LstCI(_ss, "_lstCI_Vanglai", _lstCI);
					if (luuss1 == true)
					{
						_toastService.ShowSuccess("Sản phẩm đã được thêm vào giỏ hàng của bạn");
						return;
					}
					else
					{
						_toastService.ShowError("Đã có lỗi xảy ra với Session");
						return;
					}
				}
				// k thì add
				CartItems_VM cartItems = new CartItems_VM
				{
					Id = Guid.NewGuid(),
					UserId = Guid.Parse("ba803e1a-ae0f-40e2-95db-3abe82fa176a"),
					ProductItemId = _pi_S_VM.Id,
					Quantity = _soLuong,
					Status = 1
				};
				_lstCI.Add(cartItems);
				var luuss2 = SessionServices.SetLstFromSession_LstCI(_ss, "_lstCI_Vanglai", _lstCI);
				if (luuss2 == true)
				{
					_toastService.ShowSuccess("Sản phẩm đã được thêm vào giỏ hàng của bạn");
					return;
				}
				else
				{
					_toastService.ShowError("Đã có lỗi xảy ra với Session");
					return;
				}
			}
		}
	}
}