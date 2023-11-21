using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using DATN_Client.Areas.Customer.Controllers;
using System.Globalization;
using System.Text;

namespace DATN_Client.Areas.Admin.Components
{
	public partial class ProductItemManager
	{
		HttpClient _client = new HttpClient();
		[Inject] NavigationManager _navigation { get; set; }
		[Inject] IJSRuntime JsRuntime { get; set; }
		[Inject] Blazored.Toast.Services.IToastService _toastService { get; set; } // Khai báo khi cần gọi ở code-behind
		List<ProductItem_Show_VM> _lstPrI_show_VM = new List<ProductItem_Show_VM>();
		List<Image_VM> _lstImg = new List<Image_VM>();
		List<Products_VM> _lstP = new List<Products_VM>();
		List<Color_VM> _lstC = new List<Color_VM>();
		List<Size_VM> _lstS = new List<Size_VM>();
		List<Categories_VM> _lstCate = new List<Categories_VM>();
		private List<Image_Join_ProductItem> _lstImg_PI = new List<Image_Join_ProductItem>();
		private List<Image_Join_ProductItem> _lstImg_PI_tam = new List<Image_Join_ProductItem>();
		ProductItem_VM _PI_VM = new ProductItem_VM();
		Products_VM _P_VM = new Products_VM();
		Categories_VM _Cate_VM = new Categories_VM();
		Color_VM _C_VM = new Color_VM();
		Size_VM _S_VM = new Size_VM();
		IBrowserFile _file { get; set; }
		public Guid _idPI { get; set; }
		public string _pathImg { get; set; }
		List<Image_VM> _lstImg_Tam = new List<Image_VM>();
		List<Image_VM> _lstImg_Tam_Them = new List<Image_VM>();
		List<Image_VM> _lstImg_Tam_Xoa = new List<Image_VM>();
		List<Image_VM> _lstImg_Tam_Sua = new List<Image_VM>();
		public Guid _idPI_Tam { get; set; }
		public Guid _idImg_Tam { get; set; }
		ProductItem_Show_VM _PM_S_VM = new ProductItem_Show_VM();
		//public Guid Id { get; set; }
		//public Guid ProductId { get; set; }
		//public Guid ColorId { get; set; }
		//public Guid SizeId { get; set; }
		//public int AvaiableQuantity { get; set; }
		//public int PurchasePrice { get; set; }
		//public int CostPrice { get; set; }
		//public int Status { get; set; }
		bool isModalOpenAddProduct = false;
		bool isModalOpenAddCate = false;
		bool isModalOpenAddColor = false;
		bool isModalOpenAddSize = false;
		List<string> _lstSizeSample = new List<string> { "XS", "S", "M", "L", "XL", "2XL", "3XL", "4XL", "5XL" };
		private string XoaDau(string text)
		{
			string normalizedString = text.Normalize(NormalizationForm.FormD);
			StringBuilder stringBuilder = new StringBuilder();

			foreach (char c in normalizedString)
			{
				UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
				if (unicodeCategory != UnicodeCategory.NonSpacingMark)
				{
					stringBuilder.Append(c);
				}
			}

			return stringBuilder.ToString().Normalize(NormalizationForm.FormC).ToLower();
		}
		protected override async Task OnInitializedAsync()
		{
			_lstP = await _client.GetFromJsonAsync<List<Products_VM>>("https://localhost:7141/api/product/get_allProduct");
			_lstP = _lstP.OrderBy(c => c.Name).ToList();
			_lstCate = await _client.GetFromJsonAsync<List<Categories_VM>>("https://localhost:7141/api/Categories");
			_lstCate = _lstCate.OrderBy(c => c.Name).ToList();
			_lstC = await _client.GetFromJsonAsync<List<Color_VM>>("https://localhost:7141/api/Color/get_color");
			_lstC = _lstC.OrderBy(c => c.Name).ToList();
			_lstS = await _client.GetFromJsonAsync<List<Size_VM>>("https://localhost:7141/api/Size/get_size");
			_lstS = _lstS.OrderBy(c => _lstSizeSample.IndexOf(c.Name)).ToList();
			_lstImg = await _client.GetFromJsonAsync<List<Image_VM>>("https://localhost:7141/api/Image");
			_lstImg_PI = await _client.GetFromJsonAsync<List<Image_Join_ProductItem>>("https://localhost:7141/api/Image/GetAllImage_PrductItem");
			_lstPrI_show_VM = await _client.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");
			_lstPrI_show_VM = _lstPrI_show_VM.OrderBy(c => c.Name).ToList();
			_idPI_Tam = Guid.NewGuid();
		}
		public async Task ChangeEv(InputFileChangeEventArgs e)
		{
			Image_VM imgTam = new Image_VM();
			_file = e.File;
			if (_file != null)
			{
				// Trỏ tới thư mục wwwroot để lát nữa thực hiện việc copy sang
				var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", _file.Name);
				using (var stream = new FileStream(path, FileMode.Create))
				{
					// Thực hiện copy ảnh vừa chọn sang thư mục mới (wwwroot)
					try
					{

						await _file.OpenReadStream(2048 * 1024).CopyToAsync(stream);
					}
					catch (Exception)
					{

						_toastService.ShowError("Ảnh có kích thước quá lớn, vui lòng chọn ảnh khác");
						return;
					}
				}
				// Gán lại giá trị cho Description của đối tượng bằng tên file ảnh đã đưuọc sao chép
				imgTam.PathImage = _file.Name;
				_pathImg = _file.Name;
				imgTam.Id = Guid.NewGuid();
				_idImg_Tam = imgTam.Id;
				imgTam.Name = "";
				if (_lstImg_Tam.Count == 0) imgTam.STT = 1;
				else imgTam.STT = _lstImg_Tam.Count == 0
							? _lstImg.Max(c => c.STT) + 1
							: (_lstImg.Max(c => c.STT) > _lstImg_Tam.Max(c => c.STT)
							? _lstImg.Max(c => c.STT) + 1
							: _lstImg_Tam.Max(c => c.STT) + 1);
				if (_idPI.ToString() == "00000000-0000-0000-0000-000000000000") imgTam.ProductItemId = _idPI_Tam;
				else imgTam.ProductItemId = _idPI;
				imgTam.Status = 1;
				_lstImg_Tam_Them.Add(imgTam);
				_lstImg_Tam.Add(imgTam);
				_toastService.ShowSuccess("Ảnh đã được tải lên thành công");
			}
		}
		public async Task ChangeImg(InputFileChangeEventArgs e)
		{
			Image_VM imgTam = _lstImg.Where(c => c.Id == _idImg_Tam).FirstOrDefault();
			if (imgTam == null) imgTam = _lstImg_Tam_Them.Where(c => c.Id == _idImg_Tam).FirstOrDefault();

			_file = e.File;
			if (_file != null)
			{
				// Trỏ tới thư mục wwwroot để lát nữa thực hiện việc copy sang
				var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", _file.Name);
				using (var stream = new FileStream(path, FileMode.Create))
				{
					// Thực hiện copy ảnh vừa chọn sang thư mục mới (wwwroot)
					try
					{
						await _file.OpenReadStream(2048 * 1024).CopyToAsync(stream);
					}
					catch (Exception)
					{

						_toastService.ShowError("Ảnh có kích thước quá lớn, vui lòng chọn ảnh khác");
						return;
					}
				}
				// Gán lại giá trị cho Description của đối tượng bằng tên file ảnh đã đưuọc sao chép
				imgTam.PathImage = _file.Name;
				_pathImg = _file.Name;
				if (!_lstImg_Tam_Sua.Any(c => c.Id == imgTam.Id)) _lstImg_Tam_Sua.Add(imgTam);
				_toastService.ShowSuccess("Thay đổi ảnh thành công");
			}
		}
		public async Task Add_PI()
		{
			_PI_VM.Id = _idPI_Tam;

			var a = await _client.PostAsJsonAsync("https://localhost:7141/api/productitem/add_productitem", _PI_VM);
			if (a.IsSuccessStatusCode)
			{
				foreach (var x in _lstImg_Tam)
				{
					await _client.PostAsJsonAsync("https://localhost:7141/api/Image/Post-Image", x);
				}
				_toastService.ShowSuccess("Thêm thành công");
				await Task.Delay(3000);
				_navigation.NavigateTo("Admin/ProductItem", true);
			}
		}

