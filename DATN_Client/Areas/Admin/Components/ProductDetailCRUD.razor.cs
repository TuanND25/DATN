using DATN_Client.Areas.Admin.Controllers;
using DATN_Client.Areas.Customer.Component;
using DATN_Shared.ViewModel;
using DocumentFormat.OpenXml.Drawing.Charts;
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
		private List<Color_VM> _lstColor_Loc = new();
		private List<Size_VM> _lstSize_Loc = new();
		private List<Categories_VM> _lstCate_Loc = new();
		private List<Categories_VM> _lstCate_Add = new();
		private List<Image_Join_ProductItem> _lstImg_PI = new();
		private List<Image_VM> _lstImg_them = new();
		private List<Image_VM> _lstImg = new();
		private List<Image_VM> _lstImg_xoa = new();
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
		private bool isModalOpenUpdatePI = false;
		private bool isModalOpenXoaPI = false;
		private IBrowserFile _file { get; set; }
		private string _chonMau { get; set; } = string.Empty;
		private ProductItem_Show_VM _pri_s_vm_Update = new();
		private ProductItem_Show_VM _pri_s_vm_Loc = new();

		protected override async Task OnInitializedAsync()
		{
			if (Login.Roleuser != "Admin")
			{
				_navi.NavigateTo("https://localhost:7075/Admin", true);
				return;
			}
			_lst_pri = await _httpClient.GetFromJsonAsync<List<ProductItem_Show_VM>>($"https://localhost:7141/api/productitem/get_all_productitem_byProduct/{ProductController._productID}");
			_nameProduct = (await _httpClient.GetFromJsonAsync<Products_VM>($"https://localhost:7141/api/product/get_product_byid/{ProductController._productID}")).Name;
			_lstCate = (await _httpClient.GetFromJsonAsync<List<Categories_VM>>("https://localhost:7141/api/Categories")).Where(c => c.Status == 1).ToList();
			_lstCate = _lstCate.OrderBy(c => c.Name).ToList();
			_lstColor = (await _httpClient.GetFromJsonAsync<List<Color_VM>>("https://localhost:7141/api/Color/get_color")).Where(c => c.Status == 1).ToList();
			_lstColor = _lstColor.OrderBy(c => c.Name).ToList();
			_lstSize = (await _httpClient.GetFromJsonAsync<List<Size_VM>>("https://localhost:7141/api/Size/get_size")).Where(c => c.Status == 1).ToList();
			_lstSize = _lstSize.OrderBy(c => _lstSizeSample.IndexOf(c.Name)).ToList();
			foreach (var x in _lst_pri)
			{
				if (!_lstCate_Loc.Any(c => c.Id == x.CategoryID))
				{
					_lstCate_Loc.Add(new Categories_VM()
					{
						Id = x.CategoryID,
						Name = x.CategoryName,
					});
				}
				if (!_lstColor_Loc.Any(c => c.Id == x.ColorId))
				{
					_lstColor_Loc.Add(new Color_VM()
					{
						Id = Guid.Parse(x.ColorId.ToString()),
						Name = x.ColorName,
					});
				}
				if (!_lstSize_Loc.Any(c => c.Id == x.SizeId))
				{
					_lstSize_Loc.Add(new Size_VM()
					{
						Id = Guid.Parse(x.SizeId.ToString()),
						Name = x.SizeName,
					});
				}
			}
			var uri = new Uri(_navi.Uri);
			var queryParams = System.Web.HttpUtility.ParseQueryString(uri.Query);
			_pri_s_vm_Loc.CategoryName = queryParams["category"];
			_pri_s_vm_Loc.ColorName = queryParams["color"];
			_pri_s_vm_Loc.SizeName = queryParams["size"];
			await LocHangLoat();
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
				case "UpdatePI":
					isModalOpenUpdatePI = isOpen;
					break;
				case "XoaPI":
					isModalOpenXoaPI = isOpen;
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
			if (Login.Roleuser != "Admin")
			{
				_navi.NavigateTo("https://localhost:7075/Admin", true);
				return;
			}
			_navi.NavigateTo("/product-manager", true);
		}

		private async Task AddMauListTam()
		{
			if (Login.Roleuser != "Admin")
			{
				_navi.NavigateTo("https://localhost:7075/Admin", true);
				return;
			}
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
			if (Login.Roleuser != "Admin")
			{
				_navi.NavigateTo("https://localhost:7075/Admin", true);
				return;
			}
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
			if (Login.Roleuser != "Admin")
			{
				_navi.NavigateTo("https://localhost:7075/Admin", true);
				return;
			}
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
			if (Login.Roleuser != "Admin")
			{
				_navi.NavigateTo("https://localhost:7075/Admin", true);
				return;
			}
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
			if (Login.Roleuser != "Admin")
			{
				_navi.NavigateTo("https://localhost:7075/Admin", true);
				return;
			}
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
			if (Login.Roleuser != "Admin")
			{
				_navi.NavigateTo("https://localhost:7075/Admin", true);
				return;
			}
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
			if (Login.Roleuser != "Admin")
			{
				_navi.NavigateTo("https://localhost:7075/Admin", true);
				return;
			}
			_idColorChon = Guid.Parse("00000000-0000-0000-0000-000000000000");
			OpenPopup("AddColor_Tam");
		}

		private void Mo_AddCate_Tam()
		{
			if (Login.Roleuser != "Admin")
			{
				_navi.NavigateTo("https://localhost:7075/Admin", true);
				return;
			}
			_idCateChon = Guid.Parse("00000000-0000-0000-0000-000000000000");
			OpenPopup("AddCate_Tam");
		}

		private void Mo_AddSize_Tam()
		{
			if (Login.Roleuser != "Admin")
			{
				_navi.NavigateTo("https://localhost:7075/Admin", true);
				return;
			}
			_idSizeChon = Guid.Parse("00000000-0000-0000-0000-000000000000");
			OpenPopup("AddSize_Tam");
		}

		private async Task MoAddCate()
		{
			if (Login.Roleuser != "Admin")
			{
				_navi.NavigateTo("https://localhost:7075/Admin", true);
				return;
			}
			_Cate_VM.Name = string.Empty;
			OpenPopup("AddCate");
		}

		private async Task MoAddColor()
		{
			if (Login.Roleuser != "Admin")
			{
				_navi.NavigateTo("https://localhost:7075/Admin", true);
				return;
			}
			_C_VM.Name = string.Empty;
			OpenPopup("AddColor");
		}

		private async Task MoAddSize()
		{
			if (Login.Roleuser != "Admin")
			{
				_navi.NavigateTo("https://localhost:7075/Admin", true);
				return;
			}
			_S_VM.Name = string.Empty;
			OpenPopup("AddSize");
		}

		private async Task Mo_AddImage()
		{
			if (Login.Roleuser != "Admin")
			{
				_navi.NavigateTo("https://localhost:7075/Admin", true);
				return;
			}
			await ChonMau(_chonMau);
			_lstImg_them.Clear();
			_lstImg_xoa.Clear();
			_lstColor_string = _lst_pri.Select(c => c.ColorName).Distinct().OrderBy(c => c).ToList();
			OpenPopup("AddImage");
		}

		private async Task MoUpdatePI(ProductItem_Show_VM pi)
		{
			if (Login.Roleuser != "Admin")
			{
				_navi.NavigateTo("https://localhost:7075/Admin", true);
				return;
			}
			_pri_s_vm_Update.Id = pi.Id;
			_pri_s_vm_Update.ProductId = pi.ProductId;
			_pri_s_vm_Update.Name = pi.Name;
			_pri_s_vm_Update.ColorId = pi.ColorId;
			_pri_s_vm_Update.SizeId = pi.SizeId;
			_pri_s_vm_Update.CategoryID = pi.CategoryID;
			_pri_s_vm_Update.AvaiableQuantity = pi.AvaiableQuantity;
			_pri_s_vm_Update.PriceAfterReduction = pi.PriceAfterReduction;
			_pri_s_vm_Update.CostPrice = pi.CostPrice;
			_pri_s_vm_Update.Status = pi.Status;
			OpenPopup("UpdatePI");
		}

		private async Task MoDungHDPI(ProductItem_Show_VM pi)
		{
			if (Login.Roleuser != "Admin")
			{
				_navi.NavigateTo("https://localhost:7075/Admin", true);
				return;
			}
			_pri_s_vm_Update.Id = pi.Id;
			_pri_s_vm_Update.ProductId = pi.ProductId;
			_pri_s_vm_Update.ColorId = pi.ColorId;
			_pri_s_vm_Update.SizeId = pi.SizeId;
			_pri_s_vm_Update.CategoryID = pi.CategoryID;
			_pri_s_vm_Update.AvaiableQuantity = pi.AvaiableQuantity;
			_pri_s_vm_Update.PriceAfterReduction = pi.PriceAfterReduction;
			_pri_s_vm_Update.CostPrice = pi.CostPrice;
			_pri_s_vm_Update.Status = 0;
			OpenPopup("XoaPI");
		}

		private async Task Add_C()
		{
			if (Login.Roleuser != "Admin")
			{
				_navi.NavigateTo("https://localhost:7075/Admin", true);
				return;
			}
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
			if (Login.Roleuser != "Admin")
			{
				_navi.NavigateTo("https://localhost:7075/Admin", true);
				return;
			}
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
			if (Login.Roleuser != "Admin")
			{
				_navi.NavigateTo("https://localhost:7075/Admin", true);
				return;
			}
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
			if (Login.Roleuser != "Admin")
			{
				_navi.NavigateTo("https://localhost:7075/Admin", true);
				return;
			}
			bool checkAdd = true;
			//	public Guid Id { get; set; }
			//public Guid ProductId { get; set; }
			//public Guid? ColorId { get; set; }
			//public Guid? SizeId { get; set; }
			//public Guid CategoryId { get; set; }
			//public int? AvaiableQuantity { get; set; }
			//public int? PriceAfterReduction { get; set; }
			//public int? CostPrice { get; set; }
			//public int Status { get; set; } = 0;
			foreach (var x in _lst_pri_Add)
			{
				if (_lst_pri.Where(a => a.ColorId == x.ColorId && a.SizeId == x.SizeId && a.CategoryID == x.CategoryID).Count() > 0 || x.AvaiableQuantity <= 0 || x.CostPrice <= 0)
				{
					_toastService.ShowError("Tạo mới không thành công. Vui lòng thỏa mãn các điều kiện cần thiết!");
					return;
				}
			}
			foreach (var a in _lst_pri_Add)
			{
				ProductItem_VM pri_vm = new ProductItem_VM()
				{
					Id = Guid.NewGuid(),
					ProductId = a.ProductId,
					ColorId = a.ColorId,
					SizeId = a.SizeId,
					CategoryId = a.CategoryID,
					AvaiableQuantity = a.AvaiableQuantity,
					CostPrice = a.CostPrice,
					PriceAfterReduction = a.CostPrice,
					Status = a.Status
				};
				var addPI = await _httpClient.PostAsJsonAsync("https://localhost:7141/api/productitem/add_productitem", pri_vm);
				if (addPI.StatusCode != System.Net.HttpStatusCode.OK) checkAdd = false;
				var lstImg = await _httpClient.GetFromJsonAsync<List<Image_Join_ProductItem>>($"https://localhost:7141/api/Image/GetAllImage_PrductItem_ByProductId/{ProductController._productID}");
				var lstImgMoiTam = lstImg.Where(c => c.ColorId == pri_vm.ColorId).ToList();
				List<Image_Join_ProductItem> lstImgMoi = new();
				foreach (var b in lstImgMoiTam)
				{
					if (!lstImgMoi.Any(c => c.PathImage == b.PathImage))
					{
						lstImgMoi.Add(b);
					}
				}
				foreach (var b in lstImgMoi)
				{
					Image_VM img = new()
					{
						Id = Guid.NewGuid(),
						Name = "",
						STT = await _httpClient.GetFromJsonAsync<int>("https://localhost:7141/api/Image/GetSttMax"),
						PathImage = b.PathImage,
						ProductItemId = pri_vm.Id,
						Status = 1
					};
					var addImg = await _httpClient.PostAsJsonAsync("https://localhost:7141/api/Image/Post-Image", img);
					if (addImg.StatusCode != System.Net.HttpStatusCode.OK) checkAdd = false;
				}
			}
			if (checkAdd == true)
			{
				_lstCate_Add.Clear();
				_lstSize_Add.Clear();
				_lstColor_Add.Clear();
				_lst_pri_Add.Clear();
				_lst_pri = await _httpClient.GetFromJsonAsync<List<ProductItem_Show_VM>>($"https://localhost:7141/api/productitem/get_all_productitem_byProduct/{ProductController._productID}");
				await JsRuntime.InvokeVoidAsync("OnScrollEvent");
				_toastService.ShowSuccess("Thêm thành công");
			}
			if (checkAdd == false)
			{
				_toastService.ShowError("Thêm thất bại");
			}
		}

		private void DeletePITam(ProductItem_Show_VM pi)
		{
			if (Login.Roleuser != "Admin")
			{
				_navi.NavigateTo("https://localhost:7075/Admin", true);
				return;
			}
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
			if (Login.Roleuser != "Admin")
			{
				_navi.NavigateTo("https://localhost:7075/Admin", true);
				return;
			}
			if (string.IsNullOrEmpty(mau)) return;
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
			if (Login.Roleuser != "Admin")
			{
				_navi.NavigateTo("https://localhost:7075/Admin", true);
				return;
			}
			var lstImg = await _httpClient.GetFromJsonAsync<List<Image_Join_ProductItem>>("https://localhost:7141/api/Image/GetAllImage_PrductItem");
			Image_VM imgTam = new Image_VM();
			_file = e.File;
			if (_file != null)
			{
				if (_lstImg.Any(c => c.PathImage == _file.Name))
				{
					_toastService.ShowError("Ảnh đã tồn tại");
					return;
				}
				// Trỏ tới thư mục wwwroot để lát nữa thực hiện việc copy sang
				var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", _file.Name);
				using (var stream = new FileStream(path, FileMode.Create))
				{
					// Thực hiện copy ảnh vừa chọn sang thư mục mới (wwwroot)
					try
					{
						await _file.OpenReadStream(5120 * 1024).CopyToAsync(stream);
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
				//_idImg_Tam = imgTam.Id;
				if (_lstImg.Count == 0) imgTam.STT = 1;
				else imgTam.STT = _lstImg.Max(c => c.STT) + 1;
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
			if (Login.Roleuser != "Admin")
			{
				_navi.NavigateTo("https://localhost:7075/Admin", true);
				return;
			}
			_lstImg_xoa.Add(img);
			_lstImg.Remove(img);
			_lstImg_them.Remove(img);
		}

		private async Task XacNhanAddImg()
		{
			if (Login.Roleuser != "Admin")
			{
				_navi.NavigateTo("https://localhost:7075/Admin", true);
				return;
			}
			bool checkAddDelete = true;
			var lstImg = await _httpClient.GetFromJsonAsync<List<Image_VM>>("https://localhost:7141/api/Image");
			List<Image_VM> lstTam1 = new();
			List<Image_VM> lstXoa = new();
			foreach (var a in _lstPri_ChonMau)
			{
				foreach (var b in lstImg)
				{
					if (a.Id == b.ProductItemId)
					{
						lstTam1.Add(b);
					}
				}
			}
			foreach (var a in lstTam1)
			{
				foreach (var b in _lstImg_xoa)
				{
					if (a.PathImage == b.PathImage)
					{
						var delete = await _httpClient.DeleteAsync($"https://localhost:7141/api/Image/Delete-Image/{a.Id}");
						if (delete.StatusCode != System.Net.HttpStatusCode.OK) checkAddDelete = false;
					}
				}
			}
			foreach (var a in _lstPri_ChonMau)
			{
				foreach (var c in _lstImg_them)
				{
					Image_VM img = new()
					{
						Id = Guid.NewGuid(),
						Name = "",
						STT = await _httpClient.GetFromJsonAsync<int>("https://localhost:7141/api/Image/GetSttMax"),
						PathImage = c.PathImage,
						ProductItemId = a.Id,
						Status = 1
					};
					var add = await _httpClient.PostAsJsonAsync("https://localhost:7141/api/Image/Post-Image", img);
					if (add.StatusCode != System.Net.HttpStatusCode.OK) checkAddDelete = false;
				}
			}
			if (checkAddDelete == true)
			{
				_lstImg_them.Clear();
				_lstImg_xoa.Clear();
				_toastService.ShowSuccess("Thay đổi ảnh minh họa thành công");
				ClosePopup("AddImage");
			}
			else _toastService.ShowError("Thao tác thất bại");
		}

		private async Task XacNhanUpdate()
		{
			if (Login.Roleuser != "Admin")
			{
				_navi.NavigateTo("https://localhost:7075/Admin", true);
				return;
			}
			bool checkStatusCode = true;
			var checkPromotion = await _httpClient.GetFromJsonAsync<ProductItem_VM>($"https://localhost:7141/api/productitem/get_all_productitem_byID/{_pri_s_vm_Update.Id}");
			var promotionItem = await _httpClient.GetFromJsonAsync<PromotionItem_VM>($"https://localhost:7141/api/PromotionItem/getPromotionItem_Percent_by_productItemID/{_pri_s_vm_Update.Id}");
			ProductItem_VM priUpdate = new()
			{
				Id = _pri_s_vm_Update.Id,
				ProductId = _pri_s_vm_Update.ProductId,
				ColorId = _pri_s_vm_Update.ColorId,
				SizeId = _pri_s_vm_Update.SizeId,
				CategoryId = _pri_s_vm_Update.CategoryID,
				AvaiableQuantity = _pri_s_vm_Update.AvaiableQuantity,
				CostPrice = _pri_s_vm_Update.CostPrice,
				PriceAfterReduction = checkPromotion.CostPrice == checkPromotion.PriceAfterReduction
									? _pri_s_vm_Update.CostPrice
									: (_pri_s_vm_Update.CostPrice - _pri_s_vm_Update.CostPrice / promotionItem.Percent),
				Status = _pri_s_vm_Update.Status,
			};
			if (_lst_pri.Where(c => (c.ColorId == priUpdate.ColorId && c.SizeId == priUpdate.SizeId && c.CategoryID == priUpdate.CategoryId) && c.Id != priUpdate.Id).Count() > 0)
			{
				_toastService.ShowError("Cập nhật thất bại do biến thể đã tồn tại");
				return;
			}
			var checkIdColor = await _httpClient.GetFromJsonAsync<ProductItem_VM>($"https://localhost:7141/api/productitem/get_all_productitem_byID/{_pri_s_vm_Update.Id}");
			if (_pri_s_vm_Update.ColorId != checkIdColor.ColorId)
			{

				var lstImg = await _httpClient.GetFromJsonAsync<List<Image_Join_ProductItem>>($"https://localhost:7141/api/Image/GetAllImage_PrductItem_ByProductId/{ProductController._productID}");
				var lstImgCu = lstImg.Where(c => c.ProductItemId == checkIdColor.Id).ToList();
				var lstImgMoiTam = lstImg.Where(c => c.ColorId == _pri_s_vm_Update.ColorId).ToList();
				List<Image_Join_ProductItem> lstImgMoi = new();
				foreach (var item in lstImgMoiTam)
				{
					if (!lstImgMoi.Any(c => c.PathImage == item.PathImage))
					{
						lstImgMoi.Add(item);
					}
				}
				foreach (var a in lstImgCu)
				{
					var delete = await _httpClient.DeleteAsync($"https://localhost:7141/api/Image/Delete-Image/{a.Id}");
					if (delete.StatusCode != System.Net.HttpStatusCode.OK) checkStatusCode = false;
				}
				foreach (var b in lstImgMoi)
				{
					Image_VM img = new()
					{
						Id = Guid.NewGuid(),
						Name = "",
						STT = await _httpClient.GetFromJsonAsync<int>("https://localhost:7141/api/Image/GetSttMax"),
						PathImage = b.PathImage,
						ProductItemId = _pri_s_vm_Update.Id,
						Status = 1
					};
					var add = await _httpClient.PostAsJsonAsync("https://localhost:7141/api/Image/Post-Image", img);
					if (add.StatusCode != System.Net.HttpStatusCode.OK) checkStatusCode = false;
				}
			}
			var update = await _httpClient.PutAsJsonAsync("https://localhost:7141/api/productitem/update_productitem", priUpdate);
			if (update.StatusCode == System.Net.HttpStatusCode.OK && checkStatusCode == true)
			{
				ClosePopup("UpdatePI");
				_lst_pri = await _httpClient.GetFromJsonAsync<List<ProductItem_Show_VM>>($"https://localhost:7141/api/productitem/get_all_productitem_byProduct/{ProductController._productID}");
				await JsRuntime.InvokeVoidAsync("OnScrollEvent");
				_toastService.ShowSuccess("Cập nhật thành công");
			}
			else
			{
				_toastService.ShowError("Cập nhật thất bại");
			}
		}

		private async Task XacNhanXoaPI()
		{
			if (Login.Roleuser != "Admin")
			{
				_navi.NavigateTo("https://localhost:7075/Admin", true);
				return;
			}
			ProductItem_VM priUpdate = new()
			{
				Id = _pri_s_vm_Update.Id,
				ProductId = _pri_s_vm_Update.ProductId,
				ColorId = _pri_s_vm_Update.ColorId,
				SizeId = _pri_s_vm_Update.SizeId,
				CategoryId = _pri_s_vm_Update.CategoryID,
				AvaiableQuantity = _pri_s_vm_Update.AvaiableQuantity,
				CostPrice = _pri_s_vm_Update.CostPrice,
				PriceAfterReduction = _pri_s_vm_Update.CostPrice,
				Status = _pri_s_vm_Update.Status,
			};
			var update = await _httpClient.PutAsJsonAsync("https://localhost:7141/api/productitem/update_productitem", priUpdate);
			if (update.StatusCode == System.Net.HttpStatusCode.OK)
			{
				ClosePopup("XoaPI");
				_lst_pri = await _httpClient.GetFromJsonAsync<List<ProductItem_Show_VM>>($"https://localhost:7141/api/productitem/get_all_productitem_byProduct/{ProductController._productID}");
				await JsRuntime.InvokeVoidAsync("OnScrollEvent");
				_toastService.ShowSuccess("Thao tác thành công");
			}
			else _toastService.ShowError("Thao tác thất bại");
		}
		public async Task ChonLoc()
		{
			var currentUrl = _navi.ToAbsoluteUri(_navi.Uri);

			// Thêm thông tin vào URL parameters
			var uriBuilder = new UriBuilder(currentUrl);
			var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);
			if (!string.IsNullOrEmpty(_pri_s_vm_Loc.CategoryName))
				query["category"] = _pri_s_vm_Loc.CategoryName.ToString();
			else query["category"] = null;
			if (!string.IsNullOrEmpty(_pri_s_vm_Loc.ColorName))
				query["color"] = _pri_s_vm_Loc.ColorName.ToString();
			else query["color"] = null;
			if (!string.IsNullOrEmpty(_pri_s_vm_Loc.SizeName))
				query["size"] = _pri_s_vm_Loc.SizeName.ToString();
			else query["size"] = null;
			uriBuilder.Query = query.ToString();
			// Chuyển đến URL mới
			_navi.NavigateTo(uriBuilder.ToString());
			await LocHangLoat();
		}
		public async Task LocHangLoat()
		{
			if (Login.Roleuser != "Admin")
			{
				_navi.NavigateTo("https://localhost:7075/Admin", true);
				return;
			}
			_lst_pri = await _httpClient.GetFromJsonAsync<List<ProductItem_Show_VM>>($"https://localhost:7141/api/productitem/get_all_productitem_byProduct/{ProductController._productID}");

			_lst_pri = _lst_pri.Where(c =>
						(string.IsNullOrEmpty(_pri_s_vm_Loc.CategoryName) ||
						c.CategoryName == _pri_s_vm_Loc.CategoryName) &&
						(string.IsNullOrEmpty(_pri_s_vm_Loc.SizeName) ||
						c.SizeName == _pri_s_vm_Loc.SizeName) &&
						(string.IsNullOrEmpty(_pri_s_vm_Loc.ColorName) ||
						c.ColorName == _pri_s_vm_Loc.ColorName)).OrderBy(c => c.Name).ToList();
		}
	}
}