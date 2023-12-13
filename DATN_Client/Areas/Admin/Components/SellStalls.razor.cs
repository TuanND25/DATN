using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using DATN_Shared.ViewModel.DiaChi;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Components;
using Microsoft.Build.Evaluation;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using Twilio.Types;


namespace DATN_Client.Areas.Admin.Components
{
	public partial class SellStalls
	{
		HttpClient _client = new HttpClient();
		[Inject] NavigationManager _navigation { get; set; }
		[Inject] Blazored.Toast.Services.IToastService _toastService { get; set; }
		List<ProductItem_Show_VM> _lstPrI_show_VM = new List<ProductItem_Show_VM>();
		List<Image_Join_ProductItem> _lstImg_PI = new List<Image_Join_ProductItem>();
		List<Products_VM> _lstP = new List<Products_VM>();
		List<Products_VM> _lstP_1 = new List<Products_VM>();
		List<Bill_VM> _lstBill = new List<Bill_VM>();
		Bill_VM bill = new Bill_VM();
		List<Bill_VM> _lstBill_Vm = new List<Bill_VM>();
		List<Bill_VM> _lstBill_Vm_show = new List<Bill_VM>();
		List<User_VM> _lstUser = new List<User_VM>();
		List<User_VM> _lstUser_1 = new List<User_VM>();
		List<User_VM> _lstUser_2 = new List<User_VM>();
		User_VM getuser = new User_VM();
		User_VM userKhachvanglai_bien = new User_VM();
		List<CustomerPoint_VM> _lstCustomerPoint = new List<CustomerPoint_VM>();


		public int ActiveTabSearchUser { get; set; } = 1;
		public int paymentmethodid { get; set; } = 2;

		private List<Province_VM> _lstTinhTp = new List<Province_VM>();
		private List<District_VM> _lstQuanHuyen = new List<District_VM>();
		private List<Ward_VM> _lstXaPhuong = new List<Ward_VM>();
		private List<Province_VM> _lstTinhTp_Data = new List<Province_VM>();
		private List<District_VM> _lstQuanHuyen_Data = new List<District_VM>();
		private List<Ward_VM> _lstXaPhuong_Data = new List<Ward_VM>();


		private List<ProductItem_VM> _lstProductItem = new List<ProductItem_VM>();
		private List<Size_VM> _lstSizeAll = new List<Size_VM>();
		private List<Color_VM> _lstColorAll = new List<Color_VM>();
		private List<Size_VM> _lstSize = new List<Size_VM>();
		private List<Color_VM> _lstColor = new List<Color_VM>();


		private List<BillItem_VM> _lstBillItemOnBill = new List<BillItem_VM>();
		private List<BillItem_VM> _lstBillItemOnBillShow = new List<BillItem_VM>();
		private List<ProductItem_Show_VM> _lstProductItemShow = new List<ProductItem_Show_VM>();
		private List<BillItemShowSellStall> _lstBillItemShow = new List<BillItemShowSellStall>();

		public int Tongtienhang { get; set; } = 0;
		public string TongtienhangText { get; set; } = "0";

		public int DiemToiDa { get; set; } = 0;

		public int Tongtien { get; set; } = 0;
		public string TongtienText { get; set; } = "0";

		public int InputTichDiem { get; set; } = 0;

		public int InputTienMat { get; set; } = 0;
		public int InputChuyenKhoan { get; set; } = 0;
		public int CountPayment { get; set; } = 0;


		public int tienRefund { get; set; } = 0;
		public string tienRefundText { get; set; } = "0";
		public string tienRefundFormat { get; set; } = "";


		public int? SoluongProductItem { get; set; } = 0;
		public int SoluongProductItemMua { get; set; } = 10;

		public Guid IdSize { get; set; }
		public Guid IdColor { get; set; }



		public string nameNguoiNhan { get; set; } = "";
		public string phoneNumberNguoinhan { get; set; } = "";
		public string _TinhTp { get; set; }
		public string _QuanHuyen { get; set; }
		public string _PhuongXa { get; set; }

		public string AddressDetail { get; set; } = "";
		public string NoteAddresShip { get; set; } = "";
		public int PhiShip { get; set; } = 0;

		public string ShowDiaChi { get; set; } = "";


		public string nameKhachhang { get; set; } = "";
		public string numberPhoneKhachHang { get; set; } = "";
		public string nameKhachhangdefault { get; set; } = "";
		public string numberPhoneKhachHangdefault { get; set; } = "";
		public int pointKhachhang { get; set; } = 0;



		public Guid BillId { get; set; }

		public bool activeTabThemThongTin { get; set; }
		public bool activeTabDungDiem { get; set; }
		public bool activeTienMat { get; set; } = false;
		public bool activeChuyenKhoan { get; set; } = false;
		public bool activeBtnAddProductToBill { get; set; } = false;
		public bool activePlus { get; set; } = false;
		public Guid activeSize { get; set; }
		public Guid activeColor { get; set; }
		public bool checkPopupAddress { get; set; } = true;
		public bool checkBillIsNull { get; set; } = true;

		public bool checkPopupAddUser { get; set; } = true;

		public bool isLoader { get; set; } = false;



		protected override async Task OnInitializedAsync()
		{
			isLoader = true;
			_lstPrI_show_VM = await _client.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");
			_lstImg_PI = await _client.GetFromJsonAsync<List<Image_Join_ProductItem>>("https://localhost:7141/api/Image/GetAllImage_PrductItem");
			_lstP = await _client.GetFromJsonAsync<List<Products_VM>>("https://localhost:7141/api/product/get_allProduct");
			_lstP_1 = await _client.GetFromJsonAsync<List<Products_VM>>("https://localhost:7141/api/product/get_allProduct");
			_lstUser = await _client.GetFromJsonAsync<List<User_VM>>("https://localhost:7141/api/user/get-user");
			_lstUser_1 = await _client.GetFromJsonAsync<List<User_VM>>("https://localhost:7141/api/user/get-user");
			_lstBillItemOnBill = await _client.GetFromJsonAsync<List<BillItem_VM>>("https://localhost:7141/api/BillItem/get_alll_bill_item");
			_lstProductItemShow = await _client.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");
			var userKhachvanglai1 = _lstUser_1.FirstOrDefault(c => c.UserName == "khachvanglai");
			var userKhachvanglai = _lstUser.FirstOrDefault(c => c.UserName == "khachvanglai");
			userKhachvanglai_bien = userKhachvanglai;
			getuser = userKhachvanglai;

			_lstUser_1.Remove(userKhachvanglai1);
			_lstUser.Remove(userKhachvanglai);

			_lstTinhTp_Data = await _client.GetFromJsonAsync<List<Province_VM>>("https://api.npoint.io/ac646cb54b295b9555be"); // api tỉnh tp
			_lstTinhTp = _lstTinhTp_Data;
			_lstQuanHuyen_Data = await _client.GetFromJsonAsync<List<District_VM>>("https://api.npoint.io/34608ea16bebc5cffd42");
			_lstXaPhuong_Data = await _client.GetFromJsonAsync<List<Ward_VM>>("https://api.npoint.io/dd278dc276e65c68cdf5");
			_lstCustomerPoint = await _client.GetFromJsonAsync<List<CustomerPoint_VM>>("https://localhost:7141/api/CustomerPoint/getAllCustomerPoint");
			isLoader = false;
		}



