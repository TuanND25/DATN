using System.Net.Http;
using System.Text;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;

namespace DATN_Client.Areas.Admin.Components
{
    public partial class Voucher
    {
        HttpClient httpClient = new HttpClient();
        [Inject] NavigationManager navigationManager { get; set; }
        public string Message { get; set; }= string.Empty;
         
        List<Voucher_VM> listvouchers = new List<Voucher_VM>();

        public Voucher_VM voucher = new Voucher_VM();
        protected override async Task OnInitializedAsync()
        {
            listvouchers = await httpClient.GetFromJsonAsync<List<Voucher_VM>>("https://localhost:7141/api/Voucher");
        }

        public async Task AddVoucher()
        {
            var response = await httpClient.PostAsJsonAsync<Voucher_VM>("https://localhost:7141/api/Voucher/Post-Voucher", voucher);
            var result = response.Content.ReadAsStringAsync();
            if (result.IsCompletedSuccessfully)
            {
				navigationManager.NavigateTo("https://localhost:7075/Admin/Voucher", true);
            }
            else
            {
                Message = "fail";
            }
         
        }
        public async Task UpdateVoucher()
        {
            var response = await httpClient.PutAsJsonAsync<Voucher_VM>("https://localhost:7141/api/Voucher/Post-Voucher", voucher);
            var result = response.Content?.ReadAsStringAsync();
            if (result.IsCompletedSuccessfully)
            {
				navigationManager.NavigateTo("https://localhost:7075/Admin/Voucher", true);
			}
            else
            {
				Message = "fail";
			}
        }
    }
}
