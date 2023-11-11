using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;

namespace DATN_Client.Areas.Customer.Component
{
    public partial class BillByUser
    {
        [Inject] NavigationManager _navigationManager { get; set; }
        HttpClient _httpClient=new HttpClient();
        [Inject] Blazored.Toast.Services.IToastService _toastService { get; set; }
        [Inject] public IHttpContextAccessor _ihttpcontextaccessor { get; set; }
        public static Bill_VM _bill_VM=new Bill_VM();
        List<Bill_VM> _lstBills = new List<Bill_VM>();
        User _user=new User();

        protected override async Task OnInitializedAsync()
        {

            //var a = Guid.Parse(_ihttpcontextaccessor.HttpContext.Session.GetString("UserId"));
            //var a = Guid.Parse("a4c10abe-eec2-40e6-9b6c-cf1221e9da78");
            var a = Guid.Parse("c416a2f2-4a08-4787-ac30-4c856e9abf1a");
            _user = await _httpClient.GetFromJsonAsync<User>($"https://localhost:7141/api/user/get_user_by_id/{a}");
            _lstBills = await _httpClient.GetFromJsonAsync<List<Bill_VM>>($"https://localhost:7141/api/Bill/get_bill_by_user/{a}");
        }
        public async Task NavBillItem(Bill_VM bill_VM)
        {
            _bill_VM = bill_VM;
            _navigationManager.NavigateTo("https://localhost:7075/Customer/UserManagement/BilllItemByUser", true);
        }
    }
}