		public async Task addBill()
		{
			var codeToday = DateTime.Now.ToString().Replace("/", "").Substring(0, 4) +
								DateTime.Now.Year.ToString().Substring(2);
			var id = Guid.NewGuid();
			bill = new Bill_VM();
			bill.Id = id;
			bill.Status = 5;
			bill.Type = 2;//offline
			bill.PaymentMethodId = Guid.Parse("261d402e-dfa8-4213-9e29-4fdd6fc6b95d");
			bill.UserId = getuser.Id;


			_lstBill = (await _client.GetFromJsonAsync<List<Bill_VM>>("https://localhost:7141/api/Bill/get_alll_bill")).Where(c => c.BillCode.StartsWith(codeToday)).ToList();


			if (_lstBill.Count == 0) bill.BillCode = codeToday + "1";
			else bill.BillCode = codeToday + _lstBill.Max(c => int.Parse(c.BillCode.Substring(6)) + 1).ToString();


			var a = await _client.PostAsJsonAsync("https://localhost:7141/api/Bill/Post-Bill", bill);
			if (a.StatusCode.ToString() == "OK")
			{
				_lstBill_Vm_show.Add(bill);
				BillId = id;
				await GetBillItemShowOnBill(id);
				checkBillIsNull = false;
			}
		}
		private async Task getBillId(Guid id)
		{
			if (_lstBill_Vm_show.Count > 0)
			{
				var x = _lstBill_Vm_show.FirstOrDefault(x => x.Id == id);
				BillId = x.Id;
			}
			var _lstProductItem = (await _client.GetFromJsonAsync<List<BillItem_VM>>("https://localhost:7141/api/BillItem/getbilldetail/" + BillId)).ToList();

			activeTabThemThongTin = false;
			activeTienMat = false;
			activeChuyenKhoan = false;
			activeTabDungDiem = false;
			InputTichDiem = default;


			await GetBillItemShowOnBill(id);
		}
		public void getPaymetMethod(int id)
		{
			paymentmethodid = id;
		}
		public async Task closeBill(Guid id)
		{
			var z = _lstBill_Vm_show.FirstOrDefault(x => x.Id == id);
			_lstBill_Vm_show.Remove(z);
			if (_lstBill_Vm_show.Count == 0)
			{
				BillId = default;
				_lstBillItemShow.Clear();
				checkBillIsNull = true;

				activeTabDungDiem = false;
				InputTichDiem = 0;
				Tongtienhang = 0;
				TongtienhangText = "0";
				Tongtien = 0;
				TongtienText = "0";
				activeTienMat = false;
				activeChuyenKhoan = false;
				InputTienMat = 0;
				InputChuyenKhoan = 0;
				PhiShip = 0;

				return;
			}
			if (_lstBill_Vm_show.Count > 0)
			{
				BillId = _lstBill_Vm_show[0].Id;
			}

		}
		public void SearchUser(ChangeEventArgs e)
		{
			string a = RemoveUnicode(e.Value.ToString().ToLower());
			//if (String.IsNullOrEmpty(e.Value.ToString()))
			//{
			//    _lstUser = _lstUser_1;
			//}
			//if (_lstUser.Count == 0)
			//{
			//    _lstUser = _lstUser_1;
			//}
			_lstUser = _lstUser_1;


			_lstUser = _lstUser.Where(c => RemoveUnicode(c.Name.ToLower()).Contains(a) || c.PhoneNumber.Contains(e.Value.ToString())).ToList();

			var userKhachvanglai = _lstUser.FirstOrDefault(c => c.UserName == "khachvanglai");
			_lstUser.Remove(userKhachvanglai);
			ActiveTabSearchUser = 2;
		}

		public void SearchProduct(ChangeEventArgs e)
		{
			_lstP = _lstP_1;
			string productName = RemoveUnicode(e.Value.ToString().ToLower());

			if (e.Value == null || e.Value.ToString() == "" || e.Value.ToString() == string.Empty)
			{
				_lstP = _lstP_1;
				return;
			}
			//if (_lstUser.Count == 0)
			//{
			//    _lstP = _lstP_1;

			//}
			_lstP = _lstP.Where(c => RemoveUnicode(c.Name.ToLower()).Contains(productName) || RemoveUnicode(c.ProductCode.ToLower()).Contains(productName)).ToList();
			//Chưa xong còn search theo mã nữa
		}

		public void offTabSearchUser()
		{
			ActiveTabSearchUser = 1;
		}
		public async Task GetUserFormSearchUser(Guid id)
		{
			_lstCustomerPoint = await _client.GetFromJsonAsync<List<CustomerPoint_VM>>("https://localhost:7141/api/CustomerPoint/getAllCustomerPoint");


			getuser = _lstUser_1.FirstOrDefault(x => x.Id == id);
			pointKhachhang = Convert.ToInt32(_lstCustomerPoint.FirstOrDefault(x => x.UserID == getuser.Id).Point);
			ActiveTabSearchUser = 1;
		}
		public void ClearInfoUser()
		{
			getuser = userKhachvanglai_bien;

			InputTichDiem = 0;
			if (activeTabThemThongTin == false)
			{
				Tongtien = Tongtienhang;
				TongtienText = FormatNumber(Tongtien);
			}
			//GetBillItemShowOnBill(BillId);
			CheckInputPayment();
			CheckRefund();
		}



