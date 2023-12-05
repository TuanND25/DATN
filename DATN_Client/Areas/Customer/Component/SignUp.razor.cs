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
		private int countdownMinutes = 2;
		private int countdownSeconds = 0;
		private System.Timers.Timer timer;
		public string timeline { get; set; }
		public async Task SignUpUser()
		{
			var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
			if (hasSymbols.IsMatch(signUp.Name))
			{
				_toastService.ShowError("Tên người dùng không chứa được các ký tự được biệt");
				return;
			}
			if (hasSymbols.IsMatch(signUp.UserName))
			{
				_toastService.ShowError("Tên đăng nhập không chứa được các ký tự được biệt");
				return;
			}
			if (hasSymbols.IsMatch(signUp.Password))
			{
				_toastService.ShowError("Mật khẩu không chứa được các ký tự được biệt");
				return;
			}
			if (hasSymbols.IsMatch(signUp.ConfirmPassword))
			{
				_toastService.ShowError("Xác nhận mật khẩu không chứa được các ký tự được biệt");
				return;
			}
			if (hasSymbols.IsMatch(signUp.Email))
			{
				_toastService.ShowError("Email không chứa được các ký tự được biệt");
				return;
			}
			
			if (signUp.UserName == string.Empty || signUp.Email == string.Empty || signUp.PhoneNumber == string.Empty || signUp.Password == string.Empty || signUp.ConfirmPassword == string.Empty || signUp.Name == string.Empty)
            {
                _toastService.ShowError("Vui lòng điền đầy đủ thông tin");
                return;
            }
			ToggleCountdown();
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
                 await Task.Delay(3000);
                navigationManager.NavigateTo("https://localhost:7075/Customer/Login/Login",true);
				
				return;
            }
            else
            {
				_toastService.ShowError(result.Result);
				Task.Delay(2000);
                return;
			}
        }
		private string GetButtonText()
		{
			return "Reset and Start 2-minute Countdown";
		}

		private void ToggleCountdown()
		{
			ResetCountdown();
			StartCountdown();
		}

		private void StartCountdown()
		{
			InitializeTimer();
		}

		private void ResetCountdown()
		{
			countdownMinutes = 2;
			countdownSeconds = 0;

			if (timer != null)
			{
				timer.Stop();
				timer.Dispose();
			}

			InvokeAsync(StateHasChanged);
		}

		private void InitializeTimer()
		{
			timer = new System.Timers.Timer(1000);
			timer.Elapsed += TimerElapsed;
			timer.AutoReset = true;
			timer.Enabled = true;
		}

		private void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			if (countdownMinutes == 0 && countdownSeconds == 0)
			{
				timer.Stop();
				timeline = string.Empty;
				// Thực hiện hành động khi đồng hồ đếm ngược đạt đến 0.
			}
			else
			{
				if (countdownSeconds == 0)
				{
					countdownSeconds = 59;
					countdownMinutes--;
				}
				else
				{
					countdownSeconds--;
				}
				timeline = "timelinecss";
			}

			InvokeAsync(StateHasChanged);
		}

	}
}

