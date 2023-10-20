using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;

namespace DATN_Client.Areas.Admin.Components
{
    public partial class Address
    {
        [Inject] NavigationManager navigationManager { get; set; }
        HttpClient httpClient = new HttpClient();
        public List<AddressShip_VM> addressShip_s  = new List<AddressShip_VM>();   
        protected override async Task OnInitializedAsync()
        {
           addressShip_s  =   User.addressShips.ToList();
        }
    }
}
