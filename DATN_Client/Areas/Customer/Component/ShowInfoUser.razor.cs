using Blazored.SessionStorage;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using DATN_Shared.ViewModel.DiaChi;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;

namespace DATN_Client.Areas.Customer.Component
{
    public partial class ShowInfoUser
    {
        HttpClient _client = new HttpClient();
        public List<ObjectMenu> listDetail = new List<ObjectMenu>();
        public bool elActivity { get; set; }
        public int ChangeColorBtn { get; set; } = 1;

        public string TypeInput_Pass { get; set; } = "password";
        public string Icon_Pass { get; set; } = "fa-regular fa-eye-slash";
        public string TypeInput_New { get; set; } = "password";
        public string Icon_New { get; set; } = "fa-regular fa-eye-slash";
        public string TypeInput_CheckNew { get; set; } = "password";
        public string Icon_CheckNew { get; set; } = "fa-regular fa-eye-slash";


        User User_VM = new User();
        List<User> _lstUser_VM = new List<User>();
        [Inject] public IHttpContextAccessor _ihttpcontextaccessor { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }


        public ChangePassword_VM changePassword = new ChangePassword_VM();
        public string Message { get; set; } = string.Empty;


        private List<AddressShip_VM> _lstAddressGetById = new List<AddressShip_VM>();
        AddressShip AddressShip=new AddressShip();
        AddressShip_VM addressShip_VM = new AddressShip_VM();
        private List<Province_VM> _lstTinhTp = new List<Province_VM>();
        private List<District_VM> _lstQuanHuyen = new List<District_VM>();
        private List<Ward_VM> _lstXaPhuong = new List<Ward_VM>();
        public string _TinhTp { get; set; }
        public string _QuanHuyen { get; set; }
        public string _XaPhuong { get; set; }
        private bool IsDefaultAddress
        {
            get { return addressShip_VM.Status == 1; }
            set { addressShip_VM.Status = value ? 1 : 0; }
        }



        protected override async Task OnInitializedAsync()
        {
            //var token = _ihttpcontextaccessor.HttpContext.Session.GetString("Token"); // Gọi token
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); // Xác thực

            listDetail = new List<ObjectMenu>
                    {
                        new ObjectMenu { Id = 1, Name = "Thông tin tài khoản", ImagePath = "fa-solid fa-user-pen" },
                        new ObjectMenu { Id = 2, Name = "Lịch sử đơn hàng", ImagePath = "fa-regular fa-chart-bar" },
                        new ObjectMenu { Id = 3, Name = "Sổ địa chỉ", ImagePath = "fa-solid fa-location-dot" },
                        new ObjectMenu { Id = 4, Name = "Đổi mật khẩu", ImagePath = "fa-solid fa-power-off" },
                        new ObjectMenu { Id = 5, Name = "Đăng xuất", ImagePath = "fa-solid fa-power-off" },

                    };
            elActivity = true;


            //var a = Guid.Parse(_ihttpcontextaccessor.HttpContext.Session.GetString("UserId"));
            var a = Guid.Parse("a4c10abe-eec2-40e6-9b6c-cf1221e9da78");
            User_VM = await _client.GetFromJsonAsync<User>($"https://localhost:7141/api/user/get_user_by_id/{a}");

            _lstAddressGetById = await _client.GetFromJsonAsync<List<AddressShip_VM>>($"https://localhost:7141/api/AddressShip/get_address_by_UserID/{a}");
            _lstAddressGetById = _lstAddressGetById.OrderByDescending(x => x.Status).ToList();
            _lstTinhTp = await _client.GetFromJsonAsync<List<Province_VM>>("https://api.npoint.io/ac646cb54b295b9555be"); //


        }
        public async Task UpdateUser()
        {
            var a = await _client.PutAsJsonAsync($"https://localhost:7141/api/user/update-user", User_VM);
            _navigationManager.NavigateTo("https://localhost:7075/Customer/UserManagement", true);
        }

        public async Task ChangePasswordMethor()
        {
            changePassword.UserId = Guid.Parse(_ihttpcontextaccessor.HttpContext.Session.GetString("UserId"));
            var response = await _client.PutAsJsonAsync<ChangePassword_VM>("https://localhost:7141/api/user/change-password/", changePassword);
            if (response.IsSuccessStatusCode)
            {
                Message = "success";
            }
            else
            {
                Message = "error";
            }
        }


        public async Task ChonTinhTP()
        {
            if (addressShip_VM.Province == _TinhTp) return;
            _lstQuanHuyen.Clear();
            _lstXaPhuong.Clear();
            addressShip_VM.District = string.Empty;
            addressShip_VM.WardName = string.Empty;
            if (addressShip_VM.Province == string.Empty) return;
            Province_VM chon = new Province_VM();
            chon = _lstTinhTp.FirstOrDefault(c => c.Name == addressShip_VM.Province);
            _lstQuanHuyen =
                (await _client.GetFromJsonAsync<List<District_VM>>("https://api.npoint.io/34608ea16bebc5cffd42"))
                .Where(c => c.ProvinceId == chon.Id)
                .ToList();
            _TinhTp = addressShip_VM.Province;
        }