		public void ActiveTabTienMat()
		{
			activeTienMat = true;
		}
		public void ActiveTabChuyenKhoan()
		{
			activeChuyenKhoan = true;
		}
		public void CloseTabTienMat()
		{
			InputTienMat = 0;
			activeTienMat = false;
			CheckInputPayment();
			CheckRefund();
		}
		public void CloseTabChuyenKhoan()
		{
			InputChuyenKhoan = 0;
			activeChuyenKhoan = false;
			CheckInputPayment();
			CheckRefund();

		}

	
		public async Task getProductChooseSizeAndColor(Guid IdProduct)
		{

			activeSize = default;
			SoluongProductItemMua = 1;
			SoluongProductItem = 0;
			if (BillId == default)
			{
				_toastService.ShowError("Vui lòng thêm hóa đơn trước khi thanh toán");
				return;
			}

			_lstProductItem = (await _client.GetFromJsonAsync<List<ProductItem_VM>>($"https://localhost:7141/api/productitem/get_all_productitem_byProduct/{IdProduct}")).ToList();

			//Lấy list size All
			_lstSizeAll = (await _client.GetFromJsonAsync<List<Size_VM>>("https://localhost:7141/api/Size/get_size")).ToList();

			//Lấy các size hiện có của Product
			List<string> _lstSizeSample = new List<string> { "XS", "S", "M", "L", "XL", "2XL", "3XL", "4XL", "5XL" };
			var listSizeId = _lstProductItem.Select(c => c.SizeId).Distinct().ToList();

			_lstSize.Clear();
			_lstColor.Clear();
			foreach (var item in listSizeId)
			{
				_lstSize.Add(_lstSizeAll.FirstOrDefault(x => x.Id == item));
			}
			_lstSize = _lstSize.OrderBy(c => _lstSizeSample.IndexOf(c.Name)).ToList();
			//Lấy list size của Product


			//Lấy list số của Product
			//Lấy số lượng của Product Item

			CheckValidateAddProductToBill();
		}
		public async Task getSizeAndShowColor(Guid IdSize)
		{
			activeSize = default;
			activeSize = IdSize;
			activeColor = default;


			_lstColorAll = (await _client.GetFromJsonAsync<List<Color_VM>>("https://localhost:7141/api/Color/get_color")).ToList();
			var _listColorId = _lstProductItem.Select(c => c.ColorId).Distinct().ToList();

			_lstColor.Clear();
			foreach (var item in _listColorId)
			{
				_lstColor.Add(_lstColorAll.FirstOrDefault(x => x.Id == item));
			}
			CheckValidateAddProductToBill();
		}
		public async Task getColorAndgetQuantityProductItem(Guid IdColor)
		{
			activeColor = default;
			activeColor = IdColor;

			var a = _lstProductItem.FirstOrDefault(x => x.ColorId == IdColor && x.SizeId == activeSize);

            SoluongProductItem = a.AvaiableQuantity;

			List<BillDetailShow> _lstBillItem = await _client.GetFromJsonAsync<List<BillDetailShow>>("https://localhost:7141/api/BillItem/get_alll_bill_item_show");

			var checkroductItemInBillItem = _lstBillItem.FirstOrDefault(q => q.ProductItemId == a.Id && q.BillID == BillId);
            if (checkroductItemInBillItem != null)
			{
				SoluongProductItem -= checkroductItemInBillItem.Quantity;
            }

			if (SoluongProductItem < SoluongProductItemMua)
			{
				activePlus = true;
			}
			else
			{
				activePlus = false;
			}
			CheckValidateAddProductToBill();
		}
		public async Task AddProductToBill()
		{

			//Thực hiện add productItem vào hóa đơn chờ
			CheckValidateAddProductToBill();
			//vừa phải add vào hóa đơn chi tiết thật và add vào hóa đơn chi tiết ảo 
			//add vào hòa đơn thật nếu thành công add vào hóa đơn ảo 
			//add vào hóa đơnt thật

			var ProductItem = _lstProductItem.FirstOrDefault(x => x.ColorId == activeColor && x.SizeId == activeSize);
			//var ProductItemShow = _lstProductItemShow.FirstOrDefault(x => x.Id == ProductItem.Id);
			var Soluongtonkho = ProductItem.AvaiableQuantity;
			var price = ProductItem.PriceAfterReduction;
			var status = 1;
			var IdBillAdd = Guid.NewGuid();
			BillItem_VM billadd = new BillItem_VM();
			billadd.Id = IdBillAdd;
			billadd.BillId = BillId;
			billadd.ProductItemsId = ProductItem.Id;
			billadd.Quantity = SoluongProductItemMua;
			billadd.Price = ProductItem.PriceAfterReduction;
			billadd.Status = 1;

			//check trùng sản phẩm 
			//tìm xem có sản phẩm đó trong bill không 
			var listbillItemInBilll = await _client.GetFromJsonAsync<List<BillItems>>("https://localhost:7141/api/BillItem/get_alll_bill_item");
			var billItemInBill = listbillItemInBilll.FirstOrDefault(x => x.ProductItemsId == billadd.ProductItemsId && x.BillId == BillId);

			if (billItemInBill == null)
			{
				var AddBillItemToDB = await _client.PostAsJsonAsync("https://localhost:7141/api/BillItem/Post-BillItem", billadd);
				if (AddBillItemToDB.StatusCode.ToString() == "OK")
				{
					await GetBillItemShowOnBill(BillId);
					_toastService.ShowSuccess("Thêm sản phẩm thành công");
				}
			}
			else if (billItemInBill != null)
			{
				billItemInBill.Quantity += SoluongProductItemMua;

				var reponse = await _client.PutAsJsonAsync("https://localhost:7141/api/BillItem/Put-BillItems", billItemInBill);
				if (reponse.StatusCode.ToString() == "OK")
				{
					await GetBillItemShowOnBill(BillId);
					_toastService.ShowSuccess("Cập nhật số lượng thành công");
				}


			}




		}

		public async Task RemoveBillItem(Guid IdBillItem)
		{
			var RemoveProductItem = await _client.DeleteAsync("https://localhost:7141/api/BillItem/Delete-BillItem?Id=" + IdBillItem.ToString());
			if (RemoveProductItem.StatusCode.ToString() == "OK")
			{
				await GetBillItemShowOnBill(BillId);
				_toastService.ShowWarning("Bạn vừa xóa một sản phẩm khỏi giỏ hàng");
			}


		}

		public async Task AddQuantityToBillItem(Guid BillItemId)
		{
			var BillItem = await _client.GetFromJsonAsync<BillItem_VM>("https://localhost:7141/api/BillItem/get_alll_billItem_byId?Id=" + BillItemId.ToString());
			BillItem.Quantity += 1;
			var ProductItem = await _client.GetFromJsonAsync<ProductItem_VM>("https://localhost:7141/api/productitem/get_all_productitem_byID/" + BillItem.ProductItemsId);
			int AvaiableQuantity = ProductItem.AvaiableQuantity ?? default(int);

			if (BillItem.Quantity <= AvaiableQuantity)
			{
				var UpdateAddQuantity = await _client.PutAsJsonAsync("https://localhost:7141/api/BillItem/Put-BillItems", BillItem);
				await GetBillItemShowOnBill(BillId);
			}

		}
		public async Task MinusQuantityToBillItem(Guid BillItemId)
		{
			var BillItem = await _client.GetFromJsonAsync<BillItem_VM>("https://localhost:7141/api/BillItem/get_alll_billItem_byId?Id=" + BillItemId.ToString());
			BillItem.Quantity -= 1;
			var ProductItem = await _client.GetFromJsonAsync<ProductItem_VM>("https://localhost:7141/api/productitem/get_all_productitem_byID/" + BillItem.ProductItemsId);
			int AvaiableQuantity = ProductItem.AvaiableQuantity ?? default(int);

			if (BillItem.Quantity > 0)
			{
				var UpdateAddQuantity = await _client.PutAsJsonAsync("https://localhost:7141/api/BillItem/Put-BillItems", BillItem);
				await GetBillItemShowOnBill(BillId);
			}

		}


		public void CheckSoluongProductItemMua(ChangeEventArgs e)
		{
			if (int.TryParse(e.Value.ToString(), out int inputValue))
			{
				if (!String.IsNullOrEmpty(e.Value.ToString()))
				{
					SoluongProductItemMua = inputValue;

					if (SoluongProductItem < SoluongProductItemMua)
					{
						activePlus = true;
					}
					else
					{
						activePlus = false;
					}
				}
			}
			CheckValidateAddProductToBill();
		}



