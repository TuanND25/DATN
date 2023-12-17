using DATN_Client.Areas.Customer.Component;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;
namespace DATN_Client.Areas.Admin.Components
{
    public partial class Color
    {
        HttpClient _httpClient = new HttpClient();
        public Color_VM color_VM = new Color_VM();
        [Inject] NavigationManager navigationManager { get; set; }
        [Inject] private Blazored.Toast.Services.IToastService _toastService { get; set; } // Khai báo khi cần gọi ở code-behind
        List<Color_VM> _lstcolor = new List<Color_VM>();
        public string Message { get; set; } = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            if (Login.Roleuser != "Admin")
            {
                navigationManager.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            _lstcolor = await _httpClient.GetFromJsonAsync<List<Color_VM>>("https://localhost:7141/api/Color/get_color");
        }
        public async Task AddColor()
        {
            if (Login.Roleuser != "Admin")
            {
                navigationManager.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            color_VM.Id = Guid.NewGuid();
            if (!string.IsNullOrEmpty(color_VM.Name))
            {
                if (_lstcolor.Any(c => c.Name == color_VM.Name))
                {
                    _toastService.ShowError("Màu sắc đã tồn tại");
                    return;
                }
            }
            if (string.IsNullOrEmpty(color_VM.Name)) return;
            color_VM.Name = color_VM.Name.Trim();
            await _httpClient.PostAsJsonAsync<Color_VM>("https://localhost:7141/api/Color/PostColor", color_VM);
            navigationManager.NavigateTo("/color-management", true);


        }
        public async Task UpdateColor(Color_VM color)
        {
            if (Login.Roleuser != "Admin")
            {
                navigationManager.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            if (!string.IsNullOrEmpty(color_VM.Name))
            {
                if (_lstcolor.Any(c => c.Name == color_VM.Name) && !_lstcolor.Any(c=>c.Id==color.Id))
                {
                    _toastService.ShowError("Màu sắc đã tồn tại");
                    return;
                }
            }
            if (string.IsNullOrEmpty(color_VM.Name)) return;
            color.Name = color_VM.Name.Trim();
            await _httpClient.PutAsJsonAsync<Color_VM>("https://localhost:7141/api/Color/PutColoe", color);
            navigationManager.NavigateTo("/color-management", true);
        }
        public async void DeleteColor(Guid Id)
        {
            if (Login.Roleuser != "Admin")
            {
                navigationManager.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            await _httpClient.DeleteAsync("https://localhost:7141/api/Color/DeleteColor/" + Id);
            navigationManager.NavigateTo("/color-management", true);
        }
        public async Task LoadForm(Color_VM rvm)
        {
            if (Login.Roleuser != "Admin")
            {
                navigationManager.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            color_VM.Id = rvm.Id;
            color_VM.Name = rvm.Name;
            color_VM.Status = rvm.Status;
        }

    }
}
