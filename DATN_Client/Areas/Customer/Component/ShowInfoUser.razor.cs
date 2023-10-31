using Blazored.SessionStorage;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;

namespace DATN_Client.Areas.Customer.Component
{
    public partial class ShowInfoUser
    {
        HttpClient _client = new HttpClient();
        public List<ObjectMenu> listDetail = new List<ObjectMenu>();
        public bool elActivity { get; set; }
        public int ChangeColorBtn { get; set; } = 1;
        private List<AddressShip_VM> _lstAddressGetById = new List<AddressShip_VM>();
        public string TypeInput_Pass { get; set; } = "password";
        public string Icon_Pass { get; set; } = "fa-regular fa-eye-slash";
        public string TypeInput_New { get; set; } = "password";
        public string Icon_New { get; set; } = "fa-regular fa-eye-slash";
        public string TypeInput_CheckNew { get; set; } = "password";
        public string Icon_CheckNew { get; set; } = "fa-regular fa-eye-slash";
        protected override async Task OnInitializedAsync()
        {
             listDetail = new List<ObjectMenu>
                    {
                        new ObjectMenu { Id = 1, Name = "Thông tin tài khoản", ImagePath = "fa-solid fa-user-pen" },
                        new ObjectMenu { Id = 2, Name = "Lịch sử đơn hàng", ImagePath = "fa-regular fa-chart-bar" },
                        new ObjectMenu { Id = 3, Name = "Sổ địa chỉ", ImagePath = "fa-solid fa-location-dot" },
                        new ObjectMenu { Id = 4, Name = "Đăng xuất", ImagePath = "fa-solid fa-power-off" },
                    };
            elActivity = true;
            _lstAddressGetById = await _client.GetFromJsonAsync<List<AddressShip_VM>>("https://localhost:7141/api/AddressShip/UserId?UserId=e4ff175e-91ab-41ba-aa46-ba4e87fdb5bf");
        }
      
        private void  elActiveTab(int Id)
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