		public async Task GetBillItemShowOnBill(Guid IdBill)
		{
			_lstBillItemShow.Clear();
			Tongtienhang = 0;
			DiemToiDa = 0;
			TongtienhangText = "0";
			Tongtien = 0;
			TongtienText = "0";

			var _lstBillItem = await _client.GetFromJsonAsync<List<BillItem_VM>>("https://localhost:7141/api/BillItem/get_alll_bill_item");

			var _lstBillItemOnBill = _lstBillItem.Where(x => x.BillId == IdBill).ToList();

			//lấy toàn bộ các productItem của billItem


			_lstProductItemShow = await _client.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");


			foreach (var item in _lstBillItemOnBill)
			{
				//Lấy ProductItemShow
				ProductItem_Show_VM ProductItemShow = _lstProductItemShow.FirstOrDefault(x => x.Id == item.ProductItemsId);

				BillItemShowSellStall BillItemShowSellStall = new BillItemShowSellStall();


				BillItemShowSellStall.Id = item.Id;
				BillItemShowSellStall.ProductItemId = ProductItemShow.Id;
				BillItemShowSellStall.BillId = IdBill;
				BillItemShowSellStall.ProductName = ProductItemShow.Name;
				BillItemShowSellStall.SizeName = ProductItemShow.SizeName;
				BillItemShowSellStall.ColorName = ProductItemShow.ColorName;
				BillItemShowSellStall.AvailableQuantity = ProductItemShow.AvaiableQuantity;
				BillItemShowSellStall.Quantity = item.Quantity;
				BillItemShowSellStall.Price = ProductItemShow.PriceAfterReduction;
				BillItemShowSellStall.Status = 1;
				_lstBillItemShow.Add(BillItemShowSellStall);
				//Lấy thông tin tổng tiền hàng
				var thanhtien = BillItemShowSellStall.Price * BillItemShowSellStall.Quantity;
				Tongtienhang += thanhtien ?? default(int);
				DiemToiDa = Tongtienhang / 10;



				TongtienhangText = FormatNumber(Tongtienhang);

				Tongtien = Tongtienhang;

			}


			if (activeTabDungDiem == true)
			{
				Tongtien -= InputTichDiem;
			}
			else if (activeTabDungDiem == false)
			{
				Tongtien = Tongtienhang;

			}




			//Phai dat o cuoi
			TongtienText = FormatNumber(Tongtien);
			CheckRefund();
		}

		public void ChangeInputTichDiem(ChangeEventArgs e)
		{
			if (int.TryParse(e.Value.ToString(), out int inputValue))
			{
				if (e.Value != "")
				{
					if (BillId != default)
					{
						InputTichDiem = inputValue;
						Tongtien = Tongtienhang;
						Tongtien -= InputTichDiem;
						TongtienText = FormatNumber(Tongtien);
					}
				}
			}
			CheckRefund();
		}

		public void CheckDungDiem(ChangeEventArgs e)
		{
			if ((bool)e.Value == false)
			{
				InputTichDiem = 0;
                Tongtien = Tongtienhang;
			}
			else
			{
				
				if (pointKhachhang > Tongtienhang/10)
				{
					InputTichDiem = Tongtienhang / 10;
                }
				else
				{
                    InputTichDiem = pointKhachhang;
                }
				Tongtien = Tongtienhang;
				Tongtien -= InputTichDiem;
			}
			TongtienText = FormatNumber(Tongtien);
			CheckRefund();
		}

		public void CheckInputPayment()
		{

			if (BillId != default && Tongtien > 0)
			{
				if (activeTienMat == true && activeChuyenKhoan == false)
				{
					CountPayment = InputTienMat;
				}
				else if (activeChuyenKhoan == true && activeTienMat == false)
				{
					CountPayment = InputChuyenKhoan;
				}
				else if (activeChuyenKhoan == true && activeTienMat == true)
				{
					CountPayment = InputTienMat + InputChuyenKhoan;
				}
				else if (activeChuyenKhoan == false && activeTienMat == false)
				{
					CountPayment = 0;
				}
			}
		}

		public void CheckRefund()
		{
			tienRefund = CountPayment - Tongtien;
			if (CountPayment == 0)
			{
				tienRefund = 0;
			}
			tienRefundText = FormatNumber(tienRefund);

			if (tienRefund > 0)
			{
				tienRefundFormat = "Bằng chữ: " + NumberToText(Convert.ToDouble(tienRefund));
			}
			else
			{
				tienRefundFormat = "";
			}
		}

		public void CheckInputTienMat(ChangeEventArgs e)
		{

			if (int.TryParse(e.Value.ToString(), out int inputValue))
			{
				if (inputValue < 0)
				{
					_toastService.ClearAll();
					_toastService.ShowError("Vui lòng không nhập dấu nhỏ hơn 0");
					return;

				}
				if (inputValue != 0 || inputValue != null)
				{
					InputTienMat = inputValue;
				}
				else
				{
					InputTienMat = 0;
				}
				CheckInputPayment();
				CheckRefund();
			}

		}
		public void CheckInputChuyenKhoan(ChangeEventArgs e)
		{
			if (int.TryParse(e.Value.ToString(), out int inputValue))
			{
				if (inputValue < 0)
				{
					_toastService.ClearAll();
					_toastService.ShowError("Vui lòng không nhập dấu nhỏ hơn 0");
				}
				if (inputValue != 0 || inputValue != null)
				{
					InputChuyenKhoan = inputValue;
				}
				else
				{
					InputChuyenKhoan = 0;
				}
				CheckInputPayment();
				CheckRefund();
			}
		}
		public void HandleSubmitAddress()
		{
			checkPopupAddress = true;
			_toastService.ShowSuccess("Thêm địa chỉ thành công");
		}


		public void PlusSoluongProduct()
		{
			if (SoluongProductItemMua < SoluongProductItem)
			{
				SoluongProductItemMua += 1;
			}

		}
		public void MinusSoluongProduct()
		{
			if (SoluongProductItemMua > 1)
			{
				SoluongProductItemMua -= 1;
			}

		}

		public class BillItemShowSellStall
		{
			public Guid Id { get; set; }
			public Guid BillId { get; set; }
			public Guid ProductItemId { get; set; }
			public string ProductName { get; set; }
			public string SizeName { get; set; }
			public string ColorName { get; set; }
			public int? AvailableQuantity { get; set; }
			public int Quantity { get; set; }
			public int? Price { get; set; }
			public int Status { get; set; }
		}

		public void CheckValidateAddProductToBill()
		{
			if (activeSize == default || activeColor == default || SoluongProductItem <= 0 || SoluongProductItemMua > SoluongProductItem || SoluongProductItemMua < 1 || BillId == default)
			{
				activeBtnAddProductToBill = false;
			}
			else
			{
				activeBtnAddProductToBill = true;
			}
		}



		//validate AddressShip