		public async Task Update_PI()
		{
			var a = await _client.PutAsJsonAsync("https://localhost:7141/api/productitem/update_productitem", _PI_VM);
			if (a.IsSuccessStatusCode)
			{
				var lstbd = _lstImg.Where(c => c.ProductItemId == _idPI).ToList();
				if (_lstImg_Tam_Them.Count > 0)
				{
					foreach (var x in _lstImg_Tam)
					{
						await _client.PostAsJsonAsync("https://localhost:7141/api/Image/Post-Image", x);
					}
				}
				if (_lstImg_Tam_Xoa.Count > 0)
				{
					foreach (var x in _lstImg_Tam_Xoa)
					{
						await _client.DeleteAsync($"https://localhost:7141/api/Image/Delete-Image/{x.Id}");
					}
				}
				if (_lstImg_Tam_Sua.Count > 0)
				{
					foreach (var x in _lstImg_Tam_Sua)
					{
						await _client.PutAsJsonAsync($"https://localhost:7141/api/Image/Put-Image", x);
					}
				}
				_toastService.ShowSuccess("Cập nhật thành công");
				await Task.Delay(3000);
				_navigation.NavigateTo("Admin/ProductItem", true);
			}
		}
		//public Guid Id { get; set; }
		//public Guid? ReviewId { get; set; }
		//public string Name { get; set; }
		//public int STT { get; set; }
		//public string PathImage { get; set; }
		//public Guid ProductItemId { get; set; }
		//public int Status { get; set; }
		public async Task LoadUpdate(ProductItem_Show_VM pi)
		{
			_pathImg = null;
			_idPI = pi.Id;
			_PI_VM.Id = pi.Id;
			_PI_VM.ProductId = pi.ProductId;
			_PI_VM.ColorId = pi.ColorId;
			_PI_VM.SizeId = pi.SizeId;
			_PI_VM.CategoryId = pi.CategoryID;
			_PI_VM.AvaiableQuantity = pi.AvaiableQuantity;
			_PI_VM.PriceAfterReduction = pi.PriceAfterReduction;
			_PI_VM.CostPrice = pi.CostPrice;
			_PI_VM.Status = pi.Status;
			//_lstImg_PI = (await _client.GetFromJsonAsync<List<Image_Join_ProductItem>>("https://localhost:7141/api/Image/GetAllImage_PrductItem")).Where(c => c.ProductId == _PI_VM.ProductId).ToList();
			var lst_chonmau = _lstPrI_show_VM.Where(c => c.ColorId == _PI_VM.ColorId && c.ProductId == _PI_VM.ProductId).ToList();
			_lstImg_PI_tam.Clear();
			foreach (var x in lst_chonmau)
			{
				var a = _lstImg_PI.Where(c => c.ProductItemId == x.Id);
				_lstImg_PI_tam.AddRange(a);
			}
			_lstImg_Tam.Clear();
			foreach (var item in _lstImg_PI_tam)
			{
				Image_VM image_VM = new Image_VM();
				image_VM.Id = item.Id;
				image_VM.ReviewId = item.ReviewId;
				image_VM.Name = item.Name;
				image_VM.STT = item.STT;
				image_VM.PathImage = item.PathImage;
				image_VM.ProductItemId = item.ProductItemId;
				image_VM.Status = item.Status;
				_lstImg_Tam.Add(image_VM);
			}
			//_lstImg_Tam = _lstImg.Where(c => c.ProductItemId == _idPI).OrderBy(c => c.STT).ToList();
			_idImg_Tam = _lstImg_Tam.OrderBy(c => c.STT).Select(c => c.Id).FirstOrDefault();
			_pathImg = _lstImg.Where(c => c.Id == _idImg_Tam).Select(c => c.PathImage).FirstOrDefault();
			await JsRuntime.InvokeVoidAsync("OnScrollEvent");
		}
		public async Task Add_P()
		{
			if (_P_VM.Name == string.Empty || _P_VM.Name == null)
			{
				_toastService.ShowError("Không được để trống");
				return;
			}
			if (_lstP.Any(c => c.Name.ToLower() == _P_VM.Name.ToLower()))
			{
				_toastService.ShowError("Tên đã tồn tại");
				return;
			}
			_P_VM.Status = 1;
			var x = await _client.PostAsJsonAsync("https://localhost:7141/api/product/add_product", _P_VM);
			_lstP = await _client.GetFromJsonAsync<List<Products_VM>>("https://localhost:7141/api/product/get_allProduct");
			if (x.IsSuccessStatusCode)
			{
				_toastService.ShowSuccess("Thêm thành công");
				ClosePopup("AddProduct");
			}
			else _toastService.ShowError("Thêm không thành công");
		}
		public async Task Add_Cate()
		{
			if (_Cate_VM.Name == string.Empty || _Cate_VM.Name == null)
			{
				_toastService.ShowError("Không được để trống");
				return;
			}
			if (_lstCate.Any(c => c.Name.ToLower() == _Cate_VM.Name.ToLower()))
			{
				_toastService.ShowError("Thể loại đã tồn tại");
				return;
			}
			_Cate_VM.Status = 1;
			var x = await _client.PostAsJsonAsync("https://localhost:7141/api/Categories/PostCategory", _Cate_VM);
			_lstCate = await _client.GetFromJsonAsync<List<Categories_VM>>("https://localhost:7141/api/Categories");
			if (x.IsSuccessStatusCode)
			{
				_toastService.ShowSuccess("Thêm thành công");
				ClosePopup("AddCate");
			}
			else _toastService.ShowError("Thêm không thành công");
		}
		public async Task Add_C()
		{
			if (_C_VM.Name == string.Empty || _C_VM.Name == null)
			{
				_toastService.ShowError("Không được để trống");
				return;
			}
			if (_lstC.Any(c => c.Name.ToLower() == _C_VM.Name.ToLower()))
			{
				_toastService.ShowError("Màu sắc đã tồn tại");
				return;
			}
			_C_VM.Status = 1;
			var x = await _client.PostAsJsonAsync("https://localhost:7141/api/Color/PostColor", _C_VM);
			_lstC = await _client.GetFromJsonAsync<List<Color_VM>>("https://localhost:7141/api/Color");
			if (x.IsSuccessStatusCode)
			{
				_toastService.ShowSuccess("Thêm thành công");
				ClosePopup("AddColor");
			}
			else _toastService.ShowError("Thêm không thành công");
		}
		public async Task Add_S()
		{
			if (_S_VM.Name == string.Empty || _S_VM.Name == null)
			{
				_toastService.ShowError("Không được để trống");
				return;
			}
			if (_lstS.Any(c => c.Name.ToLower() == _S_VM.Name.ToLower()))
			{
				_toastService.ShowError("Kích thước đã tồn tại");
				return;
			}
			_S_VM.Status = 1;
			_S_VM.Name = _S_VM.Name.ToUpper();
			var x = await _client.PostAsJsonAsync("https://localhost:7141/api/Size/PostSize", _S_VM);
			_lstS = await _client.GetFromJsonAsync<List<Size_VM>>("https://localhost:7141/api/Size");
			if (x.IsSuccessStatusCode)
			{
				_toastService.ShowSuccess("Thêm thành công");
				ClosePopup("AddSize");
			}
			else _toastService.ShowError("Thêm không thành công");
		}

