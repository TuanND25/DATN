using DATN_Client.Areas.Customer.Component;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DATN_Client.Areas.Admin.Components
{
	public partial class Product
	{
		private HttpClient _httpClient = new();
		public Products_VM products_VM = new();
		[Inject] private NavigationManager navigationManager { get; set; }
		[Inject] Blazored.Toast.Services.IToastService _toastService { get; set; } // Khai báo khi cần gọi ở code-behind
		private List<Products_VM> products = new();
		public string Message { get; set; } = string.Empty;
		private string _check_productCode_byCode { get; set; } = string.Empty;
		private string _textSearch { get; set; } = string.Empty;
		protected override async Task OnInitializedAsync()
		{
            if (Login.Roleuser != "Admin")
            {
                navigationManager.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            products = await _httpClient.GetFromJsonAsync<List<Products_VM>>("https://localhost:7141/api/product/get_allProduct");
		}

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			await _JsRuntime.InvokeVoidAsync("autoResizeTextarea");
		}

		public async Task AddProduct()
		{
            if (Login.Roleuser != "Admin")
            {
                navigationManager.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            if (!string.IsNullOrEmpty(products_VM.ProductCode))
			{
				if ((await _httpClient.GetFromJsonAsync<bool>($"https://localhost:7141/api/product/check_productCode_byCode?productCode={products_VM.ProductCode}") == true))
				{
					_toastService.ShowError("Mã sản phẩm đã tồn tại");
					return;
				}
			}
			if (string.IsNullOrEmpty(products_VM.Name) || string.IsNullOrEmpty(products_VM.ProductCode)) return;
			products_VM.Id = Guid.NewGuid();
			await _httpClient.PostAsJsonAsync<Products_VM>("https://localhost:7141/api/product/add_product", products_VM);
			navigationManager.NavigateTo("/product-manager", true);
		}

		public async Task UpdateProduct(Products_VM products)
		{
            if (Login.Roleuser != "Admin")
            {
                navigationManager.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            if (!string.IsNullOrEmpty(products_VM.ProductCode))
			{
				if ((await _httpClient.GetFromJsonAsync<bool>($"https://localhost:7141/api/product/check_productCode_byCode?productCode={products_VM.ProductCode}") == true))
				{
					_toastService.ShowError("Mã sản phẩm đã tồn tại");
					return;
				}
			}
			if (string.IsNullOrEmpty(products_VM.Name) || string.IsNullOrEmpty(products_VM.ProductCode)) return;
			products_VM.Id = Guid.NewGuid();
			await _httpClient.PutAsJsonAsync<Products_VM>("https://localhost:7141/api/product/update_product", products);
			navigationManager.NavigateTo("/product-manager", true);
		}

		public async void DeleteProduct(Guid Id)
		{
            if (Login.Roleuser != "Admin")
            {
                navigationManager.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            await _httpClient.DeleteAsync("https://localhost:7141/api/product/delete_product/" + Id);
			navigationManager.NavigateTo("/product-manager", true);
		}

		public async Task LoadForm(Products_VM rvm)
		{
            if (Login.Roleuser != "Admin")
            {
                navigationManager.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            products_VM.Id = rvm.Id;
			products_VM.Name = rvm.Name;
			products_VM.ProductCode = rvm.ProductCode;
			products_VM.Description = rvm.Description;
			products_VM.Status = rvm.Status;
		}

		public void RedirectCRUD(Guid id)
		{
            if (Login.Roleuser != "Admin")
            {
                navigationManager.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            navigationManager.NavigateTo($"/product-manager/product-detail?id={id}", true);
		}

		private async Task TimKiem()
		{
            if (Login.Roleuser != "Admin")
            {
                navigationManager.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            products = await _httpClient.GetFromJsonAsync<List<Products_VM>>("https://localhost:7141/api/product/get_allProduct");
			_textSearch = _textSearch.ToLower();
			if (_textSearch != XoaDau(_textSearch))
			{
				products = products.Where(c =>
							string.IsNullOrEmpty(_textSearch) ||
							c.Name.Trim().ToLower().Contains(_textSearch.Trim()) ||
							c.ProductCode.ToLower().Contains(_textSearch.Trim())).ToList();
			}
			else
			{
				products = products.Where(c =>
							string.IsNullOrEmpty(_textSearch) ||
							XoaDau(c.Name.Trim()).Contains(XoaDau(_textSearch.Trim())) ||
							c.ProductCode.ToLower().Contains(_textSearch.Trim())).ToList();
			}
		}

		private string XoaDau(string text)
		{
			string normalizedString = text.Normalize(NormalizationForm.FormD);
			StringBuilder stringBuilder = new StringBuilder();

			foreach (char c in normalizedString)
			{
				UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
				if (unicodeCategory != UnicodeCategory.NonSpacingMark)
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString().Normalize(NormalizationForm.FormC).ToLower();
		}
	}
}