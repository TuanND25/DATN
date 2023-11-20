using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;

namespace DATN_Client.Areas.Admin.Components
{
    public partial class Categories
    {
        HttpClient _httpClient = new HttpClient();
        public Categories_VM cate_VM = new Categories_VM();
        [Inject] NavigationManager navigationManager { get; set; }
        List<Categories_VM> cate = new List<Categories_VM>();
        public string Message { get; set; } = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            cate = await _httpClient.GetFromJsonAsync<List<Categories_VM>>("https://localhost:7141/api/Categories");
        }
        public async Task AddCate()
        {
            cate_VM.Id = Guid.NewGuid();

            await _httpClient.PostAsJsonAsync<Categories_VM>("https://localhost:7141/api/Categories/PostCategory", cate_VM);
            navigationManager.NavigateTo("https://localhost:7075/Admin/Categories", true);


        }
        public async Task UpdateCate(Categories_VM cate)
        {
            await _httpClient.PutAsJsonAsync<Categories_VM>("https://localhost:7141/api/Categories/PutCategory", cate);
            navigationManager.NavigateTo("https://localhost:7075/Admin/Categories", true);
        }
        public async void DeleteCate(Guid Id)
        {
            await _httpClient.DeleteAsync("https://localhost:7141/api/Categories/DeleteCategory/" + Id);
            navigationManager.NavigateTo("https://localhost:7075/Admin/Categories", true);
        }
        public async Task LoadForm(Categories_VM rvm)
        {
            cate_VM.Id = rvm.Id;
            cate_VM.Name = rvm.Name;
            cate_VM.Status = rvm.Status;
        }

    }
}
