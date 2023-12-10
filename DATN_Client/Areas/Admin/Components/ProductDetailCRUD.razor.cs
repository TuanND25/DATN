using DATN_Client.Areas.Admin.Controllers;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace DATN_Client.Areas.Admin.Components
{
	public partial class ProductDetailCRUD
	{
		private HttpClient _httpClient = new();
		[Inject] IJSRuntime JsRuntime { get; set; }
		[Inject] private NavigationManager _navi { get; set; }
		[Inject] private Blazored.Toast.Services.IToastService _toastService { get; set; } // Khai báo khi cần gọi ở code-behind
		private List<ProductItem_Show_VM>? _lst_pri = new();
		private List<ProductItem_Show_VM>? _lst_pri_Add = new();
		private List<ProductItem_Show_VM>? _lstPri_ChonMau = new();
		private string _nameProduct { get; set; }
		private List<Color_VM> _lstColor = new();
		private List<Size_VM> _lstSize = new();
		private List<Categories_VM> _lstCate = new();
		private List<Color_VM> _lstColor_Add = new();
		private List<Size_VM> _lstSize_Add = new();
		private List<Categories_VM> _lstCate_Add = new();
		private List<Image_Join_ProductItem> _lstImg_PI = new();
		private List<Image_VM> _lstImg_them = new();
		private List<Image_VM> _lstImg = new();
		private List<string> _lstSizeSample = new List<string> { "XS", "S", "M", "L", "XL", "2XL", "3XL", "4XL", "5XL" };
		private List<string> _lstColor_string = new();
		private Categories_VM _Cate_VM = new();
		private Color_VM _C_VM = new();
		private Size_VM _S_VM = new();
		private Guid _idColorChon { get; set; }
		private Guid _idCateChon { get; set; }
		private Guid _idSizeChon { get; set; }
		private bool isModalOpenAddCate_Tam = false;
		private bool isModalOpenAddColor_Tam = false;
		private bool isModalOpenAddSize_Tam = false;
		private bool isModalOpenAddCate = false;
		private bool isModalOpenAddColor = false;
		private bool isModalOpenAddSize = false;
		private bool isModalOpenAddImage = false;
		private IBrowserFile _file { get; set; }
		private string _chonMau { get; set; } = string.Empty;

		protected override async Task OnInitializedAsync()
		{
			_lst_pri = await _httpClient.GetFromJsonAsync<List<ProductItem_Show_VM>>($"https://localhost:7141/api/productitem/get_all_productitem_byProduct/{ProductController._productID}");
			_nameProduct = _lst_pri.FirstOrDefault().Name;
			_lstCate = await _httpClient.GetFromJsonAsync<List<Categories_VM>>("https://localhost:7141/api/Categories");
			_lstCate = _lstCate.OrderBy(c => c.Name).ToList();
			_lstColor = await _httpClient.GetFromJsonAsync<List<Color_VM>>("https://localhost:7141/api/Color/get_color");
			_lstColor = _lstColor.OrderBy(c => c.Name).ToList();
			_lstSize = await _httpClient.GetFromJsonAsync<List<Size_VM>>("https://localhost:7141/api/Size/get_size");
			_lstSize = _lstSize.OrderBy(c => _lstSizeSample.IndexOf(c.Name)).ToList();
		}

		private void SetModalState(bool isOpen, string modalType)
		{
			switch (modalType)
			{
				case "AddCate_Tam":
					isModalOpenAddCate_Tam = isOpen;
					break;
				case "AddColor_Tam":
					isModalOpenAddColor_Tam = isOpen;
					break;
				case "AddSize_Tam":
					isModalOpenAddSize_Tam = isOpen;
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
				case "AddImage":
					isModalOpenAddImage = isOpen;
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

		private void BackToManager()
		{
			_navi.NavigateTo("/product-manager", true);
		}

		private async Task AddMauListTam()
		{
			if (_idColorChon == Guid.Parse("00000000-0000-0000-0000-000000000000")) return;
			var color = await _httpClient.GetFromJsonAsync<Color_VM>($"https://localhost:7141/api/Color/ById?Id={_idColorChon}");
			if (!_lstColor_Add.Any(c => c.Id == color.Id))
			{
				_lstColor_Add.Add(color);
			}
			//_lst_pri_Add.Clear();
			if (_lstCate_Add.Count > 0 && _lstSize_Add.Count > 0 && _lstColor_Add.Count > 0)
			{
				foreach (var a in _lstCate_Add)
				{
					foreach (var b in _lstSize_Add)
					{
						foreach (var c in _lstColor_Add)
						{
							if (!_lst_pri_Add.Any(x => x.ColorId == c.Id && x.SizeId == b.Id && x.CategoryID == a.Id))
							{
								_lst_pri_Add.Add(new ProductItem_Show_VM()
								{
									Id = Guid.NewGuid(),
									Name = _nameProduct,
									ProductId = ProductController._productID,
									ColorId = c.Id,
									ColorName = c.Name,
									SizeId = b.Id,
									SizeName = b.Name,
									CategoryID = a.Id,
									CategoryName = a.Name,
									Status = 1,
									AvaiableQuantity = 0,
									CostPrice = 0,
									PriceAfterReduction = 0
								});
							}
						}
					}
				}
			}
			ClosePopup("AddColor_Tam");
		}

		private async Task AddCateListTam()
		{
			if (_idCateChon == Guid.Parse("00000000-0000-0000-0000-000000000000")) return;
			var cate = await _httpClient.GetFromJsonAsync<Categories_VM>($"https://localhost:7141/api/Categories/ById?Id={_idCateChon}");
			if (!_lstCate_Add.Any(c => c.Id == cate.Id))
			{
				_lstCate_Add.Add(cate);
			}
			//_lst_pri_Add.Clear();
			if (_lstCate_Add.Count > 0 && _lstSize_Add.Count > 0 && _lstColor_Add.Count > 0)
			{
				foreach (var a in _lstCate_Add)
				{
					foreach (var b in _lstSize_Add)
					{
						foreach (var c in _lstColor_Add)
						{
							if (!_lst_pri_Add.Any(x => x.ColorId == c.Id && x.SizeId == b.Id && x.CategoryID == a.Id))
							{
								_lst_pri_Add.Add(new ProductItem_Show_VM()
								{
									Id = Guid.NewGuid(),
									Name = _nameProduct,
									ProductId = ProductController._productID,
									ColorId = c.Id,
									ColorName = c.Name,
									SizeId = b.Id,
									SizeName = b.Name,
									CategoryID = a.Id,
									CategoryName = a.Name,
									Status = 1,
									AvaiableQuantity = 0,
									CostPrice = 0,
									PriceAfterReduction = 0
								});
							}
						}
					}
				}
			}
			ClosePopup("AddCate_Tam");
		}

		private async Task AddSizeListTam()
		{
			if (_idSizeChon == Guid.Parse("00000000-0000-0000-0000-000000000000")) return;
			var size = await _httpClient.GetFromJsonAsync<Size_VM>($"https://localhost:7141/api/Size/Id?Id={_idSizeChon}");
			if (!_lstSize_Add.Any(c => c.Id == size.Id))
			{
				_lstSize_Add.Add(size);
			}
			//_lst_pri_Add.Clear();
			if (_lstCate_Add.Count > 0 && _lstSize_Add.Count > 0 && _lstColor_Add.Count > 0)
			{
				foreach (var a in _lstCate_Add)
				{
					foreach (var b in _lstSize_Add)
					{
						foreach (var c in _lstColor_Add)
						{
							if (!_lst_pri_Add.Any(x => x.ColorId == c.Id && x.SizeId == b.Id && x.CategoryID == a.Id))
							{
								_lst_pri_Add.Add(new ProductItem_Show_VM()
								{
									Id = Guid.NewGuid(),
									Name = _nameProduct,
									ProductId = ProductController._productID,
									ColorId = c.Id,
									ColorName = c.Name,
									SizeId = b.Id,
									SizeName = b.Name,
									CategoryID = a.Id,
									CategoryName = a.Name,
									Status = 1,
									AvaiableQuantity = 0,
									CostPrice = 0,
									PriceAfterReduction = 0
								});
							}
						}
					}
				}
			}
			ClosePopup("AddSize_Tam");
		}

		private void RemoveMauListTam(Color_VM cl)
		{
			List<ProductItem_Show_VM> lst = new();
			foreach (var x in _lst_pri_Add)
			{
				if (x.ColorId != cl.Id)
				{
					lst.Add(x);
				}
			}
			_lst_pri_Add = lst;
			_lstColor_Add.Remove(cl);
		}

		private void RemoveSizeListTam(Size_VM sz)
		{
			List<ProductItem_Show_VM> lst = new();
			foreach (var x in _lst_pri_Add)
			{
				if (x.SizeId != sz.Id)
				{
					lst.Add(x);
				}
			}
			_lst_pri_Add = lst;
			_lstSize_Add.Remove(sz);
		}

		private void RemoveCateListTam(Categories_VM cate)
		{
			List<ProductItem_Show_VM> lst = new();
			foreach (var x in _lst_pri_Add)
			{
				if (x.CategoryID != cate.Id)
				{
					lst.Add(x);
				}
			}
			_lst_pri_Add = lst;
			_lstCate_Add.Remove(cate);
		}

		private void Mo_AddColor_Tam()
		{
			_idColorChon = Guid.Parse("00000000-0000-0000-0000-000000000000");
			OpenPopup("AddColor_Tam");
		}

		private void Mo_AddCate_Tam()
		{
			_idCateChon = Guid.Parse("00000000-0000-0000-0000-000000000000");
			OpenPopup("AddCate_Tam");
		}

		private void Mo_AddSize_Tam()
		{
			_idSizeChon = Guid.Parse("00000000-0000-0000-0000-000000000000");
			OpenPopup("AddSize_Tam");
		}

		private async Task MoAddCate()
		{
			_Cate_VM.Name = string.Empty;
			OpenPopup("AddCate");
		}

		private async Task MoAddColor()
		{
			_C_VM.Name = string.Empty;
			OpenPopup("AddColor");
		}

		private async Task MoAddSize()
		{
			await ChonMau(_chonMau);
			_S_VM.Name = string.Empty;
			OpenPopup("AddSize");
		}

		private void Mo_AddImage()
		{			
			_lstColor_string = _lst_pri.Select(c => c.ColorName).Distinct().OrderBy(c => c).ToList();
			OpenPopup("AddImage");
		}

		private async Task Add_C()
		{
			if (string.IsNullOrEmpty(_C_VM.Name))
			{
				_toastService.ShowError("Không được để trống");
				return;
			}
			if (_lstColor.Any(c => c.Name.ToLower() == _C_VM.Name.ToLower()))
			{
				_toastService.ShowError("Màu sắc đã tồn tại");
				return;
			}
			_C_VM.Status = 1;
			var x = await _httpClient.PostAsJsonAsync("https://localhost:7141/api/Color/PostColor", _C_VM);
			_lstColor = await _httpClient.GetFromJsonAsync<List<Color_VM>>("https://localhost:7141/api/Color/get_color");
			if (x.IsSuccessStatusCode)
			{
				_toastService.ShowSuccess("Thêm thành công");
				ClosePopup("AddColor");
			}
			else _toastService.ShowError("Thêm không thành công");
		}

		private async Task Add_Cate()
		{
			if (string.IsNullOrEmpty(_Cate_VM.Name))
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
			_Cate_VM.TenKhongDau = "";
			var x = await _httpClient.PostAsJsonAsync("https://localhost:7141/api/Categories/PostCategory", _Cate_VM);
			_lstCate = await _httpClient.GetFromJsonAsync<List<Categories_VM>>("https://localhost:7141/api/Categories");
			if (x.IsSuccessStatusCode)
			{
				_toastService.ShowSuccess("Thêm thành công");
				ClosePopup("AddCate");
			}
			else _toastService.ShowError("Thêm không thành công");
		}

		private async Task Add_S()
		{
			if (string.IsNullOrEmpty(_S_VM.Name))
			{
				_toastService.ShowError("Không được để trống");
				return;
			}
			if (_lstSize.Any(c => c.Name.ToLower() == _S_VM.Name.ToLower()))
			{
				_toastService.ShowError("Kích thước đã tồn tại");
				return;
			}
			_S_VM.Status = 1;
			_S_VM.Name = _S_VM.Name.ToUpper();
			var x = await _httpClient.PostAsJsonAsync("https://localhost:7141/api/Size/PostSize", _S_VM);
			_lstSize = await _httpClient.GetFromJsonAsync<List<Size_VM>>("https://localhost:7141/api/Size/get_size");
			_lstSize = _lstSize.OrderBy(c => _lstSizeSample.IndexOf(c.Name)).ToList();
			if (x.IsSuccessStatusCode)
			{
				_toastService.ShowSuccess("Thêm thành công");
				ClosePopup("AddSize");
			}
			else _toastService.ShowError("Thêm không thành công");
		}

		private async Task AddPI()
		{

			//	public Guid Id { get; set; }
			//public Guid ProductId { get; set; }
			//public Guid? ColorId { get; set; }
			//public Guid? SizeId { get; set; }
			//public Guid CategoryId { get; set; }
			//public int? AvaiableQuantity { get; set; }
			//public int? PriceAfterReduction { get; set; }
			//public int? CostPrice { get; set; }
			//public int Status { get; set; } = 0;

			bool checkAdd = true;
			foreach (var item in _lst_pri_Add)
			{
				ProductItem_VM pri_vm = new ProductItem_VM()
				{
					Id = Guid.NewGuid(),
					ProductId = item.ProductId,
					ColorId = item.ColorId,
					SizeId = item.SizeId,
					CategoryId = item.CategoryID,
					AvaiableQuantity = item.AvaiableQuantity,
					CostPrice = item.CostPrice,
					PriceAfterReduction = item.CostPrice,
					Status = 1
				};
				var a = await _httpClient.PostAsJsonAsync("https://localhost:7141/api/productitem/add_productitem", pri_vm);
				if (a.StatusCode != System.Net.HttpStatusCode.OK) checkAdd = false;
			}
			if (checkAdd = true)
			{
				_lstCate_Add.Clear();
				_lstSize_Add.Clear();
				_lstColor_Add.Clear();
				_lst_pri_Add.Clear();
				_lst_pri = await _httpClient.GetFromJsonAsync<List<ProductItem_Show_VM>>($"https://localhost:7141/api/productitem/get_all_productitem_byProduct/{ProductController._productID}");
				await JsRuntime.InvokeVoidAsync("OnScrollEvent");
				_toastService.ShowSuccess("Thêm thành công");
			}
			if (checkAdd = false)
			{
				_toastService.ShowSuccess("Thêm thất bại");
			}
		}

		private void DeletePITam(ProductItem_Show_VM pi)
		{
			List<Size_VM> lstSz = new();
			List<Color_VM> lstCl = new();
			List<Categories_VM> lstCate = new();
			_lst_pri_Add.Remove(pi);
			foreach (var a in _lstSize_Add)
			{
				if (_lst_pri_Add.Any(c => c.SizeId == a.Id))
				{
					lstSz.Add(a);
				}
			}
			_lstSize_Add = lstSz.Distinct().ToList();
			foreach (var a in _lstColor_Add)
			{
				if (_lst_pri_Add.Any(c => c.ColorId == a.Id))
				{
					lstCl.Add(a);
				}
			}
			_lstColor_Add = lstCl.Distinct().ToList();
			foreach (var a in _lstCate_Add)
			{
				if (_lst_pri_Add.Any(c => c.CategoryID == a.Id))
				{
					lstCate.Add(a);
				}
			}
			_lstCate_Add = lstCate.Distinct().ToList();
		}

		private async Task ChonMau(string mau)
		{
			_chonMau = mau;
			_lstPri_ChonMau.Clear();
			_lstPri_ChonMau = _lst_pri.Where(c => c.ColorName == mau).ToList();
			var lstImg = await _httpClient.GetFromJsonAsync<List<Image_VM>>("https://localhost:7141/api/Image");
			_lstImg.Clear();
			foreach (var a in _lstPri_ChonMau)
			{
				foreach (var b in lstImg)
				{
					if (a.Id == b.ProductItemId)
					{
						if (!_lstImg.Any(c => c.PathImage == b.PathImage))
						{
							_lstImg.Add(b);
						}
					}
				}
			}
		}

		public async Task ChangeEv(InputFileChangeEventArgs e)
		{
			var lstImg = await _httpClient.GetFromJsonAsync<List<Image_Join_ProductItem>>("https://localhost:7141/api/Image/GetAllImage_PrductItem");
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
				// Gán lại giá trị cho Description của đối tượng bằng tên file ảnh đã được sao chép
				imgTam.PathImage = _file.Name;
				//_pathImg = _file.Name;
				imgTam.Id = Guid.NewGuid();
				//_idImg_Tam = imgTam.Id;
				imgTam.Name = "";
				//if (lstImg.Count == 0) imgTam.STT = 1;
				//else imgTam.STT = _lstImg_Tam.Count == 0
				//			? _lstImg.Max(c => c.STT) + 1
				//			: (_lstImg.Max(c => c.STT) > _lstImg_Tam.Max(c => c.STT)
				//			? _lstImg.Max(c => c.STT) + 1
				//			: _lstImg_Tam.Max(c => c.STT) + 1);
				//if (_idPI.ToString() == "00000000-0000-0000-0000-000000000000") imgTam.ProductItemId = _idPI_Tam;
				//else imgTam.ProductItemId = _idPI;
				//imgTam.Status = 1;
				_lstImg_them.Add(imgTam);
				_lstImg.Add(imgTam);
				_toastService.ShowSuccess("Ảnh đã được tải lên thành công");
			}
		}

		private void XoaAnhTam(Image_VM img)
		{
			_lstImg.Remove(img);
		}

		private async Task XacNhanAddImg()
		{
			//public Guid Id { get; set; }
			//public Guid? ReviewId { get; set; }
			//public string Name { get; set; }
			//public int STT { get; set; }
			//public string PathImage { get; set; }
			//public Guid ProductItemId { get; set; }
			//public int Status { get; set; }
			bool checkAdd = true;
			foreach (var a in _lstPri_ChonMau)
			{
				foreach (var b in _lstImg_them)
				{
					Image_VM img = new()
					{
						Id = Guid.NewGuid(),
						Name = "",
						STT = await _httpClient.GetFromJsonAsync<int>("https://localhost:7141/api/Image/GetSttMax"),
						PathImage = b.PathImage,
						ProductItemId = a.Id,
						Status = 1
					};
					var add = await _httpClient.PostAsJsonAsync("https://localhost:7141/api/Image/Post-Image", img);
					if (add.StatusCode!=System.Net.HttpStatusCode.OK) checkAdd = false;
				}
			}
			if (checkAdd == true) _toastService.ShowSuccess("Thêm ảnh minh họa thành công");
			else _toastService.ShowError("Thao tác thất bại");
		}
	}
}