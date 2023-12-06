using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;

namespace DATN_Client.Areas.Customer.Component
{
    public partial class BilllItemByUser
    {
        [Inject] NavigationManager _navigationManager { get; set; }
        HttpClient _httpClient = new HttpClient();
        [Inject] Blazored.Toast.Services.IToastService _toastService { get; set; }

        public Bill_VM _bill = new Bill_VM();
        Bill_ShowModel _bill_ShowModel = new Bill_ShowModel();

        BillItem_VM _billItem = new BillItem_VM();  
        List<BillDetailShow> _lstBillItems = new List<BillDetailShow>();
        List<AddressShip_VM> _lstAddressGetById = new List<AddressShip_VM>();
        [Inject] public IHttpContextAccessor _ihttpcontextaccessor { get; set; }

        protected async override Task OnInitializedAsync()
        {
            _bill = BillByUser._bill_VM;
            var a = await _httpClient.GetFromJsonAsync<List<Bill_ShowModel>>("https://localhost:7141/api/Bill/get_alll_bill");
            _bill_ShowModel = a.FirstOrDefault(x => x.Id == _bill.Id);
            _lstBillItems = await _httpClient.GetFromJsonAsync<List<BillDetailShow>>($"https://localhost:7141/api/BillItem/getbilldetail/{_bill.Id}");


            var c = Guid.Parse(_ihttpcontextaccessor.HttpContext.Session.GetString("UserId"));
            //var c = Guid.Parse("F3C0CEA4-8F18-4990-908A-5DF1169F87A2");
            var d = await _httpClient.GetFromJsonAsync<List<AddressShip_VM>>($"https://localhost:7141/api/AddressShip/get_address_by_UserID/{c}");
            _lstAddressGetById = d.OrderByDescending(x => x.Status).ToList();

        }
        public async Task ChonDiaChiTuList(AddressShip_VM addressShip_VM)
        {
            _bill_ShowModel.Recipient = addressShip_VM.Recipient;
            _bill_ShowModel.PhoneNumber = addressShip_VM.NumberPhone;
            _bill_ShowModel.Province = addressShip_VM.Province;
            _bill_ShowModel.District = addressShip_VM.District;
            _bill_ShowModel.WardName = addressShip_VM.WardName;

            var a = await _httpClient.PutAsJsonAsync("https://localhost:7141/api/Bill/Put-Bill", _bill_ShowModel);
            if (a.IsSuccessStatusCode)
            {
                _toastService.ShowSuccess("Thay đổi địa chỉ thành công");
            }
            else
            {
                _toastService.ShowError("Thay đổi địa chỉ thất bại");
            }

        }
        public async Task HuyDonHang()
        {
            _bill_ShowModel.Status = 0;
            var a = await _httpClient.PutAsJsonAsync("https://localhost:7141/api/Bill/Put-Bill", _bill_ShowModel);
            if (a.IsSuccessStatusCode)
            {
                _toastService.ShowSuccess("Huỷ đơn hàng thành công");
            }
            else
            {
                _toastService.ShowError("Huỷ đơn hàng thất bại");
            }
        }
    }
}
