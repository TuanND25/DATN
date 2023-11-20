using DATN_Client.Areas.Admin.Controllers;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;

namespace DATN_Client.Areas.Admin.Components
{
    public partial class Promotions
    {
        HttpClient _httpClient = new HttpClient();
        [Inject] NavigationManager _navigationManager { get; set; }

        //[Inject] PromotionController _navPromotion { get; set; }

        List<Promotions_VM> _lstPromotion = new List<Promotions_VM>();
        public static Promotions_VM _promotion_VM = new Promotions_VM();
        private int selectedValue = 0;
        private int selectedSort = 0;
        private int statusValue;
        private DateTime StartDateValue = new DateTime(2000, 1, 1);
        private DateTime EndDateValue = new DateTime(2000, 1, 1);
        private string? _promotionName = null;

        protected override async Task OnInitializedAsync()
        {
            _lstPromotion = await _httpClient.GetFromJsonAsync<List<Promotions_VM>>("https://localhost:7141/api/promotion");
        }

        public async Task NavigationAddPromotion()
        {
            _navigationManager.NavigateTo("https://localhost:7075/Admin/Promotion/Add", true);
        }


        public async Task NavigationUpdatePromotion(Promotions_VM promotionVM)
        {
            _promotion_VM = promotionVM;
            _navigationManager.NavigateTo("https://localhost:7075/Admin/Promotion/Update", true);
        }
        public async Task DeletePromotion(Promotions_VM promotionVM)
        {
            _promotion_VM = promotionVM;
            _promotion_VM.Status = 0;
            var a = await _httpClient.PutAsJsonAsync<Promotions_VM>("https://localhost:7141/api/promotion/update", _promotion_VM);
            
            var d = await _httpClient.GetFromJsonAsync<List<ProductItem_VM>>($"https://localhost:7141/api/productitem/ProductItem_By_PromotionId/{promotionVM.Id}");
            foreach (var item in d)
            {
                var productItem = await _httpClient.GetFromJsonAsync<ProductItem_VM>($"https://localhost:7141/api/productitem/get_all_productitem_byID/{item}");
                productItem.PriceAfterReduction = productItem.CostPrice;
                var t = await _httpClient.PutAsJsonAsync("https://localhost:7141/api/productitem/update_productitem", productItem);
            }
            _navigationManager.NavigateTo("https://localhost:7075/Admin/Promotion", true);
        }
        public async Task Search()
        {
            _lstPromotion = await _httpClient.GetFromJsonAsync<List<Promotions_VM>>("https://localhost:7141/api/promotion");
            _lstPromotion = _lstPromotion.Where(x => x.Name == null || x.Name == string.Empty || x.Name.ToLower().Contains(_promotionName.ToLower())).ToList();
        }
        // status 0 1 
        public async Task Loc()
        {
            _lstPromotion = await _httpClient.GetFromJsonAsync<List<Promotions_VM>>("https://localhost:7141/api/promotion");

            if (selectedValue == 1)
            {
                statusValue = 1;
            }
            else if (selectedValue == 2)
            {
                statusValue = 0;
            }
            else
            {
                statusValue = 3; // selectValue = 0;
            }

            if (selectedSort == 0)
            {
                _lstPromotion = _lstPromotion.Where(x => (statusValue == 3 || x.Status == statusValue) && (StartDateValue == new DateTime(2000, 1, 1) || x.StartDate >= StartDateValue) && (EndDateValue == new DateTime(2000, 1, 1) || x.EndDate <= EndDateValue) && (selectedSort == 0 || selectedSort == 1 || selectedSort == 2)).ToList();
            }
            else if (selectedSort == 1)
            {
                _lstPromotion = _lstPromotion.Where(x => (statusValue == 3 || x.Status == statusValue) && (StartDateValue == new DateTime(2000, 1, 1) || x.StartDate >= StartDateValue) && (EndDateValue == new DateTime(2000, 1, 1) || x.EndDate <= EndDateValue)).OrderByDescending(c => c.Percent).ToList();
            }
            else if (selectedSort == 2)
            {
                _lstPromotion = _lstPromotion.Where(x => (statusValue == 3 || x.Status == statusValue) && (StartDateValue == new DateTime(2000, 1, 1) || x.StartDate >= StartDateValue) && (EndDateValue == new DateTime(2000, 1, 1) || x.EndDate <= EndDateValue)).OrderBy(c => c.Percent).ToList();
            }
        }

    }
}
