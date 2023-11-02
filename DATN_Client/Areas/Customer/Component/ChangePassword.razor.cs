using Blazored.SessionStorage;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;

namespace DATN_Client.Areas.Customer.Component
{
    public partial class ChangePassword
    {
        [Inject] public IHttpContextAccessor _ihttpcontextaccessor { get; set; }
        HttpClient _httpClient  = new HttpClient();
        [Inject] NavigationManager NavigationManager { get; set; }

        public ChangePassword_VM changePassword = new ChangePassword_VM();
        public string Message { get; set; } = string.Empty;
    

        public async Task ChangePasswordMethor()
        {
            changePassword.UserId = Guid.Parse( _ihttpcontextaccessor.HttpContext.Session.GetString("UserId"));
            var response = await _httpClient.PutAsJsonAsync<ChangePassword_VM>("https://localhost:7141/api/user/change-password/",changePassword);
            if (response.IsSuccessStatusCode)
            {
                Message = "success";
                
            }
            else
            {
                Message= "error";
            }
        }

    }
}
