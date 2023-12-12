using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DATN_Client.Areas.Admin.Components
{
	public partial class Product
	{
		private HttpClient _httpClient = new();
		public Products_VM products_VM = new();
		[Inject] private NavigationManager navigationManager { get; set; }
		private List<Products_VM> products = new();
		public string Message { get; set; } = string.Empty;

		protected override async Task OnInitializedAsync()
		{
			products = await _httpClient.GetFromJsonAsync<List<Products_VM>>("https://localhost:7141/api/product/get_allProduct");
		}

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			await _JsRuntime.InvokeVoidAsync("autoResizeTextarea");
		}

		public async Task AddProduct()
		{
			products_VM.Id = Guid.NewGuid();
			await _httpClient.PostAsJsonAsync<Products_VM>("https://localhost:7141/api/product/add_product", products_VM);
			navigationManager.NavigateTo("/product-manager", true);
		}

		public async Task UpdateProduct(Products_VM products)
		{
			await _httpClient.PutAsJsonAsync<Products_VM>("https://localhost:7141/api/product/update_product", products);
			navigationManager.NavigateTo("/product-manager", true);
		}

		public async void DeleteProduct(Guid Id)
		{
			await _httpClient.DeleteAsync("https://localhost:7141/api/product/delete_product/" + Id);
			navigationManager.NavigateTo("/product-manager", true);
		}

		public async Task LoadForm(Products_VM rvm)
		{
			products_VM.Id = rvm.Id;
			products_VM.Name = rvm.Name;
			products_VM.ProductCode = rvm.ProductCode;
			products_VM.Description = rvm.Description;
			products_VM.Status = rvm.Status;
		}

		public void RedirectCRUD(Guid id)
		{
			navigationManager.NavigateTo($"https://localhost:7075/product/{id}", true);
		}
	}
}