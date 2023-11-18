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
        List<Size_VM> size = new List<Size_VM>();
        public string Message { get; set; } = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            size = await _httpClient.GetFromJsonAsync<List<Size_VM>>("https://localhost:7141/api/Size/get_size");
        }
        public async Task AddSize()
        {
            size_VM.Id = Guid.NewGuid();

            await _httpClient.PostAsJsonAsync<Size_VM>("https://localhost:7141/api/Size/PostSize", size_VM);
            navigationManager.NavigateTo("https://localhost:7075/Admin/Size", true);


        }
        public async Task UpdateSize(Size_VM size)
        {
            await _httpClient.PutAsJsonAsync<Size_VM>("https://localhost:7141/api/Size/PutSize", size);
            navigationManager.NavigateTo("https://localhost:7075/Admin/Size", true);
        }
        public async void DeleteSize(Guid Id)
        {
            await _httpClient.DeleteAsync("https://localhost:7141/api/Size/DeleteSize/" + Id);
            navigationManager.NavigateTo("https://localhost:7075/Admin/Size", true);
        }
        public async Task LoadForm(Size_VM rvm)
        {
            size_VM.Id = rvm.Id;
            size_VM.Name = rvm.Name;
            size_VM.Status = rvm.Status;
        }

    }
}
