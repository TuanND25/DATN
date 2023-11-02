﻿using DATN_Client.Areas.Customer.Controllers;
using DATN_Shared.ViewModel;
using DATN_Shared.ViewModel.Momo;
using Microsoft.AspNetCore.Components;

namespace DATN_Client.Areas.Customer.Component
{
    public partial class PaymentCallBack_Blazor
    {
        private HttpClient _client = new HttpClient();
        private MomoExecuteResponseModel _responseModel = new MomoExecuteResponseModel();
        private List<Bill_VM> _lstBill = new List<Bill_VM>();
        private List<CartItems_VM> _lstCI = new List<CartItems_VM>();
        private List<ProductItem_Show_VM> _lstPrI_show_VM = new List<ProductItem_Show_VM>();
        private List<ProductItem_VM> _lstPrI_VM = new List<ProductItem_VM>();

        private ProductItem_Show_VM _pi_s_vm = new ProductItem_Show_VM();
        private ProductItem_VM _pi_vm = new ProductItem_VM();
		[Inject] Blazored.Toast.Services.IToastService _toastService { get; set; } // Khai báo khi cần gọi ở code-behind
		protected override async Task OnInitializedAsync()
        {
            _responseModel = BanOnlineController._momoExecuteResponseModel;
            _lstPrI_show_VM = await _client.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");
            _lstPrI_VM = await _client.GetFromJsonAsync<List<ProductItem_VM>>("https://localhost:7141/api/productitem/get_all_productitem");
            _lstCI = await _client.GetFromJsonAsync<List<CartItems_VM>>($"https://localhost:7141/api/CartItems/{Create_Bill_With_Info._bill_vm.UserId}");
            _lstBill = await _client.GetFromJsonAsync<List<Bill_VM>>("https://localhost:7141/api/Bill/get_alll_bill");
            if (_responseModel.Message.ToLower() == "success")
            {
                Create_Bill_With_Info._bill_vm.BillCode = "HD" + _lstBill.Max(c => Convert.ToInt32(c.BillCode.Substring(2)) + 1).ToString();
                Create_Bill_With_Info._bill_vm.CreateDate = DateTime.Now;
                if (Create_Bill_With_Info._bill_vm.ToAddress == string.Empty) Create_Bill_With_Info._bill_vm.ToAddress = "...";
                var addBill = await _client.PostAsJsonAsync("https://localhost:7141/api/Bill/Post-Bill", Create_Bill_With_Info._bill_vm);
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
                    var a = await _client.PostAsJsonAsync("https://localhost:7141/api/BillItem/Post-BillItem", billItem_VM);
                    var b = await _client.PutAsJsonAsync("https://localhost:7141/api/productitem/update_productitem", _pi_vm);
                    var c = await _client.DeleteAsync($"https://localhost:7141/api/CartItems/delete-CartItems/{x.Id}");
                }
                _toastService.ShowSuccess("Đơn hàng đã được tạo thành công, để theo dõi đơn hàng hãy vào mục Lịch sử đơn hàng");
            }
        }
    }
}