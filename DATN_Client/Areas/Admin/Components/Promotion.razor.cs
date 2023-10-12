using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;

namespace DATN_Client.Areas.Admin.Components
{
    public partial class Promotion
    {
        HttpClient _httpClient=new HttpClient();
        [Inject] NavigationManager _navigationManager { get; set; }

        List<Promotions_VM> _lstPromotion = new List<Promotions_VM>();

        protected override async Task OnInitializedAsync()
        {
            _lstPromotion = await _httpClient.GetFromJsonAsync<List<Promotions_VM>>("https://localhost:7141/api/promotion/get_all_promotion");
        }
    }
}
