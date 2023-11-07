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


        User User_VM = new User();
        List<User> _lstUser_VM = new List<User>();
        [Inject] public IHttpContextAccessor _ihttpcontextaccessor { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }


        public ChangePassword_VM changePassword = new ChangePassword_VM();
        public string Message { get; set; } = string.Empty;

        [Inject] Blazored.Toast.Services.IToastService _toastService { get; set; } // Khai báo khi cần gọi ở code-behind
        bool isModalOpenAddAddress = false; // dùng để check đóng  popup
        bool isModalOpenUpdateAddress = false;
        bool isModalOpenUpdateUser = false;
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
                        new ObjectMenu { Id = 6, Name = "", ImagePath = "" },
                    };
            elActivity = true;

            //var a = Guid.Parse(_ihttpcontextaccessor.HttpContext.Session.GetString("UserId"));
            var a = Guid.Parse("a4c10abe-eec2-40e6-9b6c-cf1221e9da78");
            User_VM= await _client.GetFromJsonAsync<User>($"https://localhost:7141/api/user/get_user_by_id/{a}");
        }
        public async Task UpdateUser()
        {
            var a = await _client.PutAsJsonAsync($"https://localhost:7141/api/user/update-user", User_VM);
            if (a.IsSuccessStatusCode)
            {
                ClosePopup("UpdateUser");
                _toastService.ShowSuccess("Cập nhật thông tin người dùng thành công");
            }
            else
            {
                _toastService.ShowSuccess("Cập nhật thông tin người dùng thành công");
            }
        }
        public async Task LoadUser(Guid Id)
        {
            OpenPopup("UpdateUser");
            var a = await _client.GetFromJsonAsync<User>($"https://localhost:7141/api/user/get_user_by_id/{Id}");
            User_VM.Id = a.Id;
            User_VM.Name=a.Name;
            User_VM.Email=a.Email;
            User_VM.PhoneNumber=a.PhoneNumber;
            User_VM.Sex=a.Sex;
            User_VM.UserName=a.UserName;          
        }

       


        private void elActiveTab(int Id)
        {
            ChangeColorBtn = Id;
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
                case "UpdateUser":
                    isModalOpenUpdateUser = isOpen;
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

        public class ObjectMenu
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string ImagePath { get; set; }
        }

    }

}



