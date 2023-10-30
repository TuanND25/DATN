using DATN_Shared.ViewModel;
using DATN_Shared.ViewModel.Momo;
using DATN_Shared.ViewModel.Momo.Order;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using static System.Net.WebRequestMethods;

namespace DATN_Client.Areas.Customer.Component
{
	public partial class Create_Bill_With_Info
	{
		HttpClient _httpClient = new HttpClient();
		[Inject] NavigationManager _navi { get; set; }
		[Inject] Blazored.SessionStorage.ISessionStorageService _SessionStorageService { get; set; }
		List<User_VM> _lstUser = new List<User_VM>();
		List<CartItems_VM> _lstCI = new List<CartItems_VM>();
		List<Image_VM> _lstImg = new List<Image_VM>();
		List<ProductItem_Show_VM> _lstPrI_show_VM = new List<ProductItem_Show_VM>();
	    public static Bill_VM _bill_vm = new Bill_VM();
		User_VM _user_vm = new User_VM();
		ProductItem_Show_VM _pi_s_vm = new ProductItem_Show_VM();
		OrderInfoModel _ord = new OrderInfoModel();

		public string _sdt { get; set; }
		public int? _tongTienHang { get; set; } = 0;
		protected override async Task OnInitializedAsync()
		{
			_bill_vm.UserId = Guid.Parse(await _SessionStorageService.GetItemAsStringAsync("session"));
			_lstUser = await _httpClient.GetFromJsonAsync<List<User_VM>>("https://localhost:7141/api/user/get-user");
			_user_vm = _lstUser.Where(c => c.Id == _bill_vm.UserId).FirstOrDefault();
			_lstImg = (await _httpClient.GetFromJsonAsync<List<Image_VM>>("https://localhost:7141/api/Image")).OrderBy(c => c.STT).ToList();
			_lstCI = await _httpClient.GetFromJsonAsync<List<CartItems_VM>>($"https://localhost:7141/api/CartItems/{_bill_vm.UserId}");
			_lstPrI_show_VM = await _httpClient.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");
			_bill_vm.NumberPhone = _user_vm.PhoneNumber;
			_bill_vm.PaymentMethodId = Guid.Parse("58431a43-d36b-4ab1-af0e-402238c87402");
			_bill_vm.Note = string.Empty;
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
			//var abc = _bill_vm;
			// Bill
			_bill_vm.Id = Guid.NewGuid();
			_bill_vm.TotalAmount = _tongTienHang;
			_bill_vm.Type = 1;
			_bill_vm.Province = "Hà Nội";
			_bill_vm.District = "Hoài Đức";
			_bill_vm.WardName = "Đức Giang";
			_bill_vm.Status = 1;
			_bill_vm.HistoryConsumerPointID = Guid.Parse("F170DF48-4A56-48E8-9095-500CD4A562A3");
			// Order
			_ord.OrderId = Guid.NewGuid().ToString();
			_ord.FullName = _user_vm.UserName;
			_ord.OrderInfo = _bill_vm.Note;
			_ord.Amount = _tongTienHang;
			var reponse = await _httpClient.PostAsJsonAsync("https://localhost:7141/api/Momo/CreatePaymentAsync", _ord);
			var reponse2 = await reponse.Content.ReadFromJsonAsync<MomoCreatePaymentResponseModel>();
			_navi.NavigateTo($"{reponse2.PayUrl}", true);
		}
        public async Task ReturnCart()
        {
			_navi.NavigateTo("https://localhost:7075/Customer/BanOnline/ShowCart", true);
		}
	}
}
