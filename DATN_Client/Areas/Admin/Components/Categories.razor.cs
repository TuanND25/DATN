using DATN_Client.Areas.Customer.Component;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;

namespace DATN_Client.Areas.Admin.Components
{
    public partial class Categories
    {
        HttpClient _httpClient = new HttpClient();
        public Categories_VM cate_VM = new Categories_VM();
        [Inject] NavigationManager navigationManager { get; set; }
        [Inject] private Blazored.Toast.Services.IToastService _toastService { get; set; } // Khai báo khi cần gọi ở code-behind
        List<Categories_VM> _lstcate = new List<Categories_VM>();
        public string Message { get; set; } = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            if (Login.Roleuser != "Admin")
            {
                navigationManager.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            _lstcate = await _httpClient.GetFromJsonAsync<List<Categories_VM>>("https://localhost:7141/api/Categories");
        }
        public async Task AddCate()
        {
            if (Login.Roleuser != "Admin")
            {
                navigationManager.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            cate_VM.Id = Guid.NewGuid();
            if (!string.IsNullOrEmpty(cate_VM.Name))
            {
                if (_lstcate.Any(c => c.Name == cate_VM.Name))
                {
                    _toastService.ShowError("Thể loại đã tồn tại");
                    return;
                }
            }
            if (string.IsNullOrEmpty(cate_VM.Name)) return;
            await _httpClient.PostAsJsonAsync<Categories_VM>("https://localhost:7141/api/Categories/PostCategory", cate_VM);
            navigationManager.NavigateTo("/category-management", true);
        }
        public async Task UpdateCate(Categories_VM cate)
        {
            if (Login.Roleuser != "Admin")
            {
                navigationManager.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            if (!string.IsNullOrEmpty(cate_VM.Name))
            {
                if (_lstcate.Any(c => c.Name == cate_VM.Name))
                {
                    _toastService.ShowError("Thể loại đã tồn tại");
                    return;
                }
            }
            if (string.IsNullOrEmpty(cate_VM.Name)) return;
            cate.TenKhongDau = "";
            await _httpClient.PutAsJsonAsync<Categories_VM>("https://localhost:7141/api/Categories/PutCategory", cate);
            navigationManager.NavigateTo("/category-management", true);
        }
        public async void DeleteCate(Guid Id)
        {
            if (Login.Roleuser != "Admin")
            {
                navigationManager.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            await _httpClient.DeleteAsync("https://localhost:7141/api/Categories/DeleteCategory/" + Id);
            navigationManager.NavigateTo("/category-management", true);
        }
        public async Task LoadForm(Categories_VM rvm)
        {
            if (Login.Roleuser != "Admin")
            {
                navigationManager.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            cate_VM.Id = rvm.Id;
            cate_VM.Name = rvm.Name;
            cate_VM.Status = rvm.Status;
        }

    }
}
