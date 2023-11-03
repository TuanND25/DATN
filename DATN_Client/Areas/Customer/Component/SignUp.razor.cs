using System.Net;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace DATN_Client.Areas.Customer.Component
{
    public partial class SignUp
    {
        [Inject] NavigationManager navigationManager { get; set; }
        
        HttpClient _httpClient = new HttpClient();
		SignUpUser signUp = new SignUpUser();
		public string Message { get; set; } = string.Empty;
        public async Task SignUpUser()
        {
			Message= string.Empty;
			var respone = await _httpClient.PostAsJsonAsync<SignUpUser>("https://localhost:7141/api/user/signup", signUp);
            var result = respone.Content.ReadAsStringAsync();
          
            
            if (respone.IsSuccessStatusCode)
            {
                Message = "success";
                navigationManager.NavigateTo("https://localhost:7075/Customer/Login/Login",true);
            }
            else
            {
                if (respone.StatusCode == HttpStatusCode.Forbidden)
                {
                    Message = result.Result;
                }
                else
                {
                    Message = "error";
                }
                               
                    
                              
            }
        }
    }
}
