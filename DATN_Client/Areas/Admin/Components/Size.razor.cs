using DATN_Client.Areas.Customer.Component;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace DATN_Client.Areas.Admin.Components
{
    public partial class Size
    {
        HttpClient _httpClient = new HttpClient();
        public Size_VM size_VM = new Size_VM();
        [Inject] NavigationManager navigationManager { get; set; }
		[Inject] private Blazored.Toast.Services.IToastService _toastService { get; set; } // Khai báo khi cần gọi ở code-behind
		List<Size_VM> _lstsize = new List<Size_VM>();
        public string Message { get; set; } = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            if (Login.Roleuser != "Admin")
            {
                navigationManager.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            _lstsize = await _httpClient.GetFromJsonAsync<List<Size_VM>>("https://localhost:7141/api/Size/get_size");
        }
        public async Task AddSize()
        {
            if (Login.Roleuser != "Admin")
            {
                navigationManager.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            size_VM.Id = Guid.NewGuid();
			if (!string.IsNullOrEmpty(size_VM.Name))
			{
				if (_lstsize.Any(c => c.Name == size_VM.Name))
				{
					_toastService.ShowError("Kích thước đã tồn tại");
					return;
				}
			}
			if (string.IsNullOrEmpty(size_VM.Name)) return;
            size_VM.Name = size_VM.Name.Trim();
            await _httpClient.PostAsJsonAsync<Size_VM>("https://localhost:7141/api/Size/PostSize", size_VM);
            navigationManager.NavigateTo("/size-management", true);


        }
        public async Task UpdateSize(Size_VM size)
        {
            if (Login.Roleuser != "Admin")
            {
                navigationManager.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            size_VM.Id = Guid.NewGuid();
            if (!string.IsNullOrEmpty(size_VM.Name))
            {
                if (_lstsize.Any(c => c.Name == size_VM.Name)&& !_lstsize.Any(c => c.Id == size.Id))
                {
                    _toastService.ShowError("Kích thước đã tồn tại");
                    return;
                }
            }
            if (string.IsNullOrEmpty(size_VM.Name)) return;
            size.Name = size.Name.Trim();
            await _httpClient.PutAsJsonAsync<Size_VM>("https://localhost:7141/api/Size/PutSize", size);
            navigationManager.NavigateTo("/size-management", true);
        }
        public async void DeleteSize(Guid Id)
        {
            if (Login.Roleuser != "Admin")
            {
                navigationManager.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            await _httpClient.DeleteAsync("https://localhost:7141/api/Size/DeleteSize/" + Id);
            navigationManager.NavigateTo("/size-management", true);
        }
        public async Task LoadForm(Size_VM rvm)
        {
            if (Login.Roleuser != "Admin")
            {
                navigationManager.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            size_VM.Id = rvm.Id;
            size_VM.Name = rvm.Name;
            size_VM.Status = rvm.Status;
        }

    }
}