		public void checkNameNguoiNhan(ChangeEventArgs e)
		{
			nameNguoiNhan = "";
			if (!String.IsNullOrEmpty(e.Value.ToString()))
			{
				nameNguoiNhan = e.Value.ToString().Trim();
			}
			var regex = @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]";
			if (Regex.IsMatch(nameNguoiNhan, regex))
			{
				nameNguoiNhan = "kytudacbiet";
			}
			checkPopupAddress = checkShowPopupAddress();
		}
		public void checkPhoneNumberNguoiNhan(ChangeEventArgs e)
		{
			phoneNumberNguoinhan = "";

			//var regexPhoneNumber = @"^(0[35789])[0-9]{9}$";
			var regexPhoneNumber = @"^(0|84)(2(0[3-9]|1[0-6|8|9]|2[0-2|5-9]|3[2-9]|4[0-9]|5[1|2|4-9]|6[0-3|9]|7[0-7]|8[0-9]|9[0-4|6|7|9])|3[2-9]|5[5|6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])([0-9]{7})$";

			if (!String.IsNullOrEmpty(e.Value.ToString()))
			{
				if (Regex.IsMatch(e.Value.ToString().Trim(), regexPhoneNumber))
				{
					phoneNumberNguoinhan = e.Value.ToString().Trim();
					if (phoneNumberNguoinhan.StartsWith("84"))
					{
						// Chuyển đổi số điện thoại bắt đầu bằng "84" thành "0"
						phoneNumberNguoinhan = "0" + phoneNumberNguoinhan.Substring(2);
					}
				}
			}
			checkPopupAddress = checkShowPopupAddress();
		}
		public async Task ChonTinhTP(ChangeEventArgs e)
		{
			_TinhTp = e.Value.ToString();
			_lstQuanHuyen.Clear();
			_lstXaPhuong.Clear();

			_QuanHuyen = string.Empty;
			_PhuongXa = string.Empty;
			var chon = _lstTinhTp_Data.FirstOrDefault(x => x.Name == _TinhTp);
			if (chon != null)
			{
				_lstQuanHuyen = _lstQuanHuyen_Data.Where(x => x.ProvinceId == chon.Id).ToList();
			}
			checkPopupAddress = checkShowPopupAddress();
		}
		public async Task ChonQuanHuyen(ChangeEventArgs e)
		{
			_QuanHuyen = e.Value.ToString();
			_lstXaPhuong.Clear();
			_PhuongXa = string.Empty;
			var chon = _lstQuanHuyen.FirstOrDefault(x => x.Name == _QuanHuyen);
			if (chon != null)
			{
				_lstXaPhuong = _lstXaPhuong_Data.Where(x => x.DistrictId == chon.Id).ToList();
			}
			checkPopupAddress = checkShowPopupAddress();

		}
		public void ChonXaPhuong(ChangeEventArgs e)
		{
			_PhuongXa = e.Value.ToString();
			checkPopupAddress = checkShowPopupAddress();
		}
		public void checkAddressDetail(ChangeEventArgs e)
		{
			AddressDetail = "";
			if (!String.IsNullOrEmpty(e.Value.ToString()))
			{
				AddressDetail = e.Value.ToString().Trim();
			}
			var regex = @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]";
			if (Regex.IsMatch(AddressDetail, regex))
			{
				AddressDetail = "kytudacbiet";
			}
			checkPopupAddress = checkShowPopupAddress();
		}
		public void checkNoteAddresShip(ChangeEventArgs e)
		{
			NoteAddresShip = "";
			if (!String.IsNullOrEmpty(e.Value.ToString()))
			{
				NoteAddresShip = e.Value.ToString().Trim();
			}
			checkPopupAddress = checkShowPopupAddress();
		}

		public void checkPhiShip(ChangeEventArgs e)
		{
			if (PhiShip == 0 || String.IsNullOrEmpty(e.Value.ToString()))
			{
                if (activeTabDungDiem == true)
                {
                    PhiShip = 0;
                    Tongtien = Tongtienhang - InputTichDiem;
                }
                else
                {
                    PhiShip = 0;
                    Tongtien = Tongtienhang;
                }
            }
            if (activeTabDungDiem == true)
            {
                PhiShip = 0;
                Tongtien = Tongtienhang - InputTichDiem;
            }
            else
            {
                PhiShip = 0;
                Tongtien = Tongtienhang;
            }
            TongtienText = FormatNumber(Tongtien);
            if (int.TryParse(e.Value.ToString(), out int inputValue))
			{
				var regex = @"^[0-9]+$";
				if (Regex.IsMatch(inputValue.ToString(), regex))
				{
                    PhiShip = 0;
                    PhiShip = inputValue;
					Tongtien += PhiShip;
					TongtienText = FormatNumber(Tongtien);
				}
				else
				{
					PhiShip = 0;
				}
			}
			CheckInputPayment();
			CheckRefund();
		}

		public void checkValidateAddress()
		{
			ShowDiaChi = "";
			List<String> listContentShow = new List<String>();
			if (
				String.IsNullOrEmpty(nameNguoiNhan) ||
				nameNguoiNhan == "kytudacbiet" ||
				 String.IsNullOrEmpty(phoneNumberNguoinhan) ||
				 //phoneNumberNguoinhan.Length != 10 ||
				 String.IsNullOrEmpty(_TinhTp) ||
				String.IsNullOrEmpty(_QuanHuyen) ||
				 String.IsNullOrEmpty(_PhuongXa) ||
				 String.IsNullOrEmpty(AddressDetail) ||
				  AddressDetail == "kytudacbiet"
				 )
			{
				if (String.IsNullOrEmpty(nameNguoiNhan) || nameNguoiNhan == "kytudacbiet")
				{
					if (String.IsNullOrEmpty(nameNguoiNhan))
					{
						listContentShow.Add("Tên");
					}
					else if (nameNguoiNhan == "kytudacbiet")
					{
						listContentShow.Add("Tên có ký tự đặc biệt");
					}

				}
				if (String.IsNullOrEmpty(phoneNumberNguoinhan))
				{
					listContentShow.Add("Số điện thoại");
				}
				if (String.IsNullOrEmpty(_TinhTp))
				{
					listContentShow.Add("Tỉnh thành");
				}
				if (String.IsNullOrEmpty(_QuanHuyen))
				{
					listContentShow.Add("Quận huyện");
				}
				if (String.IsNullOrEmpty(_PhuongXa))
				{
					listContentShow.Add("Phường Xã");
				}
				if (String.IsNullOrEmpty(AddressDetail) || AddressDetail == "kytudacbiet")
				{
					if (AddressDetail == "kytudacbiet")
					{
						listContentShow.Add("Địa chỉ có ký tự đặc biệt");
					}
					else if (String.IsNullOrEmpty(AddressDetail))
					{
						listContentShow.Add("Địa chỉ chi tiết");
					}
				}

				string result = string.Join(", ", listContentShow);
				string ContentShow = "Vui lòng kiểm tra lại: " + result;
				_toastService.ShowError(ContentShow);
				checkPopupAddress = true;
			}
			else
			{
				List<string> _listDiaChi = new List<string>();
				_listDiaChi.Clear();
				_listDiaChi.Add(AddressDetail);
				_listDiaChi.Add(_PhuongXa);
				_listDiaChi.Add(_QuanHuyen);
				_listDiaChi.Add(_TinhTp);
				ShowDiaChi = string.Join(", ", _listDiaChi);
				checkPopupAddress = false;
			}
		}
		public bool checkShowPopupAddress()
		{
			if (
			   String.IsNullOrEmpty(nameNguoiNhan) ||
			   String.IsNullOrEmpty(phoneNumberNguoinhan) ||
				//phoneNumberNguoinhan.Length != 10 ||
				String.IsNullOrEmpty(_TinhTp) ||
			   String.IsNullOrEmpty(_QuanHuyen) ||
				String.IsNullOrEmpty(_PhuongXa) ||
				String.IsNullOrEmpty(AddressDetail)
				)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		// addUser Khách hàng
		public void checkNameKhachHang(ChangeEventArgs e)
		{
			nameKhachhang = "";
			if (!String.IsNullOrEmpty(e.Value.ToString()))
			{
				nameKhachhang = e.Value.ToString().Trim();
			}
			var regex = @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]";
			if (Regex.IsMatch(nameNguoiNhan, regex))
			{
				nameKhachhang = "kytudacbiet";
			}
			checkPopupAddUser = checkShowPopupAddUser();
		}
		public void checkPhoneNumberKhachHang(ChangeEventArgs e)
		{
			numberPhoneKhachHang = "";

			//var regexPhoneNumber = @"^(0[35789])[0-9]{9}$";
			var regexPhoneNumber = @"^(0|84)(2(0[3-9]|1[0-6|8|9]|2[0-2|5-9]|3[2-9]|4[0-9]|5[1|2|4-9]|6[0-3|9]|7[0-7]|8[0-9]|9[0-4|6|7|9])|3[2-9]|5[5|6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])([0-9]{7})$";

			if (!String.IsNullOrEmpty(e.Value.ToString()))
			{
				if (Regex.IsMatch(e.Value.ToString().Trim(), regexPhoneNumber))
				{
					numberPhoneKhachHang = e.Value.ToString().Trim();
					if (numberPhoneKhachHang.StartsWith("84"))
					{
						// Chuyển đổi số điện thoại bắt đầu bằng "84" thành "0"
						numberPhoneKhachHang = "0" + numberPhoneKhachHang.Substring(2);
					}
				}
			}
			checkPopupAddUser = checkShowPopupAddUser();
		}

		public async Task getAllUser()
		{
			_lstUser_1 = await _client.GetFromJsonAsync<List<User_VM>>("https://localhost:7141/api/user/get-user");
			nameKhachhangdefault = "";
			numberPhoneKhachHangdefault = "";
		}
		public async Task checkValidateAddUser()
		{
			List<String> listContentShow = new List<String>();
			if (String.IsNullOrEmpty(nameKhachhang) || String.IsNullOrEmpty(numberPhoneKhachHang) || nameKhachhang == "kytudacbiet")
			{
				if (String.IsNullOrEmpty(nameKhachhang) || nameKhachhang == "kytudacbiet")
				{
					if (nameKhachhang == "kytudacbiet")
					{
						listContentShow.Add("Tên người nhận có ký tự đặc biệt");
					}
					else
					{
						listContentShow.Add("Tên người nhận");
					}

				}
				if (String.IsNullOrEmpty(numberPhoneKhachHang))
				{
					listContentShow.Add("Số điện thoại");
				}
				string result = string.Join(", ", listContentShow);
				string ContentShow = "Vui lòng kiểm tra lại: " + result;
				_toastService.ShowError(ContentShow);
				checkPopupAddUser = true;
			}
			else
			{
				string email = numberPhoneKhachHang.ToString() + "@gmail.com";
				string username = "khachtaiquay" + numberPhoneKhachHang.ToString();

				AddUserByAdmin userAdd = new AddUserByAdmin();
				Guid idUser = Guid.NewGuid();
				userAdd.id = idUser;
				userAdd.username = username;
				userAdd.password = "123456";
				userAdd.email = email;
				userAdd.phonenumber = numberPhoneKhachHang;
				userAdd.name = nameKhachhang;
				userAdd.role = "User";
				userAdd.status = 10;
				userAdd.sex = true;

				_lstUser_1 = await _client.GetFromJsonAsync<List<User_VM>>("https://localhost:7141/api/user/get-user");
				var checkNullUser = _lstUser_1.FirstOrDefault(x => x.PhoneNumber == numberPhoneKhachHang);

				if (_lstUser_1.Count > 0 && checkNullUser != null)
				{
					_toastService.ShowError("Khách hàng đã tồn tại");
					return;
				}
				var reponse = await _client.PostAsJsonAsync("https://localhost:7141/api/user/add-employee-admin", userAdd);
				string a = reponse.StatusCode.ToString();

				if (reponse.StatusCode.ToString() == "Created")
				{
					_lstUser_1 = await _client.GetFromJsonAsync<List<User_VM>>("https://localhost:7141/api/user/get-user");
					getuser = _lstUser_1.FirstOrDefault(x => x.PhoneNumber == numberPhoneKhachHang);
					_lstCustomerPoint = await _client.GetFromJsonAsync<List<CustomerPoint_VM>>("https://localhost:7141/api/CustomerPoint/getAllCustomerPoint");

					var CustomerPoint = _lstCustomerPoint.FirstOrDefault(x => x.UserID == getuser.Id);
					pointKhachhang = Convert.ToInt32(CustomerPoint.Point);

					_toastService.ShowSuccess("Thêm khách hàng thành công");
					checkPopupAddUser = true;
				}
				else
				{
					//string err = reponse.ToJson();
					_toastService.ShowError("Đã có lỗi xảy ra");
				}
			}

		}
		public bool checkShowPopupAddUser()
		{
			if (String.IsNullOrEmpty(nameKhachhang) || String.IsNullOrEmpty(numberPhoneKhachHang) || nameKhachhang == "kytudacbiet")
			{
				return true;
			}
			else
			{
				if (_lstUser_1.Count > 0)
				{
					var user = _lstUser_1.FirstOrDefault(x => x.PhoneNumber == numberPhoneKhachHang);
					if (user != null)
					{
						return true;
					}
					else
					{
						return false;
					}
				}
				else
				{
					return true;
				}

			}
		}

		// Thanh Toan
		public void checkValidateThanhToan()
		{
			//Check điều kiện sai
			if (BillId == default)
			{
				_toastService.ShowError("Vui lòng thêm hóa đơn chờ và điền thông tin trước khi thanh toán");
				return;
			}
			if (_lstBillItemShow.Count() == 0)
			{
				_toastService.ShowError("Vui lòng thêm hóa đơn vào sản phẩm");
				return;
			}
			if (activeTabThemThongTin == true)
			{
				if (ShowDiaChi == "")
				{
					_toastService.ShowError("Vui lòng thêm địa chỉ");
					return;
				}
				if (PhiShip == 0)
				{
					_toastService.ShowError("Vui lòng kiểm tra lại phí vận chuyển");
					return;
				}
			}
			if (getuser != null && activeTabDungDiem == true)
			{
				if (InputTichDiem > pointKhachhang)
				{
					_toastService.ShowError("Dùng quá số diểm hiện có");
					return;
				}
				if (InputTichDiem > Tongtien / 10)
				{
					_toastService.ShowError("Dùng quá số diểm cho phép");
					return;
				}
			}
			CheckInputPayment();
			if (CountPayment == 0)
			{
				_toastService.ShowError("Vui lòng nhập số tiền khách cần thanh toán");
				return;
			}
			if (CountPayment < Tongtien)
			{
				_toastService.ShowError("Số tiền khách thanh toán chưa đủ");
				return;
			}
		}

		public async Task ThanhToan()
		{
			if (BillId == default)
			{
				_toastService.ShowError("Vui lòng thêm hóa đơn chờ và điền thông tin trước khi thanh toán");
				return;
			}
			if (_lstBillItemShow.Count() == 0)
			{
				_toastService.ShowError("Vui lòng thêm hóa đơn vào sản phẩm");
				return;
			}
			if (activeTabThemThongTin == true)
			{
				if (ShowDiaChi == "")
				{
					_toastService.ShowError("Vui lòng thêm địa chỉ");
					return;
				}
				if (PhiShip == 0)
				{
					_toastService.ShowError("Vui lòng kiểm tra lại phí vận chuyển");
					return;
				}
			}
			if (getuser != null && activeTabDungDiem == true)
			{
				if (InputTichDiem > pointKhachhang)
				{
					_toastService.ShowError("Dùng quá số diểm hiện có");
					return;
				}
				if (InputTichDiem > Tongtien / 10)
				{
					_toastService.ShowError("Dùng quá số diểm cho phép");
					return;
				}
			}
			CheckInputPayment();
			if (CountPayment == 0)
			{
				_toastService.ShowError("Vui lòng nhập số tiền khách cần thanh toán");
				return;
			}
			if (CountPayment < Tongtien)
			{
				_toastService.ShowError("Số tiền khách thanh toán chưa đủ");
				return;
			}

			var lstPaymentMethod = await _client.GetFromJsonAsync<List<PaymentMethod_VM>>("https://localhost:7141/api/paymentMethod/get_all_paymentMethod");

			var lstbill = await _client.GetFromJsonAsync<List<Bill_VM>>("https://localhost:7141/api/Bill/get_alll_bill");

			Bill_VM billMua = lstbill.FirstOrDefault(x => x.Id == BillId);

			if (billMua == null)
			{
				_toastService.ShowError("Đã có lỗi xảy ra 1");
				return;
			}
			if (activeTabThemThongTin == true)
			{
				billMua.Note = NoteAddresShip;
				billMua.Recipient = nameNguoiNhan;
				billMua.District = _TinhTp;
				billMua.Province = _QuanHuyen;
				billMua.WardName = _PhuongXa;
				billMua.ToAddress = AddressDetail;
				billMua.NumberPhone = phoneNumberNguoinhan;
				billMua.ShippingFee = PhiShip;
			}
			if (activeTienMat == true && activeChuyenKhoan == false)
			{

				bill.PaymentMethodId = lstPaymentMethod.FirstOrDefault(x => x.Name == "Tiền mặt").Id;
				if (bill.PaymentMethodId == default || bill.PaymentMethodId == null)
				{
					_toastService.ShowError("Đã có lỗi xảy ra 2");
					return;
				}
			}
			else if (activeTienMat == false && activeChuyenKhoan == true)
			{
				bill.PaymentMethodId = lstPaymentMethod.FirstOrDefault(x => x.Name == "Chuyển khoản").Id;
				if (bill.PaymentMethodId == default || bill.PaymentMethodId == null)
				{
					_toastService.ShowError("Đã có lỗi xảy ra 3");
					return;
				}
			}
			else if (activeTienMat == true && activeChuyenKhoan == true)
			{
				bill.PaymentMethodId = lstPaymentMethod.FirstOrDefault(x => x.Name == "Chuyển khoản và tiền mặt").Id;
				if (bill.PaymentMethodId == default || bill.PaymentMethodId == null)
				{
					_toastService.ShowError("Đã có lỗi xảy ra 4");
					return;
				}
			}
			
			//tiêu điểm cho khách hàng nếu có 
			if (activeTabDungDiem==true)
			{
				var _lstPoint = await _client.GetFromJsonAsync<List<CustomerPoint_VM>>("https://localhost:7141/api/CustomerPoint/getAllCustomerPoint");
				CustomerPoint_VM PointId = _lstPoint.FirstOrDefault(x => x.UserID == getuser.Id);

				//them ban ghi history tiêu điểm 
				HistoryConsumerPoint_VM htsCustomerPoint = new HistoryConsumerPoint_VM();
				htsCustomerPoint.Id = Guid.NewGuid();
				htsCustomerPoint.ConsumerPointId = PointId.UserID;
				htsCustomerPoint.FormulaId = default;
				htsCustomerPoint.Point = InputTichDiem;
				htsCustomerPoint.Status = 1;

				var reponsePostHtsCtm = await _client.PostAsJsonAsync("https://localhost:7141/api/HistoryConsumerPoint/add-HistoryConsumerPoint", htsCustomerPoint);
				if (reponsePostHtsCtm.StatusCode.ToString() != "OK" )
				{
					_toastService.ShowError("Đã có lỗi xảy ra 5");
					return;
				}
				// update số điểm 
				PointId.Point = (Convert.ToInt32(PointId.Point) - htsCustomerPoint.Point).ToString();
				var reponseUpdatePointUser = await _client.PutAsJsonAsync("https://localhost:7141/api/CustomerPoint/putCustomerPoint", PointId);
				if (reponseUpdatePointUser.StatusCode.ToString()!="OK")
				{
					_toastService.ShowError("Đã có lỗi xảy ra 6");
					return;
				};

            }

			//tích điểm cho khách hàng nếu có
			if (getuser != null)
			{
                //lấy công thức tính điểm
                var _lstFomula = await _client.GetFromJsonAsync<List<Formula_VM>>("https://localhost:7141/api/Formula/get_formula");
                var Congthuctinhdiem = _lstFomula.FirstOrDefault(x => x.Status == 1);
                // lay ban ghi bang diem cua khách hàng 
                var _lstPoint = await _client.GetFromJsonAsync<List<CustomerPoint_VM>>("https://localhost:7141/api/CustomerPoint/getAllCustomerPoint");
                CustomerPoint_VM PointId = _lstPoint.FirstOrDefault(x => x.UserID == getuser.Id);

                //them ban ghi history tiêu điểm 
                HistoryConsumerPoint_VM htsCustomerPoint = new HistoryConsumerPoint_VM();
                htsCustomerPoint.Id = Guid.NewGuid();
                htsCustomerPoint.ConsumerPointId = PointId.UserID;
                htsCustomerPoint.FormulaId = Congthuctinhdiem.Id;
                htsCustomerPoint.Point = Tongtien/Congthuctinhdiem.Coefficient;
                htsCustomerPoint.Status = 1;

                var reponsePostHtsCtm = await _client.PostAsJsonAsync("https://localhost:7141/api/HistoryConsumerPoint/add-HistoryConsumerPoint", htsCustomerPoint);
                if (reponsePostHtsCtm.StatusCode.ToString() != "OK")
                {
                    _toastService.ShowError("Đã có lỗi xảy ra 7");
                    return;
                }
                // update số điểm 
                PointId.Point = (Convert.ToInt32(PointId.Point) + htsCustomerPoint.Point).ToString();
                var reponseUpdatePointUser = await _client.PutAsJsonAsync("https://localhost:7141/api/CustomerPoint/putCustomerPoint", PointId);
                if (reponseUpdatePointUser.StatusCode.ToString() != "OK")
                {
                    _toastService.ShowError("Đã có lỗi xảy ra 8");
                    return;
                };


            }

            billMua.TotalAmount = Tongtien;
			billMua.Cash = InputTienMat;
			billMua.CustomerPayment = InputChuyenKhoan;
			billMua.CreateDate = DateTime.Now;
			billMua.Status = 2;
			billMua.Type = 2;

			



			//check số lượng sản phẩm trong db xem còn không
			//Gett all bill Item 
			var _lstBillItem = await _client.GetFromJsonAsync<List<BillDetailShow>>("https://localhost:7141/api/BillItem/get_alll_bill_item_show");
			var _lstBillItem1 = _lstBillItem.Where(x => x.BillID == BillId).ToList();

            var _lstPrductItem = await _client.GetFromJsonAsync<List<ProductItem_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");
			foreach (var x in _lstBillItem1)
			{
				
					var productItem = _lstPrductItem.FirstOrDefault(z => z.Id == x.ProductItemId);
					if (productItem == null)
					{
						_toastService.ShowError("Đã có lỗi xảy ra 9");
						return;
					}					
					if (x.Quantity > productItem.AvaiableQuantity)
					{
						_toastService.ShowError("Đã có lỗi xảy ra 10");
						return;
					}
				productItem.AvaiableQuantity -= x.Quantity;

                var reponseUpdateQuantity = await _client.PutAsJsonAsync("https://localhost:7141/api/productitem/update_productitem", productItem);



					if (reponseUpdateQuantity.StatusCode.ToString()!="OK")
					{
						_toastService.ShowError("Đã có lỗi xảy ra 11");
						return;
					}
                    var reponseUpdatePromotion = await _client.PutAsJsonAsync("https://localhost:7141/api/promotion/update_quantity_promotion/d16e6a1f-e30c-4cb2-9bfc-6aa812478b2d", productItem.Id);
					if (reponseUpdatePromotion.StatusCode.ToString()!="OK")
					{
						_toastService.ShowError("Đã có lỗi xảy ra 12");
						return;
					}
                
			}
			//trừ số lượng sp trong db
			
			
            //update trạng thái bill thành 1;
            var reponseUpdateBill = await _client.PutAsJsonAsync("https://localhost:7141/api/Bill/Put-Bill", billMua);
			if (reponseUpdateBill.StatusCode.ToString() == "OK")
			{
				_toastService.ShowSuccess("Đơn hàng đã thanh toán thành công");
				//reset các biến cần thiết
				var billXoa = _lstBill_Vm_show.FirstOrDefault(x => x.Id == billMua.Id);
				_lstBill_Vm_show.Remove(billXoa);

				if (_lstBill_Vm_show.Count > 0)
				{
					BillId = _lstBill_Vm_show[0].Id;
				}
				else
				{
					BillId = default;
				}
				await GetBillItemShowOnBill(BillId);
				//clear daichi
				activeTabThemThongTin = false;
				nameNguoiNhan = "";
				phoneNumberNguoinhan = "";
				_TinhTp = string.Empty;
				_PhuongXa = string.Empty;
				_QuanHuyen = string.Empty;
				AddressDetail = "";
				NoteAddresShip = "";
				PhiShip = 0;
				//clear khach hang
				ClearInfoUser();
				// clear dung diem
				activeTabDungDiem = false;
				InputTichDiem = 0;
				// clear tiền khách đưa 
				activeTienMat = false;
				activeChuyenKhoan = false;
				InputTienMat = 0;
				InputChuyenKhoan = 0;
				// clear text tien khach dua và refund
				TongtienText = "0";
				tienRefundText = "0";
				tienRefundFormat = "";
				CountPayment = 0;



            }
			else
			{
				_toastService.ShowError("Đã thanh toán đc r hehe");
				return;
			}
			
			

		}


		//Format
		static string FormatNumber(int number)
		{
			// Sử dụng định dạng chuỗi để biến đổi số
			return number.ToString("N0", CultureInfo.InvariantCulture);
		}
		public static string RemoveUnicode(string text)
		{
			string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
	"đ",
	"é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
	"í","ì","ỉ","ĩ","ị",
	"ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
	"ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
	"ý","ỳ","ỷ","ỹ","ỵ",};
			string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
	"d",
	"e","e","e","e","e","e","e","e","e","e","e",
	"i","i","i","i","i",
	"o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
	"u","u","u","u","u","u","u","u","u","u","u",
	"y","y","y","y","y",};
			for (int i = 0; i < arr1.Length; i++)
			{
				text = text.Replace(arr1[i], arr2[i]);
				text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
			}
			return text;
		}

		private static string NumberToText(double inputNumber, bool suffix = true)
		{
			string[] unitNumbers = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
			string[] placeValues = new string[] { "", "nghìn", "triệu", "tỷ" };
			bool isNegative = false;

			// -12345678.3445435 => "-12345678"
			string sNumber = inputNumber.ToString("#");
			double number = Convert.ToDouble(sNumber);
			if (number < 0)
			{
				number = -number;
				sNumber = number.ToString();
				isNegative = true;
			}
			int ones, tens, hundreds;

			int positionDigit = sNumber.Length;   // last -> first

			string result = " ";

			if (positionDigit == 0)
				result = unitNumbers[0] + result;
			else
			{
				// 0:       ###
				// 1: nghìn ###,###
				// 2: triệu ###,###,###
				// 3: tỷ    ###,###,###,###
				int placeValue = 0;

				while (positionDigit > 0)
				{
					// Check last 3 digits remain ### (hundreds tens ones)
					tens = hundreds = -1;
					ones = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
					positionDigit--;
					if (positionDigit > 0)
					{
						tens = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
						positionDigit--;
						if (positionDigit > 0)
						{
							hundreds = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
							positionDigit--;
						}
					}

					if ((ones > 0) || (tens > 0) || (hundreds > 0) || (placeValue == 3))
						result = placeValues[placeValue] + result;

					placeValue++;
					if (placeValue > 3) placeValue = 1;

					if ((ones == 1) && (tens > 1))
						result = "một " + result;
					else
					{
						if ((ones == 5) && (tens > 0))
							result = "lăm " + result;
						else if (ones > 0)
							result = unitNumbers[ones] + " " + result;
					}
					if (tens < 0)
						break;
					else
					{
						if ((tens == 0) && (ones > 0)) result = "lẻ " + result;
						if (tens == 1) result = "mười " + result;
						if (tens > 1) result = unitNumbers[tens] + " mươi " + result;
					}
					if (hundreds < 0) break;
					else
					{
						if ((hundreds > 0) || (tens > 0) || (ones > 0))
							result = unitNumbers[hundreds] + " trăm " + result;
					}
					result = " " + result;
				}
			}
			result = result.Trim();
			if (isNegative) result = "Âm " + result;
			return result + (suffix ? " đồng chẵn" : "");
		}



	}

}
