﻿using DATN_Client.SessionService;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using DATN_Shared.ViewModel.DiaChi;
using DATN_Shared.ViewModel.Momo;
using DATN_Shared.ViewModel.Momo.Order;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using Twilio.Types;

namespace DATN_Client.Areas.Customer.Component
{
	public partial class Create_Bill_With_Info
	{
		private HttpClient _httpClient = new();
		[Inject] private NavigationManager _navi { get; set; }
		[Inject] public IHttpContextAccessor _ihttpcontextaccessor { get; set; }
		[Inject] Blazored.Toast.Services.IToastService _toastService { get; set; } // Khai báo khi cần gọi ở code-behind
		private List<CartItems_VM> _lstCI = new();
		private List<Image_Join_ProductItem> _lstImg_PI = new();
		private List<Image_Join_ProductItem> _lstImg_PI_tam = new();
		private List<ProductItem_Show_VM> _lstPrI_show_VM = new();
		private List<PaymentMethod_VM> _lstPayM = new();
		public static Bill_DataAnotation_VM _bill_validate_vm = new Bill_DataAnotation_VM();
		private User_VM? _user_vm = new();
		private User_VM? _userVangLai = new();
		private ProductItem_Show_VM _pi_s_vm = new();
		private OrderInfoModel _ord = new();
		private PaymentMethod_VM _payM = new();
		public int? _tongTienHang { get; set; } = 0;
		public int? _tongTienAll { get; set; } = 0;
		private List<Province_VM> _lstTinhTp = new();
		private List<District_VM> _lstQuanHuyen = new();
		private List<Ward_VM> _lstXaPhuong = new();
		private List<Province_VM> _lstTinhTp_Data = new();
		private List<District_VM> _lstQuanHuyen_Data = new();
		private List<Ward_VM> _lstXaPhuong_Data = new();
		private List<Bill_VM> _lstBill = new();
		private List<ProductItem_VM> _lstPrI_VM = new();
		private List<HistoryConsumerPoint_VM> _lstHCP_VM = new();
		private List<AddressShip_VM> _lst_adrS_User = new();
		private List<VoucherUser_VM> _lstVchUser = new();
		private AddressShip_VM? _adrS_User = new();
		//private Voucher_VM? v_vm = new Voucher_VM();
		public string _TinhTp { get; set; }
		public string _QuanHuyen { get; set; }
		public string? _iduser { get; set; }
		public bool _datHangThanhCong { get; set; } = false;
		public string _voucherCode { get; set; } = string.Empty;
		public int? _tiengiamVoucher { get; set; } = 0;
		public int? _tiengiamDungiem { get; set; } = 0;
		public string _afterClick { get; set; } = string.Empty;
		private bool isLoader = false;
		private int _diemCuaNguoiDung { get; set; } = 0;
		private bool _checkDung { get; set; }
		private bool isModalXacNhan { get; set; } = false;
		protected override async Task OnInitializedAsync()
		{
			_bill_validate_vm = new();
			isLoader = true;
			_iduser = _ihttpcontextaccessor.HttpContext.Session.GetString("UserId");

			//var token = _ihttpcontextaccessor.HttpContext.Session.GetString("Token"); // Gọi token
			//_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); // Xác thực
			_lstPrI_show_VM = await _httpClient.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");
			if (string.IsNullOrEmpty(_iduser))
			{
				_userVangLai = await _httpClient.GetFromJsonAsync<User_VM>($"https://localhost:7141/api/user/get-user-byusername?username=khachvanglai");
				_bill_validate_vm.UserId = _userVangLai.Id;
				_lstCI = SessionServices.GetLstFromSession_LstCI(_ihttpcontextaccessor.HttpContext.Session, "_lstCI_Vanglai");
			}
			else
			{
				_bill_validate_vm.UserId = Guid.Parse(_iduser);
				_user_vm = await _httpClient.GetFromJsonAsync<User_VM>($"https://localhost:7141/api/user/get_user_by_id/{_iduser}");
				_lstCI = await _httpClient.GetFromJsonAsync<List<CartItems_VM>>($"https://localhost:7141/api/CartItems/{_bill_validate_vm.UserId}");
				_lst_adrS_User = await _httpClient.GetFromJsonAsync<List<AddressShip_VM>>($"https://localhost:7141/api/AddressShip/get_address_by_UserID/{_bill_validate_vm.UserId}"); // list địa chỉ của user
				_adrS_User = _lst_adrS_User.FirstOrDefault(c => c.Status == 1);
			}
			if (!_lstCI.Any()) _navi.NavigateTo("/cart", true);
			_diemCuaNguoiDung = int.Parse((await _httpClient.GetFromJsonAsync<CustomerPoint_VM>($"https://localhost:7141/api/CustomerPoint/GetCustomerPoint_byUserID/{_bill_validate_vm.UserId}")).Point);
			_lstImg_PI = (await _httpClient.GetFromJsonAsync<List<Image_Join_ProductItem>>("https://localhost:7141/api/Image/GetAllImage_PrductItem")).OrderBy(c => c.STT).ToList();
			_lstTinhTp_Data = await _httpClient.GetFromJsonAsync<List<Province_VM>>("https://api.npoint.io/ac646cb54b295b9555be"); // api tỉnh tp
			_lstTinhTp = _lstTinhTp_Data;
			_lstQuanHuyen_Data = await _httpClient.GetFromJsonAsync<List<District_VM>>("https://api.npoint.io/34608ea16bebc5cffd42");
			_lstXaPhuong_Data = await _httpClient.GetFromJsonAsync<List<Ward_VM>>("https://api.npoint.io/dd278dc276e65c68cdf5");
			_lstPayM = (await _httpClient.GetFromJsonAsync<List<PaymentMethod_VM>>("https://localhost:7141/api/paymentMethod/get_all_paymentMethod")).Where(c => c.Status == 1).ToList();
			_lstPayM = _lstPayM.Where(x => x.Name == "Thanh toán Momo" || x.Name == "Thanh toán khi nhận hàng (COD)").ToList();
			_lstPrI_VM = await _httpClient.GetFromJsonAsync<List<ProductItem_VM>>("https://localhost:7141/api/productitem/get_all_productitem");
			_lstHCP_VM = await _httpClient.GetFromJsonAsync<List<HistoryConsumerPoint_VM>>("https://localhost:7141/api/HistoryConsumerPoint/get-HistoryConsumerPoint");
			_lstVchUser = await _httpClient.GetFromJsonAsync<List<VoucherUser_VM>>($"https://localhost:7141/api/voucher-user/get-voucherUser-by-userid/{_bill_validate_vm.UserId}"); // list voucher của user
			if (_adrS_User != null)
			{
				_bill_validate_vm.NumberPhone = _adrS_User.NumberPhone;
				_bill_validate_vm.Recipient = _adrS_User.Recipient;
				_bill_validate_vm.ToAddress = _adrS_User.ToAddress;
				// Tự gen
				_bill_validate_vm.Province = _adrS_User.Province;
				await ChonTinhTP();
				_bill_validate_vm.District = _adrS_User.District;
				await ChonQuanHuyen();
				_bill_validate_vm.WardName = _adrS_User.WardName;
			}
			if (_adrS_User == null)
			{
				_bill_validate_vm.NumberPhone = string.Empty;
				_bill_validate_vm.Recipient = string.Empty;
				_bill_validate_vm.ToAddress = string.Empty;
				_bill_validate_vm.Province = string.Empty;
				_bill_validate_vm.District = string.Empty;
				_bill_validate_vm.WardName = string.Empty;
			}
			_payM = _lstPayM.FirstOrDefault(c => c.Name == "Thanh toán khi nhận hàng (COD)");
			_bill_validate_vm.PaymentMethodId = _payM.Id;
			_bill_validate_vm.Note = ShowCart._note;
			_bill_validate_vm.ShippingFee = 0;
			foreach (var x in _lstCI)
			{
				_pi_s_vm = _lstPrI_show_VM.Where(c => c.Id == x.ProductItemId).FirstOrDefault();
				_tongTienHang += x.Quantity * _pi_s_vm.PriceAfterReduction;
			}
			_tongTienAll = _tongTienHang + _bill_validate_vm.ShippingFee;
			isLoader = false;
		}

