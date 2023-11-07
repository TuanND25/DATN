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
        protected override async Task OnInitializedAsync()
        {
            //var token = _ihttpcontextaccessor.HttpContext.Session.GetString("Token"); // Gọi token
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); // Xác thực

            var a = Guid.Parse(_ihttpcontextaccessor.HttpContext.Session.GetString("UserId"));
            //var a = Guid.Parse("a4c10abe-eec2-40e6-9b6c-cf1221e9da78");
            User_VM = await _client.GetFromJsonAsync<User>($"https://localhost:7141/api/user/get_user_by_id/{a}");
            var d = await _client.GetFromJsonAsync<List<AddressShip_VM>>($"https://localhost:7141/api/AddressShip/get_address_by_UserID/{a}");
            _lstAddressGetById = d.OrderByDescending(x => x.Status).ToList();
            _lstTinhTp_Data = await _client.GetFromJsonAsync<List<Province_VM>>("https://api.npoint.io/ac646cb54b295b9555be");
            _lstTinhTp = _lstTinhTp_Data;
            _lstQuanHuyen_Data = await _client.GetFromJsonAsync<List<District_VM>>("https://api.npoint.io/34608ea16bebc5cffd42");
            _lstXaPhuong_Data = await _client.GetFromJsonAsync<List<Ward_VM>>("https://api.npoint.io/dd278dc276e65c68cdf5");
        }

        public async Task AddAdress()
        {
            addressShip_VM.Id = Guid.NewGuid();
            addressShip_VM.UserId = User_VM.Id;
            if (addressShip_VM.Status == 1)
            {
                foreach (var item in _lstAddressGetById)
                {
                    item.Status = 0;
                    var d = await _client.PutAsJsonAsync("https://localhost:7141/api/AddressShip/Put-Address", item);
                }
            }
            var a = await _client.PostAsJsonAsync("https://localhost:7141/api/AddressShip/Post-Address", addressShip_VM);
            if (a.IsSuccessStatusCode)
            {
                await LoadAddress();
                ClosePopup("AddAddress");
                _toastService.ShowSuccess("Thêm địa chỉ thành công");
            }
            else
            {
                _toastService.ShowError("Thêm mới địa chỉ thất bại");
            }
        }
        public async Task Load()  // khi nhấn ra ngoài popup thì popup sẽ tắt và dữ liệu sẽ bị clear
        {
            addressShip_VM.Recipient = String.Empty;
            addressShip_VM.NumberPhone = String.Empty;
            addressShip_VM.ToAddress = String.Empty;
            addressShip_VM.Province = String.Empty;
            addressShip_VM.District = String.Empty;
            addressShip_VM.WardName = String.Empty;
            OpenPopup("AddAddress");
        }
        public async Task LoadAddress()
        {
			var a = Guid.Parse(_ihttpcontextaccessor.HttpContext.Session.GetString("UserId"));
			var d = await _client.GetFromJsonAsync<List<AddressShip_VM>>($"https://localhost:7141/api/AddressShip/get_address_by_UserID/{a}");// sau khi xong phải đổi qua session
            _lstAddressGetById = d.OrderByDescending(x => x.Status).ToList();
        }
        public async Task UpdateAdress() //update-address
        {
            if (addressShip_VM.Status == 1)
            {
                foreach (var item in _lstAddressGetById)
                {
                    item.Status = 0;
                    var d = await _client.PutAsJsonAsync("https://localhost:7141/api/AddressShip/Put-Address", item);
                }
            }
            addressShip_VM.UserId = User_VM.Id;
            var a = await _client.PutAsJsonAsync("https://localhost:7141/api/AddressShip/Put-Address", addressShip_VM);
            if (a.IsSuccessStatusCode)
            {
                await LoadAddress();
                _toastService.ShowSuccess("Cập nhật địa chỉ thành công");
                ClosePopup("UpdateAddress");
            }
            else
            {
                _toastService.ShowError("Cập nhật địa chỉ thất bại");
            }
        }

        public async Task LoadUpdate(Guid Id)  // load dữ liệu lên popup
        {
            OpenPopup("UpdateAddress");
            var b = await _client.GetFromJsonAsync<AddressShip_VM>($"https://localhost:7141/api/AddressShip/get_address_by_id/{Id}");
            addressShip_VM.Id = b.Id;
            addressShip_VM.Recipient = b.Recipient;
            addressShip_VM.Province = b.Province;
            await ChonTinhTP();
            addressShip_VM.District = b.District;
            await ChonQuanHuyen();
            addressShip_VM.WardName = b.WardName;
            await ChonXaPhuong();
            addressShip_VM.ToAddress = b.ToAddress;
            addressShip_VM.NumberPhone = b.NumberPhone;
            addressShip_VM.Status = b.Status;
        }
        public async Task DeleteAddress(Guid Id)
        {
            var a = await _client.DeleteAsync($"https://localhost:7141/api/AddressShip/Delete-Address/{Id}");
            if (a.IsSuccessStatusCode)
            {
                await LoadAddress();
                _toastService.ShowSuccess("Xoá địa chỉ thành công");
            }
            else
            {
                _toastService.ShowError("Xoá địa chỉ thất bại");
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
                await LoadAddress();
                _toastService.ShowSuccess("Đặt địa chỉ thành mặc định thành công");
            }
            else
            {
                _toastService.ShowError("Đặt địa chỉ thành mặc định thất bại");
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
            chon = _lstTinhTp_Data.FirstOrDefault(c => c.Name == addressShip_VM.Province);
            _lstQuanHuyen = _lstQuanHuyen_Data.Where(c => c.ProvinceId == chon.Id).ToList();
            _TinhTp = addressShip_VM.Province;
        }

        public async Task ChonQuanHuyen()
        {
            if (addressShip_VM.District == _QuanHuyen) return;
            _lstXaPhuong.Clear();
            addressShip_VM.WardName = string.Empty;
            if (addressShip_VM.District == string.Empty) return;
            District_VM chon = _lstQuanHuyen.FirstOrDefault(c => c.Name == addressShip_VM.District);
            _lstXaPhuong = _lstXaPhuong_Data.Where(c => c.DistrictId == chon.Id).ToList();
            _QuanHuyen = addressShip_VM.District;
        }

        public async Task ChonXaPhuong()
        {
            _XaPhuong = addressShip_VM.WardName;
        }

        private void SetModalState(bool isOpen, string modalType)
        {
            switch (modalType)
            {
                case "AddAddress":
                    isModalOpenAddAddress = isOpen;
                    break;
                case "UpdateAddress":
                    isModalOpenUpdateAddress = isOpen;
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
    }
}
