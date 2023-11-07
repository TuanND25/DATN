using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using DATN_Shared.ViewModel.DiaChi;
using Microsoft.AspNetCore.Components;

namespace DATN_Client.Areas.Customer.Component
{
    public partial class Address
    {
        HttpClient _client = new HttpClient();
        User User_VM = new User();
        List<User> _lstUser_VM = new List<User>();
        [Inject] public IHttpContextAccessor _ihttpcontextaccessor { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }

        private List<AddressShip_VM> _lstAddressGetById = new List<AddressShip_VM>();
        AddressShip AddressShip = new AddressShip();
        AddressShip_VM addressShip_VM = new AddressShip_VM();
        private List<Province_VM> _lstTinhTp = new List<Province_VM>();
        private List<District_VM> _lstQuanHuyen = new List<District_VM>();
        private List<Ward_VM> _lstXaPhuong = new List<Ward_VM>();

        private List<Province_VM> _lstTinhTp_Data = new List<Province_VM>();
        private List<District_VM> _lstQuanHuyen_Data = new List<District_VM>();
        private List<Ward_VM> _lstXaPhuong_Data = new List<Ward_VM>();
        public string _TinhTp { get; set; }
        public string _QuanHuyen { get; set; }
        public string _XaPhuong { get; set; }
        private bool IsDefaultAddress
        {
            get { return addressShip_VM.Status == 1; }
            set { addressShip_VM.Status = value ? 1 : 0; }
        }
        [Inject] Blazored.Toast.Services.IToastService _toastService { get; set; } // Khai báo khi cần gọi ở code-behind
        bool isModalOpenAddAddress = false; // dùng để check đóng  popup
        bool isModalOpenUpdateAddress = false;
        bool isModalOpenUpdateUser = false;
        protected override async Task OnInitializedAsync()
        {
            //var token = _ihttpcontextaccessor.HttpContext.Session.GetString("Token"); // Gọi token
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); // Xác thực



            //var a = Guid.Parse(_ihttpcontextaccessor.HttpContext.Session.GetString("UserId"));
            var a = Guid.Parse("a4c10abe-eec2-40e6-9b6c-cf1221e9da78");
            User_VM = await _client.GetFromJsonAsync<User>($"https://localhost:7141/api/user/get_user_by_id/{a}");
            var d = await _client.GetFromJsonAsync<List<AddressShip_VM>>($"https://localhost:7141/api/AddressShip/get_address_by_UserID/{a}");
            _lstAddressGetById = d.OrderByDescending(x => x.Status).ToList();
            _lstTinhTp_Data = await _client.GetFromJsonAsync<List<Province_VM>>("https://api.npoint.io/ac646cb54b295b9555be");
            _lstTinhTp = _lstTinhTp_Data;
            _lstQuanHuyen_Data = await _client.GetFromJsonAsync<List<District_VM>>("https://api.npoint.io/34608ea16bebc5cffd42");
            _lstXaPhuong_Data = await _client.GetFromJsonAsync<List<Ward_VM>>("https://api.npoint.io/dd278dc276e65c68cdf5");
        }
    }
}
