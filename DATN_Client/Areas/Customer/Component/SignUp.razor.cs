using System.Net;
using System.Text.RegularExpressions;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Twilio.Rest.Api.V2010.Account;

namespace DATN_Client.Areas.Customer.Component
{
    public partial class SignUp
    {
        [Inject] Blazored.Toast.Services.IToastService _toastService { get; set; } // Khai báo khi cần gọi ở code-behind
        [Inject] NavigationManager navigationManager { get; set; }

        HttpClient _httpClient = new HttpClient();
        SignUpUser signUp = new SignUpUser();
        public string check { get; set; } = string.Empty;
		private int countdownMinutes = 5;
		private int countdownSeconds = 0;
		private bool isCountingDown = false;
		private System.Timers.Timer timer;
		public async Task SignUpUser()
        {
               
        
            if (signUp.UserName == string.Empty || signUp.Email == string.Empty || signUp.PhoneNumber == string.Empty || signUp.Password == string.Empty || signUp.ConfirmPassword == string.Empty || signUp.Name == string.Empty)
            {
                _toastService.ShowError("Vui lòng điền đầy đủ thông tin");
                return;
            }
            Regex phoneNumberRegex = new Regex(@"^0\d{9}$");
            if (!phoneNumberRegex.IsMatch(signUp.PhoneNumber))
            {
                _toastService.ShowError("PhoneNumber không hợp lệ");
                return;
                
            }
            
            var respone = await _httpClient.PostAsJsonAsync<SignUpUser>("https://localhost:7141/api/user/signup", signUp);
            var result = respone.Content.ReadAsStringAsync();
            if (respone.IsSuccessStatusCode)
            {
                _toastService.ShowSuccess("Mời bạn xác thực");
                check = "success";
                await Task.Delay(2000);
                
                
                return;
            }
            else
            {

                    _toastService.ShowError(result.Result);
                    return;
                

                

            }
        }
        public async Task SignUPOTP()
        {
            var response = await _httpClient.PostAsJsonAsync<SignUpUser>("https://localhost:7141/api/user/signup-otp",signUp);
            var result = response.Content.ReadAsStringAsync();  
            if (response.IsSuccessStatusCode)
            {
                _toastService.ShowSuccess("Đăng ký thành công");
                Task.Delay(3000);
				navigationManager.NavigateTo("https://localhost:7075/Customer/Login/Login", true);
				return;
            }
            else
            {
				_toastService.ShowError(result.Result);
				Task.Delay(2000);
                return;
			}
        }

		
	}
}