		public async Task Btn_DatHang()
		{
			if (string.IsNullOrEmpty(_bill_validate_vm.Recipient) || string.IsNullOrEmpty(_bill_validate_vm.NumberPhone) || string.IsNullOrEmpty(_bill_validate_vm.Province) || string.IsNullOrEmpty(_bill_validate_vm.District) || string.IsNullOrEmpty(_bill_validate_vm.WardName)||string.IsNullOrEmpty(_bill_validate_vm.ToAddress))
			{
				_toastService.ShowError("Vui lòng nhập đầy đủ thông tin");
				return;
			}
			Voucher_VM? vch = new Voucher_VM();
			// moi
			//_datHangThanhCong = true;
			//_afterClick = "afterClick";
			if (!_lstCI.Any()) _navi.NavigateTo("/cart", true);
			_lstPrI_show_VM = await _httpClient.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");
			if (_bill_validate_vm.VoucherId != null)
			{
				vch = await _httpClient.GetFromJsonAsync<Voucher_VM>($"https://localhost:7141/api/Voucher/ID?Id={_bill_validate_vm.VoucherId}");
			}
			bool checkSl = false;
			foreach (var a in _lstCI)
			{
				var pi = _lstPrI_show_VM.FirstOrDefault(c => c.Id == a.ProductItemId);
				if (a.Quantity > pi.AvaiableQuantity || pi.Status != 1)
				{
					if (pi.AvaiableQuantity == 0 || pi.Status != 1) _toastService.ShowError($"Sản phẩm {pi.Name} - {pi.ColorName} - {pi.SizeName} hiện đã tạm hết hàng");
					else _toastService.ShowError($"Sản phẩm {pi.Name} - {pi.ColorName} - {pi.SizeName} vượt quá số lượng tồn kho. Quý khách vui lòng giảm số lượng sản phẩm!");
					checkSl = true;
				}
			}
			if (checkSl == true)
			{
				_datHangThanhCong = false;
				_afterClick = string.Empty;
				return;
			}
			if (_lstVchUser.Any(c => c.VoucherId == _bill_validate_vm.VoucherId && c.Status == 0))
			{
				_toastService.ShowError("Bạn đã sử dụng mã giảm giá này rồi, vui lòng chọn mã giảm giá khác!");
				_datHangThanhCong = false;
				_afterClick = string.Empty;
				return;
			}
			if (vch.Quantity == 0 && _bill_validate_vm.VoucherId != null)
			{
				_toastService.ShowError("Mã giảm giá đã hết lượt sử dụng");
				_datHangThanhCong = false;
				_afterClick = string.Empty;
				return;
			}
			if (vch.Status != 1 && _bill_validate_vm.VoucherId != null)
			{
				_toastService.ShowError("Mã giảm giá đã hết hạn");
				_datHangThanhCong = false;
				_afterClick = string.Empty;
				return;
			}
			var regexPN = @"^0\d{9,10}$"; // regex số đt


			if (String.IsNullOrEmpty(_bill_validate_vm.Recipient) || String.IsNullOrEmpty(_bill_validate_vm.NumberPhone) || String.IsNullOrEmpty(_bill_validate_vm.ToAddress) || String.IsNullOrEmpty(_bill_validate_vm.Province) || String.IsNullOrEmpty(_bill_validate_vm.District) || String.IsNullOrEmpty(_bill_validate_vm.WardName) || !Regex.IsMatch(_bill_validate_vm.NumberPhone, regexPN))
			{
				_datHangThanhCong = false;
				_afterClick = string.Empty;
				_toastService.ShowError("Vui lòng nhập đầy đủ thông tin");
				return;
			}

			//cu
			var codeToday = DateTime.Now.ToString().Replace("/", "").Substring(0, 4) +
								DateTime.Now.Year.ToString().Substring(2);
			_lstBill = (await _httpClient.GetFromJsonAsync<List<Bill_VM>>("https://localhost:7141/api/Bill/get_alll_bill")).Where(c => c.BillCode.StartsWith(codeToday)).ToList();
			_bill_validate_vm.Id = Guid.NewGuid();
			_bill_validate_vm.TotalAmount = _tongTienAll;
			_bill_validate_vm.Type = 1;
			if (_lstPayM.FirstOrDefault(c => c.Id == _bill_validate_vm.PaymentMethodId).Name == "Thanh toán Momo")
			{
				_bill_validate_vm.Status = 1;
			}
			if (_lstPayM.FirstOrDefault(c => c.Id == _bill_validate_vm.PaymentMethodId).Name == "Thanh toán khi nhận hàng (COD)")
			{
				_bill_validate_vm.Status = 2;
			}
			if (_bill_validate_vm.Recipient == string.Empty) _bill_validate_vm.Recipient = _user_vm.Name;
			if (_lstBill.Count == 0) _bill_validate_vm.BillCode = codeToday + "1";
			else _bill_validate_vm.BillCode = codeToday + _lstBill.Max(c => int.Parse(c.BillCode.Substring(6)) + 1).ToString();
			// Ngày tạo
			_bill_validate_vm.CreateDate = DateTime.Now;
			// người tạo
			_bill_validate_vm.CreateBy = _bill_validate_vm.UserId;
			// thực hiện add bill
			var addBill = await _httpClient.PostAsJsonAsync("https://localhost:7141/api/Bill/Post-Bill", _bill_validate_vm);
			// nếu thành công thì tiếp tục
			if (addBill.StatusCode == System.Net.HttpStatusCode.OK)
			{
				ClosePopup("XacNhanTao");
				// nếu trong list addressShip rỗng và k phải vãng lai thì add để lần sau sử dụng
				if (_lst_adrS_User.Count == 0 && _bill_validate_vm.UserId != _userVangLai.Id)
				{
					AddressShip_VM addressShip_VM = new();
					addressShip_VM.Id = Guid.NewGuid();
					addressShip_VM.UserId = _bill_validate_vm.UserId;
					addressShip_VM.Recipient = _bill_validate_vm.Recipient;
					addressShip_VM.Province = _bill_validate_vm.Province;
					addressShip_VM.District = _bill_validate_vm.District;
					addressShip_VM.WardName = _bill_validate_vm.WardName;
					addressShip_VM.ToAddress = _bill_validate_vm.ToAddress;
					addressShip_VM.NumberPhone = _bill_validate_vm.NumberPhone;
					addressShip_VM.Status = 1;
					var a = await _httpClient.PostAsJsonAsync("https://localhost:7141/api/AddressShip/Post-Address", addressShip_VM);
				}
				// nếu có sử dụng voucher thì thực hiển trừ số lượng và thêm vào list đã sử dụng
				if (_bill_validate_vm.VoucherId != null)
				{
					var v_vm = await _httpClient.GetFromJsonAsync<Voucher_VM>($"https://localhost:7141/api/Voucher/GetVoucherByCode/{_voucherCode.ToUpper()}");
					v_vm.Quantity--;
					var updateVch = await _httpClient.PutAsJsonAsync("https://localhost:7141/api/Voucher/Put-Voucher", v_vm);
					if (_lstVchUser.Any(c => c.VoucherId == _bill_validate_vm.VoucherId))
					{
						// voucher đã tồn tại trong sổ => update trạng thái = 0
						var vchUser = _lstVchUser.FirstOrDefault(c => c.VoucherId == _bill_validate_vm.VoucherId);
						vchUser.Status = 0;
						var updateVchUser = await _httpClient.PutAsJsonAsync("https://localhost:7141/api/voucher-user/put-voucherUser", vchUser);
					}
					else
					{
						// voucher chưa tồn tại trong sổ => add vào sổ với trạng thái 0 (đã sử dụng)
						var vchUser = new VoucherUser_VM
						{
							Id = Guid.NewGuid(),
							UserId = _bill_validate_vm.UserId,
							VoucherId = _bill_validate_vm.VoucherId,
							Status = 0
						};
						var addVchUser = await _httpClient.PostAsJsonAsync("https://localhost:7141/api/voucher-user/post-voucherUser", vchUser);
					}
				}
				// nếu có check sử dụng điểm thì tạo bản ghi trừ điểm
				if (_checkDung == true && _diemCuaNguoiDung != 0)
				{
					HistoryConsumerPoint_VM h_vm = new()
					{
						Id = Guid.NewGuid(),
						ConsumerPointId = _bill_validate_vm.UserId,
						BillId = _bill_validate_vm.Id,
						Point = _tiengiamDungiem ?? 0,
						Status = 1,
					};
					var addhcp = await _httpClient.PostAsJsonAsync($"https://localhost:7141/api/HistoryConsumerPoint/add-HistoryConsumerPoint", h_vm);
					CustomerPoint_VM customerPoint_VM = new()
					{
						UserID = _bill_validate_vm.UserId,
						Point = (_diemCuaNguoiDung - _tiengiamDungiem).ToString() ?? "0",
						Status = 1
					};
					var addcp = await _httpClient.PutAsJsonAsync<CustomerPoint_VM>($"https://localhost:7141/api/CustomerPoint/putCustomerPoint", customerPoint_VM);
				}
				// thực hiện clear giỏ, trừ số lượng trong kho và add vào bill item
				foreach (var x in _lstCI)
				{
					var pi = _lstPrI_VM.FirstOrDefault(c => c.Id == x.ProductItemId);
					BillItem_VM billItem_VM = new BillItem_VM();
					billItem_VM.Id = Guid.NewGuid();
					billItem_VM.BillId = _bill_validate_vm.Id;
					billItem_VM.ProductItemsId = x.ProductItemId;
					billItem_VM.Quantity = x.Quantity;
					billItem_VM.Price = pi.PriceAfterReduction;
					billItem_VM.Status = 1;
					pi.AvaiableQuantity -= x.Quantity;
					var a = await _httpClient.PostAsJsonAsync("https://localhost:7141/api/BillItem/Post-BillItem", billItem_VM);
					var b = await _httpClient.PutAsJsonAsync("https://localhost:7141/api/productitem/update_productitem", pi);
					var c = await _httpClient.DeleteAsync($"https://localhost:7141/api/CartItems/delete-CartItems/{x.Id}");
					if (pi.CostPrice != pi.PriceAfterReduction)
					{
						var truPromotion = await _httpClient.PutAsJsonAsync($"https://localhost:7141/api/promotion/update_quantity_promotion/{x.ProductItemId}", x.ProductItemId);
					}
				}
				_ihttpcontextaccessor.HttpContext.Session.Remove("_lstCI_Vanglai");
				// thanh toán momo thì thực hiện điều hướng đến thanh toán momo
				if (_lstPayM.FirstOrDefault(c => c.Id == _bill_validate_vm.PaymentMethodId).Name == "Thanh toán Momo")
				{
					// Order
					_ord.OrderId = Guid.NewGuid().ToString();
					if (_user_vm.Name == null) _ord.FullName = "Không có thông tin khách hàng";
					else _ord.FullName = _user_vm.Name;
					_ord.OrderInfo = string.IsNullOrEmpty(_bill_validate_vm.Note)
					? $"Không có ghi chú. Mã hóa đơn: {_bill_validate_vm.BillCode}"
					: _bill_validate_vm.Note + $". Mã hóa đơn: {_bill_validate_vm.BillCode}";
					_ord.Amount = _tongTienAll;
					var reponse1 = await _httpClient.PostAsJsonAsync("https://localhost:7141/api/Momo/CreatePaymentAsync", _ord);
					var reponse2 = await reponse1.Content.ReadFromJsonAsync<MomoCreatePaymentResponseModel>();
					if (string.IsNullOrEmpty(reponse2.PayUrl))
					{
						_toastService.ShowError(reponse2.LocalMessage);
						_datHangThanhCong = false;
						_afterClick = string.Empty;
						return;
					}
					_navi.NavigateTo($"{reponse2.PayUrl}", true);
					return;
				}
				// thanh toán cod thì thông báo thành công và trả về trang chủ
				if (_lstPayM.FirstOrDefault(c => c.Id == _bill_validate_vm.PaymentMethodId).Name == "Thanh toán khi nhận hàng (COD)")
				{
					_toastService.ShowSuccess("Đơn hàng đã được tạo thành công");
					await Task.Delay(2000);
					if (string.IsNullOrEmpty(_iduser))
					{
						_navi.NavigateTo("/home", true);
						return;
					}
					else
					{
						_navi.NavigateTo($"/account/bill-history/bill-detail?billid={_bill_validate_vm.Id}", true);
						return;
					}
				}
			}

			// nếu thất bại thì trả thông báo thất bại
			_datHangThanhCong = false;
			_afterClick = string.Empty;
			_toastService.ShowError("Tạo đơn hàng thất bại");
		}

