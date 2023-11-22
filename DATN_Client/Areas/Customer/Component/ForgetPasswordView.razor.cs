using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;
using NuGet.Protocol.Plugins;

namespace DATN_Client.Areas.Customer.Component
{
    public  partial class ForgetPasswordView
    {
        ForgetPasswordRequest ForgetPasswordUser = new ForgetPasswordRequest();
        [Inject] NavigationManager NavigationManager { get; set; }

        HttpClient httpClient = new HttpClient();
		[Inject] Blazored.Toast.Services.IToastService _toastService { get; set; } // Khai báo khi cần gọi ở code-behind
        public string responseAPI { get; set; } = string.Empty; 
		public string messgase { get; set; } = string.Empty;
        public async Task ForgetPasswordMethor()
        {
            if (ForgetPasswordUser.Email== string.Empty || ForgetPasswordUser.PhoneNumber == string.Empty || ForgetPasswordUser.Username == string.Empty)
            {
                _toastService.ShowError(" Vui lòng điền đẩy đủ thông tin");
                return;
            }


            var response = await httpClient.PostAsJsonAsync<ForgetPasswordRequest>("https://localhost:7141/api/forget-password/request", ForgetPasswordUser);
            if (response.IsSuccessStatusCode)
            {
                messgase = await response.Content.ReadAsStringAsync();
                responseAPI ="200";
           


            }
            else
            {
                messgase = await response.Content.ReadAsStringAsync();
                responseAPI = string.Empty;
				_toastService.ShowError(messgase);
            }
        }
        public async Task ForgetPasswordOTP()
        {

		    var response =	await httpClient.PostAsJsonAsync<ForgetPasswordRequest>("https://localhost:7141/api/forget-password/otp",ForgetPasswordUser);
            if (response.IsSuccessStatusCode)
            {
				NavigationManager.NavigateTo("https://localhost:7075/Customer/Login/Login", true);
            }
            else
            {
                _toastService.ShowError(await response.Content.ReadAsStringAsync());
            }
		}
    }
}
