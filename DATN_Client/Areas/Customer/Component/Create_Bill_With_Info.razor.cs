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
		private List<User_VM> _lstUser = new List<User_VM>();
		private List<CartItems_VM> _lstCI = new List<CartItems_VM>();
		private List<Image_VM> _lstImg = new List<Image_VM>();
		private List<ProductItem_Show_VM> _lstPrI_show_VM = new List<ProductItem_Show_VM>();
		private List<PaymentMethod_VM> _lstPayM = new List<PaymentMethod_VM>();
		public static Bill_VM _bill_vm;
		private User_VM _user_vm = new User_VM();
		private ProductItem_Show_VM _pi_s_vm = new ProductItem_Show_VM();
		private OrderInfoModel _ord = new OrderInfoModel();
		private PaymentMethod_VM _payM = new PaymentMethod_VM();
		private ProductItem_VM _pi_vm = new ProductItem_VM();
		public string _sdt { get; set; }
		public int? _tongTienHang { get; set; } = 0;
		private List<Province_VM> _lstTinhTp = new List<Province_VM>();
		private List<District_VM> _lstQuanHuyen = new List<District_VM>();
		private List<Ward_VM> _lstXaPhuong = new List<Ward_VM>();
		private List<Province_VM> _lstTinhTp_Data = new List<Province_VM>();
		private List<District_VM> _lstQuanHuyen_Data = new List<District_VM>();
		private List<Ward_VM> _lstXaPhuong_Data = new List<Ward_VM>();
		private List<Bill_VM> _lstBill = new List<Bill_VM>();
		private List<ProductItem_VM> _lstPrI_VM = new List<ProductItem_VM>();
		private List<HistoryConsumerPoint_VM> _lstHCP_VM = new List<HistoryConsumerPoint_VM>();
		public string _TinhTp { get; set; }
		public string _QuanHuyen { get; set; }
		public string _XaPhuong { get; set; }
		public string _ptttbill1 { get; set; }
		public string _ptttbill2 { get; set; }
		protected override async Task OnInitializedAsync()
		{
			var token = _ihttpcontextaccessor.HttpContext.Session.GetString("Token"); // Gọi token
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); // Xác thực
			_bill_vm = new Bill_VM();
			_bill_vm.UserId = Guid.Parse(_ihttpcontextaccessor.HttpContext.Session.GetString("UserId"));
			_lstUser = await _httpClient.GetFromJsonAsync<List<User_VM>>("https://localhost:7141/api/user/get-user");
			_user_vm = _lstUser.Where(c => c.Id == _bill_vm.UserId).FirstOrDefault();
			_lstImg = (await _httpClient.GetFromJsonAsync<List<Image_VM>>("https://localhost:7141/api/Image")).OrderBy(c => c.STT).ToList();
			_lstCI = await _httpClient.GetFromJsonAsync<List<CartItems_VM>>($"https://localhost:7141/api/CartItems/{_bill_vm.UserId}");
			_lstPrI_show_VM = await _httpClient.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");
			_lstTinhTp_Data = await _httpClient.GetFromJsonAsync<List<Province_VM>>("https://api.npoint.io/ac646cb54b295b9555be"); // api tỉnh tp
			_lstTinhTp = _lstTinhTp_Data;
			_lstQuanHuyen_Data = await _httpClient.GetFromJsonAsync<List<District_VM>>("https://api.npoint.io/34608ea16bebc5cffd42");
			_lstXaPhuong_Data = await _httpClient.GetFromJsonAsync<List<Ward_VM>>("https://api.npoint.io/dd278dc276e65c68cdf5");
			_lstPayM = (await _httpClient.GetFromJsonAsync<List<PaymentMethod_VM>>("https://localhost:7141/api/paymentMethod/get_all_paymentMethod")).Where(c => c.Status == 1).ToList();
			_lstBill = await _httpClient.GetFromJsonAsync<List<Bill_VM>>("https://localhost:7141/api/Bill/get_alll_bill");
			_lstPrI_VM = await _httpClient.GetFromJsonAsync<List<ProductItem_VM>>("https://localhost:7141/api/productitem/get_all_productitem");
			_lstHCP_VM = await _httpClient.GetFromJsonAsync<List<HistoryConsumerPoint_VM>>("https://localhost:7141/api/HistoryConsumerPoint/get-HistoryConsumerPoint");
			_payM = _lstPayM.FirstOrDefault(c => c.Name == "Thanh toán khi nhận hàng (COD)");
			_bill_vm.NumberPhone = _user_vm.PhoneNumber;
			_bill_vm.PaymentMethodId = _payM.Id;
			_bill_vm.Note = ShowCart._note;
			_bill_vm.Recipient = string.Empty;
			_bill_vm.ToAddress = string.Empty;
			// Tự gen
			_bill_vm.Province = "Hà Nội";
			await ChonTinhTP();
			_bill_vm.District = "Huyện Hoài Đức";
			await ChonQuanHuyen();
			_bill_vm.WardName = "Xã Đức Giang";
			await ChonXaPhuong();
			//_bill_vm.Province = string.Empty;
			//_bill_vm.District = string.Empty;
			//_bill_vm.WardName = string.Empty;
			//_TinhTp = _bill_vm.Province;
			//_QuanHuyen = _bill_vm.District;
			//_XaPhuong = _bill_vm.WardName;

			foreach (var x in _lstCI)
			{
				_pi_s_vm = _lstPrI_show_VM.Where(c => c.Id == x.ProductItemId).FirstOrDefault();
				_tongTienHang += (x.Quantity * _pi_s_vm.CostPrice);
			}
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
				_bill_vm.TotalAmount = _tongTienHang;
				_bill_vm.Type = 1;
				_bill_vm.Status = 1;
				if (_bill_vm.Note == string.Empty) _bill_vm.Note = "Không có ghi chú";
				_bill_vm.HistoryConsumerPointID = _lstHCP_VM.FirstOrDefault().Id;
				if (_bill_vm.Recipient == string.Empty) _bill_vm.Recipient = _user_vm.UserName;
				// Order
				_ord.OrderId = Guid.NewGuid().ToString();
				_ord.FullName = _user_vm.UserName;
				_ord.OrderInfo = _bill_vm.Note;
				_ord.Amount = _tongTienHang;
				var reponse = await _httpClient.PostAsJsonAsync("https://localhost:7141/api/Momo/CreatePaymentAsync", _ord);
				var reponse2 = await reponse.Content.ReadFromJsonAsync<MomoCreatePaymentResponseModel>();
				_navi.NavigateTo($"{reponse2.PayUrl}", true);
			}
			if (_lstPayM.FirstOrDefault(c => c.Id == _bill_vm.PaymentMethodId).Name == "Thanh toán khi nhận hàng (COD)")
			{
				_bill_vm.Id = Guid.NewGuid();
				_bill_vm.TotalAmount = _tongTienHang;
				_bill_vm.Type = 1;
				_bill_vm.Status = 1;
				if (_bill_vm.Note == string.Empty) _bill_vm.Note = "Không có ghi chú";
				_bill_vm.HistoryConsumerPointID = _lstHCP_VM.FirstOrDefault().Id;
				if (_bill_vm.Recipient == string.Empty) _bill_vm.Recipient = _user_vm.UserName;
				_bill_vm.BillCode = "HD" + _lstBill.Max(c => Convert.ToInt32(c.BillCode.Substring(2)) + 1).ToString();
				_bill_vm.CreateDate = DateTime.Now;
				var addBill = await _httpClient.PostAsJsonAsync("https://localhost:7141/api/Bill/Post-Bill", Create_Bill_With_Info._bill_vm);
				if (addBill.IsSuccessStatusCode)
				{
					foreach (var x in _lstCI)
					{
						_pi_vm = _lstPrI_VM.Where(c => c.Id == x.ProductItemId).FirstOrDefault();
						BillItem_VM billItem_VM = new BillItem_VM();
						billItem_VM.Id = Guid.NewGuid();
						billItem_VM.BillId = Create_Bill_With_Info._bill_vm.Id;
						billItem_VM.ProductItemsId = x.ProductItemId;
						billItem_VM.Quantity = x.Quantity;
						billItem_VM.Price = _pi_vm.CostPrice;
						billItem_VM.Status = 1;
						_pi_vm.AvaiableQuantity -= x.Quantity;
						var a = await _httpClient.PostAsJsonAsync("https://localhost:7141/api/BillItem/Post-BillItem", billItem_VM);
						var b = await _httpClient.PutAsJsonAsync("https://localhost:7141/api/productitem/update_productitem", _pi_vm);
						var c = await _httpClient.DeleteAsync($"https://localhost:7141/api/CartItems/delete-CartItems/{x.Id}");
					}
					_toastService.ShowSuccess("Đơn hàng đã được tạo thành công, để theo dõi đơn hàng hãy vào mục Lịch sử đơn hàng");
				}
			}
		}

		public async Task BackToCart()
		{
			_navi.NavigateTo("https://localhost:7075/Customer/BanOnline/ShowCart", true);
		}

		public async Task ChonTinhTP()
		{
			if (_bill_vm.Province == _TinhTp) return;
			_lstQuanHuyen.Clear();
			_lstXaPhuong.Clear();
			_bill_vm.District = string.Empty;
			_bill_vm.WardName = string.Empty;
			if (_bill_vm.Province == string.Empty) return;
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
			if (_bill_vm.District == string.Empty) return;
			District_VM chon = _lstQuanHuyen_Data.FirstOrDefault(c => c.Name == _bill_vm.District);
			_lstXaPhuong = _lstXaPhuong_Data.Where(c => c.DistrictId == chon.Id).ToList();
			_QuanHuyen = _bill_vm.District;
		}

		public async Task ChonXaPhuong()
		{
			_XaPhuong = _bill_vm.WardName;
		}
	}
}