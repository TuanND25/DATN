using DATN_Client.SessionService;
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
		private HttpClient _httpClient = new HttpClient();
		[Inject] private NavigationManager _navi { get; set; }
		[Inject] public IHttpContextAccessor _ihttpcontextaccessor { get; set; }
		[Inject] Blazored.Toast.Services.IToastService _toastService { get; set; } // Khai báo khi cần gọi ở code-behind
																				   //private List<User_VM> _lstUser = new List<User_VM>();
		private List<CartItems_VM> _lstCI = new List<CartItems_VM>();
		private List<Image_Join_ProductItem> _lstImg_PI = new List<Image_Join_ProductItem>();
		private List<Image_Join_ProductItem> _lstImg_PI_tam = new List<Image_Join_ProductItem>();
		private List<ProductItem_Show_VM> _lstPrI_show_VM = new List<ProductItem_Show_VM>();
		private List<PaymentMethod_VM> _lstPayM = new List<PaymentMethod_VM>();
		public static Bill_VM _bill_vm;
		private User_VM? _user_vm = new User_VM();
		private ProductItem_Show_VM _pi_s_vm = new ProductItem_Show_VM();
		private OrderInfoModel _ord = new OrderInfoModel();
		private PaymentMethod_VM _payM = new PaymentMethod_VM();
		public int? _tongTienHang { get; set; } = 0;
		public int? _tongTienAll { get; set; } = 0;
		private List<Province_VM> _lstTinhTp = new List<Province_VM>();
		private List<District_VM> _lstQuanHuyen = new List<District_VM>();
		private List<Ward_VM> _lstXaPhuong = new List<Ward_VM>();
		private List<Province_VM> _lstTinhTp_Data = new List<Province_VM>();
		private List<District_VM> _lstQuanHuyen_Data = new List<District_VM>();
		private List<Ward_VM> _lstXaPhuong_Data = new List<Ward_VM>();
		private List<Bill_VM> _lstBill = new List<Bill_VM>();
		private List<ProductItem_VM> _lstPrI_VM = new List<ProductItem_VM>();
		private List<HistoryConsumerPoint_VM> _lstHCP_VM = new List<HistoryConsumerPoint_VM>();
		private List<AddressShip_VM> _lst_adrS_User = new List<AddressShip_VM>();
		private List<VoucherUser_VM> _lstVchUser = new List<VoucherUser_VM>();
		private AddressShip_VM? _adrS_User = new AddressShip_VM();
		private Voucher_VM? _v_vm = new Voucher_VM();
		public string _TinhTp { get; set; }
		public string _QuanHuyen { get; set; }
		public string? _iduser { get; set; }
		public bool _datHangThanhCong { get; set; } = false;
		public string _voucherCode { get; set; } = string.Empty;
		public int? _tiengiam { get; set; } = 0;
		public string _afterClick { get; set; } = string.Empty;
		protected override async Task OnInitializedAsync()
		{
			_iduser = _ihttpcontextaccessor.HttpContext.Session.GetString("UserId");
			if (SessionServices.GetLstFromSession_LstCI(_ihttpcontextaccessor.HttpContext.Session, "_lstCI_Vanglai").Count == 0 && _iduser == null) _navi.NavigateTo("https://localhost:7075/cart", true);
			//var token = _ihttpcontextaccessor.HttpContext.Session.GetString("Token"); // Gọi token
			//_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); // Xác thực
			_bill_vm = new Bill_VM();
			_lstPrI_show_VM = await _httpClient.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");
			if (_iduser == null)
			{
				_bill_vm.UserId = Guid.Parse("8870699c-8f34-4bcd-b07c-08c003c2a732");
				_lstCI = SessionServices.GetLstFromSession_LstCI(_ihttpcontextaccessor.HttpContext.Session, "_lstCI_Vanglai");
			}
			else
			{
				_bill_vm.UserId = Guid.Parse(_iduser);
				//_lstUser = await _httpClient.GetFromJsonAsync<List<User_VM>>("https://localhost:7141/api/user/get-user");
				_user_vm = await _httpClient.GetFromJsonAsync<User_VM>($"https://localhost:7141/api/user/get_user_by_id/{_iduser}");
				_lstCI = await _httpClient.GetFromJsonAsync<List<CartItems_VM>>($"https://localhost:7141/api/CartItems/{_bill_vm.UserId}");
				_lst_adrS_User = await _httpClient.GetFromJsonAsync<List<AddressShip_VM>>($"https://localhost:7141/api/AddressShip/get_address_by_UserID/{_bill_vm.UserId}"); // list địa chỉ của user
				_adrS_User = _lst_adrS_User.FirstOrDefault(c => c.Status == 1);
			}
			_lstImg_PI = (await _httpClient.GetFromJsonAsync<List<Image_Join_ProductItem>>("https://localhost:7141/api/Image/GetAllImage_PrductItem")).OrderBy(c => c.STT).ToList();
			_lstTinhTp_Data = await _httpClient.GetFromJsonAsync<List<Province_VM>>("https://api.npoint.io/ac646cb54b295b9555be"); // api tỉnh tp
			_lstTinhTp = _lstTinhTp_Data;
			_lstQuanHuyen_Data = await _httpClient.GetFromJsonAsync<List<District_VM>>("https://api.npoint.io/34608ea16bebc5cffd42");
			_lstXaPhuong_Data = await _httpClient.GetFromJsonAsync<List<Ward_VM>>("https://api.npoint.io/dd278dc276e65c68cdf5");
			_lstPayM = (await _httpClient.GetFromJsonAsync<List<PaymentMethod_VM>>("https://localhost:7141/api/paymentMethod/get_all_paymentMethod")).Where(c => c.Status == 1).ToList();
			_lstPrI_VM = await _httpClient.GetFromJsonAsync<List<ProductItem_VM>>("https://localhost:7141/api/productitem/get_all_productitem");
			_lstHCP_VM = await _httpClient.GetFromJsonAsync<List<HistoryConsumerPoint_VM>>("https://localhost:7141/api/HistoryConsumerPoint/get-HistoryConsumerPoint");
			_lstVchUser = await _httpClient.GetFromJsonAsync<List<VoucherUser_VM>>($"https://localhost:7141/api/voucher-user/get-voucherUser-by-userid/{_bill_vm.UserId}"); // list voucher của user
			if (_adrS_User != null)
			{
				_bill_vm.NumberPhone = _adrS_User.NumberPhone;
				_bill_vm.Recipient = _adrS_User.Recipient;
				_bill_vm.ToAddress = _adrS_User.ToAddress;
				// Tự gen
				_bill_vm.Province = _adrS_User.Province;
				await ChonTinhTP();
				_bill_vm.District = _adrS_User.District;
				await ChonQuanHuyen();
				_bill_vm.WardName = _adrS_User.WardName;
			}
			if (_adrS_User == null)
			{
				_bill_vm.NumberPhone = string.Empty;
				_bill_vm.Recipient = string.Empty;
				_bill_vm.ToAddress = string.Empty;
				_bill_vm.Province = string.Empty;
				_bill_vm.District = string.Empty;
				_bill_vm.WardName = string.Empty;
			}
			_payM = _lstPayM.FirstOrDefault(c => c.Name == "Thanh toán khi nhận hàng (COD)");
			_bill_vm.PaymentMethodId = _payM.Id;
			_bill_vm.Note = ShowCart._note;
			_bill_vm.ShippingFee = 0;

			foreach (var x in _lstCI)
			{
				_pi_s_vm = _lstPrI_show_VM.Where(c => c.Id == x.ProductItemId).FirstOrDefault();
				_tongTienHang += (x.Quantity * _pi_s_vm.PriceAfterReduction);
			}
			_tongTienAll = _tongTienHang + _bill_vm.ShippingFee;
		}

		//public Guid Id { get; set; }
		//public Guid UserId { get; set; }
		//public Guid? HistoryConsumerPointID { get; set; }
		//public Guid PaymentMethodId { get; set; }
		//public Guid? VoucherId { get; set; }
		//public string BillCode { get; set; }
		//public int? TotalAmount { get; set; }
		//public int? ReducedAmount { get; set; }
		//public int? Cash { get; set; }  // tiền mặt
		//public int? CustomerPayment { get; set; } // tiền khách đưa
		//public int? FinalAmount { get; set; } // tiền khách đưa
		//public DateTime? CreateDate { get; set; }
		//public DateTime? ConfirmationDate { get; set; }
		//public DateTime? CompletionDate { get; set; }
		//public int Type { get; set; }
		//public string? Note { get; set; }
		//public string Recipient { get; set; } // Người nhận
		//public string District { get; set; } // Quận/Huyện
		//public string Province { get; set; } // Tỉnh/ TP
		//public string WardName { get; set; } // Phường/ Xã
		//public string ToAddress { get; set; } // Địa chỉ chi tiết
		//public string NumberPhone { get; set; } // SDT
		//public int Status { get; set; }
		//public int? ShippingFee { get; set; }
		public async Task Btn_DatHang()
		{
			if (_lstVchUser.Any(c => c.VoucherId == _bill_vm.VoucherId && c.Status == 0))
			{
				_toastService.ShowError("Bạn đã sử dụng mã giảm giá này rồi, vui lòng chọn mã giảm giá khác!");
				return;
			}
			var regexPN = @"^0\d{9,10}$"; // regex số đt
			if (String.IsNullOrEmpty(_bill_vm.Recipient) || String.IsNullOrEmpty(_bill_vm.NumberPhone) || String.IsNullOrEmpty(_bill_vm.ToAddress) || String.IsNullOrEmpty(_bill_vm.Province) || String.IsNullOrEmpty(_bill_vm.District) || String.IsNullOrEmpty(_bill_vm.WardName) || !Regex.IsMatch(_bill_vm.NumberPhone, regexPN)) return;
			_datHangThanhCong = true;
			_afterClick = "afterClick";
			var codeToday = DateTime.Now.ToString().Replace("/", "").Substring(0, 4) +
								DateTime.Now.Year.ToString().Substring(2);
			_lstBill = (await _httpClient.GetFromJsonAsync<List<Bill_VM>>("https://localhost:7141/api/Bill/get_alll_bill")).Where(c => c.BillCode.StartsWith(codeToday)).ToList();
			_bill_vm.Id = Guid.NewGuid();
			_bill_vm.TotalAmount = _tongTienAll;
			_bill_vm.Type = 1;
			if (_lstPayM.FirstOrDefault(c => c.Id == _bill_vm.PaymentMethodId).Name == "Thanh toán Momo")
			{
				_bill_vm.Status = 2;
			}
			if (_lstPayM.FirstOrDefault(c => c.Id == _bill_vm.PaymentMethodId).Name == "Thanh toán khi nhận hàng (COD)")
			{
				_bill_vm.Status = 1;
			}
			if (_bill_vm.Note == string.Empty) _bill_vm.Note = "Không có ghi chú";
			//_bill_vm.HistoryConsumerPointID = _lstHCP_VM.FirstOrDefault().Id;
			if (_bill_vm.Recipient == string.Empty) _bill_vm.Recipient = _user_vm.Name;
			if (_lstBill.Count == 0) _bill_vm.BillCode = codeToday + "1";
			else _bill_vm.BillCode = codeToday + _lstBill.Max(c => int.Parse(c.BillCode.Substring(6)) + 1).ToString();
			// Ngày tạo
			_bill_vm.CreateDate = DateTime.Now;
			var addBill = await _httpClient.PostAsJsonAsync("https://localhost:7141/api/Bill/Post-Bill", _bill_vm);
			if (addBill.StatusCode == System.Net.HttpStatusCode.OK)
			{
				if (_bill_vm.VoucherId != null)
				{// cần trừ số lượng voucher
					if (_lstVchUser.Any(c => c.VoucherId == _bill_vm.VoucherId))
					{
						// voucher đã tồn tại trong sổ => update trạng thái = 0
						var vchUser = _lstVchUser.FirstOrDefault(c => c.VoucherId == _bill_vm.VoucherId);
						vchUser.Status = 0;
						var updateVchUser = await _httpClient.PutAsJsonAsync("https://localhost:7141/api/voucher-user/put-voucherUser", vchUser);
					}
					else
					{
						// voucher chưa tồn tại trong sổ => add vào sổ với trạng thái 0 (đã sử dụng)
						var vchUser = new VoucherUser_VM
						{
							Id = Guid.NewGuid(),
							UserId = _bill_vm.UserId,
							VoucherId = _bill_vm.VoucherId,
							Status = 0
						};
						var addVchUser = await _httpClient.PostAsJsonAsync("https://localhost:7141/api/voucher-user/post-voucherUser", vchUser);
					}
				}
				foreach (var x in _lstCI)
				{
					var pi = _lstPrI_VM.FirstOrDefault(c => c.Id == x.ProductItemId);
					BillItem_VM billItem_VM = new BillItem_VM();
					billItem_VM.Id = Guid.NewGuid();
					billItem_VM.BillId = _bill_vm.Id;
					billItem_VM.ProductItemsId = x.ProductItemId;
					billItem_VM.Quantity = x.Quantity;
					billItem_VM.Price = pi.PriceAfterReduction;
					billItem_VM.Status = 1;
					pi.AvaiableQuantity -= x.Quantity;
					var a = await _httpClient.PostAsJsonAsync("https://localhost:7141/api/BillItem/Post-BillItem", billItem_VM);
					var b = await _httpClient.PutAsJsonAsync("https://localhost:7141/api/productitem/update_productitem", pi);
					var c = await _httpClient.DeleteAsync($"https://localhost:7141/api/CartItems/delete-CartItems/{x.Id}");
				}
				_ihttpcontextaccessor.HttpContext.Session.Remove("_lstCI_Vanglai");
				if (_lstPayM.FirstOrDefault(c => c.Id == _bill_vm.PaymentMethodId).Name == "Thanh toán Momo")
				{
					// Order
					_ord.OrderId = Guid.NewGuid().ToString();
					if (_user_vm.Name == null) _ord.FullName = _bill_vm.Recipient;
					else _ord.FullName = _user_vm.Name;
					_ord.OrderInfo = _bill_vm.Note;
					_ord.Amount = _tongTienAll;
					var reponse1 = await _httpClient.PostAsJsonAsync("https://localhost:7141/api/Momo/CreatePaymentAsync", _ord);
					var reponse2 = await reponse1.Content.ReadFromJsonAsync<MomoCreatePaymentResponseModel>();
					_navi.NavigateTo($"{reponse2.PayUrl}", true);
					return;
				}
				if (_lstPayM.FirstOrDefault(c => c.Id == _bill_vm.PaymentMethodId).Name == "Thanh toán khi nhận hàng (COD)")
				{										
					_toastService.ShowSuccess("Đơn hàng đã được tạo thành công, để theo dõi đơn hàng hãy vào mục Lịch sử đơn hàng");
					_toastService.ShowSuccess("Sau 3 giây bạn sẽ được chuyển hướng đến danh sách hóa đơn cá nhân");
					await Task.Delay(3000);
					_navi.NavigateTo("/all-product", true);
					return;
				}
			}
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
			if (_bill_vm.Province == _TinhTp) return;
			_lstQuanHuyen.Clear();
			_lstXaPhuong.Clear();
			_bill_vm.District = string.Empty;
			_bill_vm.WardName = string.Empty;
			if (_bill_vm.Province == string.Empty)
			{
				_TinhTp = string.Empty;
				return;
			}
			Province_VM chon = new Province_VM();
			chon = _lstTinhTp_Data.FirstOrDefault(c => c.Name == _bill_vm.Province);
			_lstQuanHuyen = _lstQuanHuyen_Data.Where(c => c.ProvinceId == chon.Id).ToList();
			_TinhTp = _bill_vm.Province;
		}

		public async Task ChonQuanHuyen()
		{
			if (_bill_vm.District == _QuanHuyen) return;
			_lstXaPhuong.Clear();
			_bill_vm.WardName = string.Empty;
			if (_bill_vm.District == string.Empty)
			{
				_QuanHuyen = string.Empty;
				return;
			}
			District_VM chon = _lstQuanHuyen_Data.FirstOrDefault(c => c.Name == _bill_vm.District);
			_lstXaPhuong = _lstXaPhuong_Data.Where(c => c.DistrictId == chon.Id).ToList();
			_QuanHuyen = _bill_vm.District;
		}

		public async Task ChonDiaChiTuList(AddressShip_VM adrShip)
		{
			_bill_vm.Recipient = adrShip.Recipient;
			_bill_vm.NumberPhone = adrShip.NumberPhone;
			_bill_vm.ToAddress = adrShip.ToAddress;
			_bill_vm.Province = adrShip.Province;
			await ChonTinhTP();
			_bill_vm.District = adrShip.District;
			await ChonQuanHuyen();
			_bill_vm.WardName = adrShip.WardName;
			_toastService.ShowSuccess("Thay đổi địa chỉ thành công");
		}

		private async Task ApDungVoucher()
		{
			_v_vm = await _httpClient.GetFromJsonAsync<Voucher_VM>($"https://localhost:7141/api/Voucher/GetVoucherByCode/{_voucherCode.ToUpper()}");
			if (_lstVchUser.Any(c => c.VoucherId == _v_vm.Id && c.Status == 0))
			{
				_toastService.ShowError("Mã giảm giá này bạn đã sử dụng rồi, vui lòng sử dụng mã giảm giá khác!");
				return;
			}
			if (_v_vm.Quantity == 0)
			{
				_toastService.ShowError("Mã giảm giá không tồn tại, đã hết hạn hoặc hết lượt sử dụng");
				return;
			}
			if (_tongTienHang < _v_vm.Discount_Conditions)
			{
				_toastService.ShowError($"Mã giảm giá {_voucherCode.ToUpper()} chỉ sử dụng cho đơn hàng có tổng trị từ {_v_vm.Discount_Conditions?.ToString("#,##0")}đ");
				return;
			}
			if (_v_vm != null)
			{
				_tongTienHang = 0;
				_tongTienAll = 0;
				foreach (var x in _lstCI)
				{
					_pi_s_vm = _lstPrI_show_VM.Where(c => c.Id == x.ProductItemId).FirstOrDefault();
					_tongTienHang += (x.Quantity * _pi_s_vm.PriceAfterReduction);
				}
				_tongTienAll = _tongTienHang + _bill_vm.ShippingFee;
				_bill_vm.VoucherId = _v_vm.Id;
				_tiengiam = _tongTienAll * _v_vm.Percent / 100;
				if (_tiengiam > _v_vm.Maximum_Reduction)
				{
					_tiengiam = _v_vm.Maximum_Reduction;
				}
				_tongTienAll -= _tiengiam;
				_toastService.ShowSuccess("Áp dụng mã giảm giá thành công");
			}
		}

		private async Task XoaVoucher()
		{
			_voucherCode = string.Empty;
			_tongTienHang = 0;
			_tongTienAll = 0;
			_tiengiam = 0;
			foreach (var x in _lstCI)
			{
				_pi_s_vm = _lstPrI_show_VM.Where(c => c.Id == x.ProductItemId).FirstOrDefault();
				_tongTienHang += (x.Quantity * _pi_s_vm.PriceAfterReduction);
			}
			_tongTienAll = _tongTienHang + _bill_vm.ShippingFee;
			_toastService.ShowSuccess("Hủy áp dụng thành công, mã giảm giá đã được hoàn lại vào ví Vocher");
		}
	}
}