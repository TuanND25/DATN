using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;

namespace DATN_Client.Areas.Customer.Component
{
    public partial class SignUp
    {
        [Inject] NavigationManager navigationManager { get; set; }
        SignUpUser signUp = new SignUpUser();
        HttpClient _httpClient = new HttpClient();

        public string Message { get; set; } = string.Empty;
        public async Task SignUpUser()
        {

            var respone = await _httpClient.PostAsJsonAsync<SignUpUser>("https://localhost:7141/api/user/signup", signUp);
            if (respone.IsSuccessStatusCode)
            {
                Message = "success";
                navigationManager.NavigateTo("https://localhost:7075/Customer/Login",true);
            }
            else
            {
                Message = "fail";
            }
        }
    }
}
