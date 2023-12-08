using DATN_Client.Areas.Admin.Controllers;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;

namespace DATN_Client.Areas.Admin.Components
{
    public partial class ProductDetailCRUD
    {
        HttpClient _httpClient = new();
        [Inject] NavigationManager _navi {  get; set; }
        private List<ProductItem_Show_VM>? _lst_pri = new ();

        protected override async Task OnInitializedAsync()
        {
            _lst_pri = await _httpClient.GetFromJsonAsync<List<ProductItem_Show_VM>>($"https://localhost:7141/api/productitem/get_all_productitem_byProduct/{ProductController._productID}");
        }

        private void BackToManager()
        {
            _navi.NavigateTo("/product-manager",true);
        }
    }
}
