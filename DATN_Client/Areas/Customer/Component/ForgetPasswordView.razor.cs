using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;

namespace DATN_Client.Areas.Customer.Component
{
    public  partial class ForgetPasswordView
    {
        ForgetPasswordRequest ForgetPasswordUser = new ForgetPasswordRequest();
        [Inject] NavigationManager NavigationManager { get; set; }

        HttpClient httpClient = new HttpClient();
        public string messgase { get; set; } = string.Empty;
        public async Task ForgetPasswordMethor()
        {
            var response = await httpClient.PostAsJsonAsync<ForgetPasswordRequest>("https://localhost:7141/api/forget-password/request", ForgetPasswordUser);
            if (response.IsSuccessStatusCode)
            {
                messgase = await response.Content.ReadAsStringAsync();


            }
            else
            {
                messgase = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