		public async Task BackToCart()
		{
			_navi.NavigateTo("/cart", true);
		}

		public async Task ChonTinhTP()
		{
			if (_bill_validate_vm.Province == _TinhTp) return;
			_lstQuanHuyen.Clear();
			_lstXaPhuong.Clear();
			_bill_validate_vm.District = string.Empty;
			_bill_validate_vm.WardName = string.Empty;
			_QuanHuyen = string.Empty;
			if (_bill_validate_vm.Province == string.Empty)
			{
				_TinhTp = string.Empty;
				return;
			}
			Province_VM chon = new Province_VM();
			chon = _lstTinhTp_Data.FirstOrDefault(c => c.Name == _bill_validate_vm.Province);
			_lstQuanHuyen = _lstQuanHuyen_Data.Where(c => c.ProvinceId == chon.Id).ToList();
			_TinhTp = _bill_validate_vm.Province;
		}

		public async Task ChonQuanHuyen()
		{
			if (_bill_validate_vm.District == _QuanHuyen) return;
			_lstXaPhuong.Clear();
			_bill_validate_vm.WardName = string.Empty;
			if (_bill_validate_vm.District == string.Empty)
			{
				_QuanHuyen = string.Empty;
				return;
			}
			District_VM chon = _lstQuanHuyen_Data.FirstOrDefault(c => c.Name == _bill_validate_vm.District);
			_lstXaPhuong = _lstXaPhuong_Data.Where(c => c.DistrictId == chon.Id).ToList();
			_QuanHuyen = _bill_validate_vm.District;
		}

