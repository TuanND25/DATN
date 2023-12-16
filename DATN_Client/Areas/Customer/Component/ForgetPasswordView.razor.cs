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
		private int countdownMinutes = 2;
		private int countdownSeconds = 0;
		private System.Timers.Timer timer;
		public string timeline { get; set; }
		public async Task ForgetPasswordMethor()
        {
            if ( ForgetPasswordUser.PhoneNumber == string.Empty)
            {
                _toastService.ShowError(" Vui lòng điền đẩy đủ thông tin");
                return;
            }
			ToggleCountdown();

            var response = await httpClient.PostAsJsonAsync<ForgetPasswordRequest>("https://localhost:7141/api/forget-password/request", ForgetPasswordUser);
			var result = response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode)
            {
				_toastService.ShowSuccess("Mã xác thực OTP đã gửi về máy");
                Task.Delay(2000);
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
			var result =  response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode)
            {
                _toastService.ShowSuccess("Mật khẩu đã được gửi về điện thoại của bạn");
				await Task.Delay(2000);
				NavigationManager.NavigateTo("/login", true);
            }
            else
            {
                _toastService.ShowError(result.Result);
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