        public async Task ChonQuanHuyen()
        {
            if (addressShip_VM.District == _QuanHuyen) return;
            _lstXaPhuong.Clear();
            addressShip_VM.WardName = string.Empty;
            if (addressShip_VM.District == string.Empty) return;
            District_VM chon = _lstQuanHuyen.FirstOrDefault(c => c.Name == addressShip_VM.District);
            _lstXaPhuong =
                (await _client.GetFromJsonAsync<List<Ward_VM>>("https://api.npoint.io/dd278dc276e65c68cdf5"))
                .Where(c => c.DistrictId == chon.Id)
                .ToList();
            _QuanHuyen = addressShip_VM.District;
        }

        public async Task ChonXaPhuong()
        {
            _XaPhuong = addressShip_VM.WardName;
        }


        public async Task AddAdress()
        {

            addressShip_VM.Id=Guid.NewGuid();
            addressShip_VM.UserId = User_VM.Id;
            var a = await _client.PostAsJsonAsync("https://localhost:7141/api/AddressShip/Post-Address",addressShip_VM);
            if (a.IsSuccessStatusCode)
            {
                _navigationManager.NavigateTo("https://localhost:7075/Customer/UserManagement", true);


            }

        }
        public async Task Load()
        {
            addressShip_VM.Recipient = String.Empty;
            addressShip_VM.NumberPhone = String.Empty;
            addressShip_VM.ToAddress = String.Empty;
            addressShip_VM.Province = String.Empty;
            addressShip_VM.District = String.Empty;
            addressShip_VM.WardName = String.Empty;
        }
        public async Task UpdateAdress()
        {
            addressShip_VM.UserId = User_VM.Id;
            var a = await _client.PutAsJsonAsync("https://localhost:7141/api/AddressShip/Put-Address", addressShip_VM);
            if (a.IsSuccessStatusCode)
            {
                _navigationManager.NavigateTo("https://localhost:7075/Customer/UserManagement", true);
            }
        }

        public async Task LoadUpdate(Guid Id)
        {
            var b = await _client.GetFromJsonAsync<AddressShip_VM>($"https://localhost:7141/api/AddressShip/get_address_by_id/{Id}");
            addressShip_VM.Id=b.Id;
            addressShip_VM.Recipient = b.Recipient;
            addressShip_VM.Province = b.Province;
            await ChonTinhTP();
            addressShip_VM.District = b.District;
            await ChonQuanHuyen();
            addressShip_VM.WardName = b.WardName;
            await ChonXaPhuong();
            addressShip_VM.ToAddress = b.ToAddress;
            addressShip_VM.NumberPhone = b.NumberPhone;


        }
        public async Task DeleteAddress(Guid Id)
        {
            var a = await _client.DeleteAsync($"https://localhost:7141/api/AddressShip/Delete-Address/{Id}");
            if (a.IsSuccessStatusCode)
            {
                _navigationManager.NavigateTo("https://localhost:7075/Customer/UserManagement", true);
            }
        }

        public async Task SetDefaultAddress(Guid Id)
        {            
            foreach (var item in _lstAddressGetById)
            {
                item.Status = 0;
                var d = await _client.PutAsJsonAsync("https://localhost:7141/api/AddressShip/Put-Address", item);
            }
            addressShip_VM = await _client.GetFromJsonAsync<AddressShip_VM>($"https://localhost:7141/api/AddressShip/get_address_by_id/{Id}");
            addressShip_VM.Status = 1;
            var a = await _client.PutAsJsonAsync("https://localhost:7141/api/AddressShip/Put-Address", addressShip_VM);
            if (a.IsSuccessStatusCode)
            {
                _navigationManager.NavigateTo("https://localhost:7075/Customer/UserManagement", true);
            }
        }















































        private void elActiveTab(int Id)
        {
            ChangeColorBtn = Id;
        }
        private void ShowPass()
        {
            if (TypeInput_Pass == "text")
            {
                TypeInput_Pass = "password";
                Icon_Pass = "fa-regular fa-eye-slash";
            }
            else
            {
                TypeInput_Pass = "text";
                Icon_Pass = "fa-regular fa-eye";
            }
        }
        private void ShowNewPass()
        {
            if (TypeInput_New == "text")
            {
                TypeInput_New = "password";
                Icon_New = "fa-regular fa-eye-slash";
            }
            else
            {
                TypeInput_New = "text";
                Icon_New = "fa-regular fa-eye";
            }
        }
        private void ShowCheckNewPass()
        {
            if (TypeInput_CheckNew == "text")
            {
                TypeInput_CheckNew = "password";
                Icon_CheckNew = "fa-regular fa-eye-slash";
            }
            else
            {
                TypeInput_CheckNew = "text";
                Icon_CheckNew = "fa-regular fa-eye";

            }
        }
        public class ObjectMenu
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string ImagePath { get; set; }
        }

    }

}



