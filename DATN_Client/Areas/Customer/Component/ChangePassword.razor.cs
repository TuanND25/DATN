using Blazored.SessionStorage;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;

namespace DATN_Client.Areas.Customer.Component
{
    public partial class ChangePassword
    {
        HttpClient _httpClient  = new HttpClient(); 
        public ChangePassword_VM changePassword = new ChangePassword_VM();
        public string Message { get; set; } = string.Empty;
        [Inject] Blazored.SessionStorage.ISessionStorageService _SessionStorageService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        public async Task ChangePasswordMethor()
        {
            changePassword.UserId = Guid.Parse(await _SessionStorageService.GetItemAsStringAsync("session"));
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
