using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DATN_Client.Areas.Admin.Components
{
	public partial class ProductItemManager
	{
		HttpClient _client = new HttpClient();
		[Inject] NavigationManager _navigation { get; set; }
		List<ProductItem_Show_VM> _lstPrI_show_VM = new List<ProductItem_Show_VM>();
		List<Image_VM> _lstImg = new List<Image_VM>();
		List<Products_VM> _lstP = new List<Products_VM>();
		List<Color_VM> _lstC = new List<Color_VM>();
		List<Size_VM> _lstS = new List<Size_VM>();
		List<Categories_VM> _lstCate = new List<Categories_VM>();
		ProductItem_VM _PI_VM = new ProductItem_VM();
		Products_VM _P_VM = new Products_VM();
		Categories_VM _Cate_VM = new Categories_VM();
		Color_VM _C_VM = new Color_VM();
		Size_VM _S_VM = new Size_VM();
		Image_VM _I_VM = new Image_VM();
		IBrowserFile _file { get; set; }
		public Guid _idPI { get; set; }
		public string _pathImg { get; set; }
		List<Image_VM> _lstImg_Tam = new List<Image_VM>();
		List<Image_VM> _lstImg_Tam_Them = new List<Image_VM>();
		List<Image_VM> _lstImg_Tam_Xoa = new List<Image_VM>();
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
		protected override async Task OnInitializedAsync()
		{
			_lstP = await _client.GetFromJsonAsync<List<Products_VM>>("https://localhost:7141/api/product/get_allProduct");
			_lstCate = await _client.GetFromJsonAsync<List<Categories_VM>>("https://localhost:7141/api/Categories");
			_lstC = await _client.GetFromJsonAsync<List<Color_VM>>("https://localhost:7141/api/Color");
			_lstS = await _client.GetFromJsonAsync<List<Size_VM>>("https://localhost:7141/api/Size");
			_lstImg = await _client.GetFromJsonAsync<List<Image_VM>>("https://localhost:7141/api/Image");
			_lstPrI_show_VM = await _client.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");
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
					await _file.OpenReadStream().CopyToAsync(stream);
				}
				// Gán lại giá trị cho Description của đối tượng bằng tên file ảnh đã đưuọc sao chép
				imgTam.PathImage = _file.Name;
				_pathImg = _file.Name;
				imgTam.Id = Guid.NewGuid();
				_idImg_Tam = imgTam.Id;
				imgTam.Name = "";
				if (_idPI.ToString() == "00000000-0000-0000-0000-000000000000") imgTam.ProductItemId = _idPI_Tam;
				else imgTam.ProductItemId = _idPI;
				imgTam.Status = 1;
				_lstImg_Tam_Them.Add(imgTam);
				_lstImg_Tam.Add(imgTam);
			}
		}
		public async Task Add_PI()
		{
			_PI_VM.Id = _idPI_Tam;

			var a = await _client.PostAsJsonAsync("https://localhost:7141/api/productitem/add_productitem", _PI_VM);
			foreach (var x in _lstImg_Tam)
			{
				await _client.PostAsJsonAsync("https://localhost:7141/api/Image/Post-Image", x);
			}
			if (a.IsSuccessStatusCode)
			{
				_navigation.NavigateTo("Admin/ProductItem", true);
			}
		}
		
		public async Task Update_PI()
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

			var a= await _client.PutAsJsonAsync("https://localhost:7141/api/productitem/update_productitem", _PI_VM);
			if (a.IsSuccessStatusCode)
			{
				_navigation.NavigateTo("Admin/ProductItem", true);
			}
		}
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
			_PI_VM.PurchasePrice = pi.PurchasePrice;
			_PI_VM.CostPrice = pi.CostPrice;
			_PI_VM.Status = pi.Status;
			_lstImg_Tam = _lstImg.Where(c => c.ProductItemId == _idPI).ToList();
			_idImg_Tam = _lstImg_Tam.Select(c => c.Id).FirstOrDefault();
			_pathImg = _lstImg.Where(c => c.ProductItemId == pi.Id).Select(c => c.PathImage).FirstOrDefault();
			await JsRuntime.InvokeVoidAsync("OnScrollEvent");
		}
		public async Task Add_P()
		{
			var x = await _client.PostAsJsonAsync("https://localhost:7141/api/product/add_product", _P_VM);
			_lstP = await _client.GetFromJsonAsync<List<Products_VM>>("https://localhost:7141/api/product/get_allProduct");
		}
		public async Task Add_Cate()
		{
			var x = await _client.PostAsJsonAsync("https://localhost:7141/api/Categories/PostCategory", _Cate_VM);
			_lstCate = await _client.GetFromJsonAsync<List<Categories_VM>>("https://localhost:7141/api/Categories");
		}
		public async Task Add_C()
		{
			var x = await _client.PostAsJsonAsync("https://localhost:7141/api/Color/PostColor", _C_VM);
			_lstC = await _client.GetFromJsonAsync<List<Color_VM>>("https://localhost:7141/api/Color");
		}
		public async Task Add_S()
		{
			var x = await _client.PostAsJsonAsync("https://localhost:7141/api/Size/PostSize", _S_VM);
			_lstS = await _client.GetFromJsonAsync<List<Size_VM>>("https://localhost:7141/api/Size");
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
			_idImg_Tam = _lstImg_Tam.Select(c => c.Id).FirstOrDefault();
			if (_idPI.ToString() != "00000000-0000-0000-0000-000000000000")
			{
				_pathImg = _lstImg_Tam.Where(c => c.ProductItemId == _idPI).Select(c => c.PathImage).FirstOrDefault();
			}
			else
			{
				_pathImg = _lstImg_Tam.Select(c => c.PathImage).FirstOrDefault();
			}
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
								c.ColorName == _PM_S_VM.ColorName)).ToList();
		}
		public async Task TimKiem()
		{
			_lstPrI_show_VM = await _client.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");

			_lstPrI_show_VM = _lstPrI_show_VM.Where(c =>
								_PM_S_VM.Name == null ||
								_PM_S_VM.Name == string.Empty ||
								c.Name.Trim().ToLower().Contains(_PM_S_VM.Name.Trim().ToLower())).ToList();
		}
	}
}
