using DATN_Client.SessionService;
using DATN_Shared.ViewModel;
using DATN_Shared.ViewModel.DiaChi;
using DATN_Shared.ViewModel.Momo;
using DATN_Shared.ViewModel.Momo.Order;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;

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
		private ProductItem_VM _pi_vm = new ProductItem_VM();
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
		private AddressShip_VM? _adrS_User = new AddressShip_VM();
		private Voucher_VM? _v_vm = new Voucher_VM();
		public string _TinhTp { get; set; }
		public string _QuanHuyen { get; set; }
		public string? _iduser { get; set; }
		public bool _datHangThanhCong { get; set; } = false;
		public string _voucherCode { get; set; } = string.Empty;
		public int? _tiengiam { get; set; } = 0;
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
				_lst_adrS_User = await _httpClient.GetFromJsonAsync<List<AddressShip_VM>>($"https://localhost:7141/api/AddressShip/get_address_by_UserID/{_user_vm.Id}");
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
			_bill_vm.ShippingFee = 30000;

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
			if (_lstPayM.FirstOrDefault(c => c.Id == _bill_vm.PaymentMethodId).Name == "Thanh toán Momo")
			{
				//var abc = _bill_vm;
				// Bill
				_bill_vm.Id = Guid.NewGuid();
				_bill_vm.TotalAmount = _tongTienAll;
				_bill_vm.Type = 1;
				_bill_vm.Status = 1;
				if (_bill_vm.Note == string.Empty) _bill_vm.Note = "Không có ghi chú";
				//_bill_vm.HistoryConsumerPointID = _lstHCP_VM.FirstOrDefault().Id;
				if (_bill_vm.Recipient == string.Empty) _bill_vm.Recipient = _user_vm.Name;
				// Order
				_ord.OrderId = Guid.NewGuid().ToString();
				if (_user_vm.Name == null) _ord.FullName = _bill_vm.Recipient;
				else _ord.FullName = _user_vm.Name;
				_ord.OrderInfo = _bill_vm.Note;
				_ord.Amount = _tongTienAll;
				var reponse = await _httpClient.PostAsJsonAsync("https://localhost:7141/api/Momo/CreatePaymentAsync", _ord);
				var reponse2 = await reponse.Content.ReadFromJsonAsync<MomoCreatePaymentResponseModel>();
				_navi.NavigateTo($"{reponse2.PayUrl}", true);
			}
			if (_lstPayM.FirstOrDefault(c => c.Id == _bill_vm.PaymentMethodId).Name == "Thanh toán khi nhận hàng (COD)")
			{
				var codeToday = DateTime.Now.ToString().Replace("/", "").Substring(0, 4) +
								DateTime.Now.Year.ToString().Substring(2);
				_lstBill = (await _httpClient.GetFromJsonAsync<List<Bill_VM>>("https://localhost:7141/api/Bill/get_alll_bill")).Where(c => c.BillCode.StartsWith(codeToday)).ToList();
				_bill_vm.Id = Guid.NewGuid();
				_bill_vm.TotalAmount = _tongTienAll;
				_bill_vm.Type = 1;
				_bill_vm.Status = 1;
				if (_bill_vm.Note == string.Empty) _bill_vm.Note = "Không có ghi chú";
				//_bill_vm.HistoryConsumerPointID = _lstHCP_VM.FirstOrDefault().Id;
				if (_bill_vm.Recipient == string.Empty) _bill_vm.Recipient = _user_vm.Name;
				// Tạo mã bill dạng: B + ngày tháng năm tạo bill + số lớn nhất +1
				if (_lstBill.Count == 0) _bill_vm.BillCode = codeToday + "1";
				else _bill_vm.BillCode = codeToday + _lstBill.Max(c => int.Parse(c.BillCode.Substring(6)) + 1).ToString();
				// Ngày tạo
				_bill_vm.CreateDate = DateTime.Now;
				try
				{
					var addBill = await _httpClient.PostAsJsonAsync("https://localhost:7141/api/Bill/Post-Bill", _bill_vm);
					if (addBill.IsSuccessStatusCode)
					{
						foreach (var x in _lstCI)
						{
							_pi_vm = _lstPrI_VM.Where(c => c.Id == x.ProductItemId).FirstOrDefault();
							BillItem_VM billItem_VM = new BillItem_VM();
							billItem_VM.Id = Guid.NewGuid();
							billItem_VM.BillId = _bill_vm.Id;
							billItem_VM.ProductItemsId = x.ProductItemId;
							billItem_VM.Quantity = x.Quantity;
							billItem_VM.Price = _pi_vm.PriceAfterReduction;
							billItem_VM.Status = 1;
							_pi_vm.AvaiableQuantity -= x.Quantity;
							var a = await _httpClient.PostAsJsonAsync("https://localhost:7141/api/BillItem/Post-BillItem", billItem_VM);
							var b = await _httpClient.PutAsJsonAsync("https://localhost:7141/api/productitem/update_productitem", _pi_vm);
							var c = await _httpClient.DeleteAsync($"https://localhost:7141/api/CartItems/delete-CartItems/{x.Id}");
						}
						_datHangThanhCong = true;
						_ihttpcontextaccessor.HttpContext.Session.Remove("_lstCI_Vanglai");
						_toastService.ShowSuccess("Đơn hàng đã được tạo thành công, để theo dõi đơn hàng hãy vào mục Lịch sử đơn hàng");
						_toastService.ShowSuccess("Sau 3 giây bạn sẽ được chuyển hướng đến danh sách hóa đơn cá nhân");
						await Task.Delay(3000);
						_navi.NavigateTo("/all-product", true);
						return;
					}
				}
				catch (Exception)
				{

					throw;
				}
				_toastService.ShowError("Tạo đơn hàng thất bại");
			}
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
			_tongTienHang = 0;
			_tongTienAll = 0;
			foreach (var x in _lstCI)
			{
				_pi_s_vm = _lstPrI_show_VM.Where(c => c.Id == x.ProductItemId).FirstOrDefault();
				_tongTienHang += (x.Quantity * _pi_s_vm.PriceAfterReduction);
			}
			_tongTienAll = _tongTienHang + _bill_vm.ShippingFee;

			_v_vm = await _httpClient.GetFromJsonAsync<Voucher_VM>($"https://localhost:7141/api/Voucher/GetVoucherByCode/{_voucherCode.ToUpper()}");
			if (_v_vm == null || _v_vm.Quantity == 0)
			{
				_toastService.ShowError("Mã giảm giá không tồn tại, đã hết hạn hoặc hết lượt sử dụng");
				return;
			}
			if (_tongTienHang < _v_vm.Discount_Conditions)
			{
				_toastService.ShowError($"Mã giảm giá {_voucherCode.ToUpper()} chỉ sử dụng cho đơn hàng có tổng trị từ {_v_vm.Discount_Conditions?.ToString("#,##0")}");
				return;
			}
			if (_v_vm != null)
			{
				_tiengiam = _tongTienAll * _v_vm.Percent / 100;
				if (_tiengiam > _v_vm.Maximum_Reduction)
				{
					_tiengiam = _v_vm.Maximum_Reduction;
				}
				_tongTienAll -= _tiengiam;
			}
		}
	}
}