		public async Task LoadAnh(Guid id)
		{
			_idImg_Tam = id;
			_pathImg = _lstImg_Tam.FirstOrDefault(c => c.Id == id).PathImage;

		}
		public async Task Delete_Img_Tam()
		{
			Image_VM imgTam = new Image_VM();
			var imgVuaXoa = _lstImg_Tam.FirstOrDefault(c => c.Id == _idImg_Tam);
			_lstImg_Tam_Xoa.Add(imgVuaXoa);
			_lstImg_Tam.Remove(imgVuaXoa);
			_lstImg_Tam_Them.Remove(imgVuaXoa);
			_lstImg_Tam_Sua.Remove(imgVuaXoa); // Moi nhat
			_idImg_Tam = _lstImg_Tam.Select(c => c.Id).FirstOrDefault();
			if (_idPI.ToString() != "00000000-0000-0000-0000-000000000000")
			{
				_pathImg = _lstImg_Tam.Where(c => c.ProductItemId == _idPI).Select(c => c.PathImage).FirstOrDefault();
			}
			else
			{
				_pathImg = _lstImg_Tam.Select(c => c.PathImage).FirstOrDefault();
			}
			_toastService.ShowSuccess("Ảnh đã được xóa thành công");
		}
		public async Task LocHangLoat()
		{
			_lstPrI_show_VM = await _client.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");

			_lstPrI_show_VM = _lstPrI_show_VM.Where(c =>
								(_PM_S_VM.CategoryName == null ||
								_PM_S_VM.CategoryName == "0" ||
								c.CategoryName == _PM_S_VM.CategoryName) &&
								(_PM_S_VM.SizeName == null ||
								_PM_S_VM.SizeName == "0" ||
								c.SizeName == _PM_S_VM.SizeName) &&
								(_PM_S_VM.ColorName == null ||
								_PM_S_VM.ColorName == "0" ||
								c.ColorName == _PM_S_VM.ColorName)).OrderBy(c=>c.Name).ToList();
		}
		public async Task TimKiem()
		{
			_lstPrI_show_VM = await _client.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");

			if (_PM_S_VM.Name.ToLower() != XoaDau(_PM_S_VM.Name))
			{
				_lstPrI_show_VM = _lstPrI_show_VM.Where(c =>
								_PM_S_VM.Name == null ||
								_PM_S_VM.Name == string.Empty ||
								c.Name.Trim().ToLower().Contains(_PM_S_VM.Name.Trim().ToLower())).ToList();
			}
			else
			{
				_lstPrI_show_VM = _lstPrI_show_VM.Where(c =>
								_PM_S_VM.Name == null ||
								_PM_S_VM.Name == string.Empty ||
								XoaDau(c.Name.Trim()).Contains(XoaDau(_PM_S_VM.Name.Trim()))).ToList();
			}
			
		}
		private void SetModalState(bool isOpen, string modalType)
		{
			switch (modalType)
			{
				case "AddProduct":
					isModalOpenAddProduct = isOpen;
					break;
				case "AddCate":
					isModalOpenAddCate = isOpen;
					break;
				case "AddColor":
					isModalOpenAddColor = isOpen;
					break;
				case "AddSize":
					isModalOpenAddSize = isOpen;
					break;
				default:
					break;
			}
		}

		private void OpenPopup(string modalType)
		{
			SetModalState(true, modalType);
		}

		private void ClosePopup(string modalType)
		{
			SetModalState(false, modalType);
		}
		public async Task MoAddP()
		{
			_P_VM.Name = string.Empty;
			OpenPopup("AddProduct");
		}
		public async Task MoAddCate()
		{
			_Cate_VM.Name = string.Empty;
			OpenPopup("AddCate");
		}
		public async Task MoAddColor()
		{
			_C_VM.Name = string.Empty;
			OpenPopup("AddColor");
		}
		public async Task MoAddSize()
		{
			_S_VM.Name = string.Empty;
			OpenPopup("AddSize");
		}
	}
}
