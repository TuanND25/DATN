using DATN_Client.Areas.Admin.Controllers;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;

namespace DATN_Client.Areas.Admin.Components
{
	public partial class ProductDetailCRUD
	{
		private HttpClient _httpClient = new();
		[Inject] private NavigationManager _navi { get; set; }
		[Inject] private Blazored.Toast.Services.IToastService _toastService { get; set; } // Khai báo khi cần gọi ở code-behind
		private List<ProductItem_Show_VM>? _lst_pri = new();
		private List<ProductItem_Show_VM>? _lst_pri_Add = new();
		private string _nameProduct { get; set; }
		private List<Color_VM> _lstColor = new();
		private List<Size_VM> _lstSize = new();
		private List<Categories_VM> _lstCate = new();
		private List<Color_VM> _lstColor_Add = new();
		private List<Size_VM> _lstSize_Add = new();
		private List<Categories_VM> _lstCate_Add = new();
		private List<string> _lstSizeSample = new List<string> { "XS", "S", "M", "L", "XL", "2XL", "3XL", "4XL", "5XL" };
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
									AvaiableQuantity = 10,
									CostPrice = 100000,
									PriceAfterReduction = 100000
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
									AvaiableQuantity = 10,
									CostPrice = 100000,
									PriceAfterReduction = 100000
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
									AvaiableQuantity = 10,
									CostPrice = 100000,
									PriceAfterReduction = 100000
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
			_S_VM.Name = string.Empty;
			OpenPopup("AddSize");
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

		private void Check12()
		{
			var a = _lst_pri_Add;
		}

		private void DeletePITam(ProductItem_Show_VM pi)
		{
			List<Size_VM> lstSz = new();
			List<Color_VM> lstCl = new();
			List<Categories_VM> lstCate = new();
			_lst_pri_Add.Remove(pi);
			foreach (var a in _lstSize_Add)
			{
				if (_lst_pri_Add.Any(c=>c.SizeId==a.Id))
				{
					lstSz.Add(a);
				}
			}
			_lstSize_Add=lstSz.Distinct().ToList();
			foreach (var a in _lstColor_Add)
			{
				if (_lst_pri_Add.Any(c => c.ColorId == a.Id))
				{
					lstCl.Add(a);
				}
			}
			_lstColor_Add=lstCl.Distinct().ToList();
			foreach (var a in _lstCate_Add)
			{
				if (_lst_pri_Add.Any(c => c.CategoryID == a.Id))
				{
					lstCate.Add(a);
				}
			}
			_lstCate_Add=lstCate.Distinct().ToList();
		}
	}
}