		public async Task ChonDiaChiTuList(AddressShip_VM adrShip)
		{
			_bill_validate_vm.NumberPhone = adrShip.NumberPhone;
			_bill_validate_vm.Recipient = adrShip.Recipient;
			_bill_validate_vm.ToAddress = adrShip.ToAddress;
			// Tự gen
			_bill_validate_vm.Province = adrShip.Province;
			await ChonTinhTP();
			_bill_validate_vm.District = adrShip.District;
			await ChonQuanHuyen();
			_bill_validate_vm.WardName = adrShip.WardName;
			_toastService.ShowSuccess("Thay đổi địa chỉ thành công");
		}

		private async Task ApDungVoucher()
		{
			var v_vm = await _httpClient.GetFromJsonAsync<Voucher_VM>($"https://localhost:7141/api/Voucher/GetVoucherByCode/{_voucherCode.ToUpper()}");
			if (_lstVchUser.Any(c => c.VoucherId == v_vm.Id && c.Status == 0))
			{
				_toastService.ShowError("Mã giảm giá này bạn đã sử dụng rồi, vui lòng sử dụng mã giảm giá khác!");
				return;
			}
			if (v_vm.Quantity == 0 || v_vm.Status == 0)
			{
				_toastService.ShowError("Mã giảm giá không tồn tại, đã hết hạn hoặc hết lượt sử dụng");
				return;
			}
			if (_tongTienHang < v_vm.Discount_Conditions)
			{
				_toastService.ShowError($"Mã giảm giá {_voucherCode.ToUpper()} chỉ sử dụng cho đơn hàng có tổng trị từ {v_vm.Discount_Conditions?.ToString("#,##0")}đ");
				return;
			}
			if (v_vm != null)
			{
				_tongTienHang = 0;
				_tongTienAll = 0;
				foreach (var x in _lstCI)
				{
					_pi_s_vm = _lstPrI_show_VM.Where(c => c.Id == x.ProductItemId).FirstOrDefault();
					_tongTienHang += (x.Quantity * _pi_s_vm.PriceAfterReduction);
				}
				_tongTienAll = _tongTienHang + _bill_validate_vm.ShippingFee;
				_bill_validate_vm.VoucherId = v_vm.Id;
				_tiengiamVoucher = _tongTienAll * v_vm.Percent / 100;
				if (_tiengiamVoucher > v_vm.Maximum_Reduction)
				{
					_tiengiamVoucher = v_vm.Maximum_Reduction;
				}
				_tongTienAll -= _tiengiamVoucher;
				if (_checkDung == true)
				{
					_tiengiamDungiem = _tongTienAll / 10;
					if (_tiengiamDungiem >= _diemCuaNguoiDung) _tiengiamDungiem = _diemCuaNguoiDung;
					_tongTienAll -= _tiengiamDungiem;
				}
				_toastService.ShowSuccess("Áp dụng mã giảm giá thành công");
			}
		}

		private async Task XoaVoucher()
		{
			_bill_validate_vm.VoucherId = null;
			_voucherCode = string.Empty;
			_tongTienHang = 0;
			_tongTienAll = 0;
			_tiengiamVoucher = 0;
			foreach (var x in _lstCI)
			{
				_pi_s_vm = _lstPrI_show_VM.Where(c => c.Id == x.ProductItemId).FirstOrDefault();
				_tongTienHang += (x.Quantity * _pi_s_vm.PriceAfterReduction);
			}
			_tongTienAll = _tongTienHang + _bill_validate_vm.ShippingFee;
			if (_checkDung == true)
			{
				_tiengiamDungiem = _tongTienAll / 10;
				if (_tiengiamDungiem >= _diemCuaNguoiDung) _tiengiamDungiem = _diemCuaNguoiDung;
				_tongTienAll -= _tiengiamDungiem;
			}
			_toastService.ShowSuccess("Hủy áp dụng thành công");
		}
		private void SetDataPayMId(Guid id)
		{
			_bill_validate_vm.PaymentMethodId = id;
		}
		private void NavLogin()
		{
			_navi.NavigateTo("/login", true);
		}

		private async Task CheckedDungDiem(ChangeEventArgs e)
		{
			_checkDung = (bool)e.Value;
			if (_diemCuaNguoiDung == 0) return;
			if (_checkDung == true)
			{
				if (_bill_validate_vm.VoucherId != null)
				{
					var v_vm = await _httpClient.GetFromJsonAsync<Voucher_VM>($"https://localhost:7141/api/Voucher/ID?Id={_bill_validate_vm.VoucherId}");
					_tongTienHang = 0;
					_tongTienAll = 0;
					foreach (var x in _lstCI)
					{
						_pi_s_vm = _lstPrI_show_VM.Where(c => c.Id == x.ProductItemId).FirstOrDefault();
						_tongTienHang += (x.Quantity * _pi_s_vm.PriceAfterReduction);
					}
					_tongTienAll = _tongTienHang + _bill_validate_vm.ShippingFee;
					_bill_validate_vm.VoucherId = v_vm.Id;
					_tiengiamVoucher = _tongTienAll * v_vm.Percent / 100;
					if (_tiengiamVoucher > v_vm.Maximum_Reduction)
					{
						_tiengiamVoucher = v_vm.Maximum_Reduction;
					}
					_tongTienAll -= _tiengiamVoucher;
				}
				if (_bill_validate_vm.VoucherId == null)
				{
					_voucherCode = string.Empty;
					_tongTienHang = 0;
					_tongTienAll = 0;
					_tiengiamVoucher = 0;
					foreach (var x in _lstCI)
					{
						_pi_s_vm = _lstPrI_show_VM.Where(c => c.Id == x.ProductItemId).FirstOrDefault();
						_tongTienHang += (x.Quantity * _pi_s_vm.PriceAfterReduction);
					}
					_tongTienAll = _tongTienHang + _bill_validate_vm.ShippingFee;
				}
				_tiengiamDungiem = _tongTienAll / 10;
				if (_tiengiamDungiem >= _diemCuaNguoiDung) _tiengiamDungiem = _diemCuaNguoiDung;
				_tongTienAll -= _tiengiamDungiem;
			}
			else
			{
				if (_bill_validate_vm.VoucherId != null)
				{
					var v_vm = await _httpClient.GetFromJsonAsync<Voucher_VM>($"https://localhost:7141/api/Voucher/ID?Id={_bill_validate_vm.VoucherId}");
					_tongTienHang = 0;
					_tongTienAll = 0;
					foreach (var x in _lstCI)
					{
						_pi_s_vm = _lstPrI_show_VM.Where(c => c.Id == x.ProductItemId).FirstOrDefault();
						_tongTienHang += (x.Quantity * _pi_s_vm.PriceAfterReduction);
					}
					_tongTienAll = _tongTienHang + _bill_validate_vm.ShippingFee;
					_tiengiamVoucher = _tongTienAll * v_vm.Percent / 100;
					if (_tiengiamVoucher > v_vm.Maximum_Reduction)
					{
						_tiengiamVoucher = v_vm.Maximum_Reduction;
					}
					_tongTienAll -= _tiengiamVoucher;
				}
				if (_bill_validate_vm.VoucherId == null)
				{
					_voucherCode = string.Empty;
					_tongTienHang = 0;
					_tongTienAll = 0;
					_tiengiamVoucher = 0;
					foreach (var x in _lstCI)
					{
						_pi_s_vm = _lstPrI_show_VM.Where(c => c.Id == x.ProductItemId).FirstOrDefault();
						_tongTienHang += (x.Quantity * _pi_s_vm.PriceAfterReduction);
					}
					_tongTienAll = _tongTienHang + _bill_validate_vm.ShippingFee;
				}
			}
		}

		private void SetModalState(bool isOpen, string modalType)
		{
			switch (modalType)
			{
				case "XacNhanTao":
					isModalXacNhan = isOpen;
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

		private void Mo_XacNhan()
		{
			OpenPopup("XacNhanTao");
		}                                                 
	}
}