using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;

namespace DATN_Client.Areas.Admin.Components
{
    public partial class Product
    {
        HttpClient _httpClient = new HttpClient();
        public Products_VM products_VM = new Products_VM();
        [Inject] NavigationManager navigationManager { get; set; }
        List<Products_VM> products = new List<Products_VM>();
        public string Message { get; set; } = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            products = await _httpClient.GetFromJsonAsync<List<Products_VM>>("https://localhost:7141/api/product/get_allProduct");
        }
        public async Task AddProduct()
        {
            products_VM.Id = Guid.NewGuid();

            await _httpClient.PostAsJsonAsync<Products_VM>("https://localhost:7141/api/product/add_product", products_VM);
            navigationManager.NavigateTo("https://localhost:7075/Admin/Product", true);


        }
        public async Task UpdateProduct(Products_VM products)
        {
            await _httpClient.PutAsJsonAsync<Products_VM>("https://localhost:7141/api/product/update_product", products);
            navigationManager.NavigateTo("https://localhost:7075/Admin/Product", true);
        }
        public async void DeleteProduct(Guid Id)
        {
            await _httpClient.DeleteAsync("https://localhost:7141/api/product/delete_product/" + Id);
            navigationManager.NavigateTo("https://localhost:7075/Admin/Product", true);
        }
        public async Task LoadForm(Products_VM rvm)
        {
            products_VM.Id = rvm.Id;
            products_VM.Name = rvm.Name;
            products_VM.Status = rvm.Status;
        }
    }
}
