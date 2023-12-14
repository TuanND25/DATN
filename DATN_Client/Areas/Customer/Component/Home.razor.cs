using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Text;

namespace DATN_Client.Areas.Customer.Component
{
    public partial class Home
    {
        List<ProductItem_Show_VM> _lstProductItem = new List<ProductItem_Show_VM>();
        List<PromotionItem_VM> _lstpi_Percent = new List<PromotionItem_VM>();
        List<Products_VM> _lstP = new List<Products_VM>();
        List<Products_VM> _lstP_Tam1 = new();
        List<Categories_VM> _lstCate = new();
        
        [Inject] NavigationManager _navigationManager { get; set; }
        HttpClient _httpClient =new HttpClient();
        private List<Image_Join_ProductItem> _lstImg_PI = new List<Image_Join_ProductItem>();
        [Inject] public IHttpContextAccessor _ihttpcontextaccessor { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var a = _ihttpcontextaccessor.HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(a))
            {
                Login.UserNameShowHome = null;

            }

            _lstProductItem = await _httpClient.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_product_home");
            await ListProduct();
        }
        public async Task ListProduct()
        {
            _lstProductItem = _lstProductItem
            .Where(pi => pi.PriceAfterReduction != null && pi.PriceAfterReduction > 0 && pi.Status != 0)
            .GroupBy(pi => pi.ProductId)
            .Select(g => new ProductItem_Show_VM
            {
                Id = g.Min(pi => pi.Id),
                ProductId = g.Key,
                Name = _lstProductItem.FirstOrDefault(x => x.ProductId == g.Key)?.Name,
                CategoryID = _lstProductItem.FirstOrDefault(x => x.ProductId == g.Key).CategoryID,
                CategoryName = _lstProductItem.FirstOrDefault(x => x.ProductId == g.Key)?.CategoryName,
                PriceAfterReduction = g.Min(pi => pi.PriceAfterReduction),
                CostPrice = g.FirstOrDefault(pi => pi.PriceAfterReduction == null || pi.PriceAfterReduction == g.Min(pi => pi.PriceAfterReduction))?.CostPrice,
                Percent = _lstProductItem.Where(pi => pi.ProductId == g.Key).Max(pi => pi.Percent)
            }).OrderByDescending(x => x.Percent).ToList();

            _lstCate = _lstProductItem.GroupBy(p => p.CategoryID)
                                      .Select(g => new Categories_VM { Id = g.Key, Name = g.First().CategoryName })
                                      .ToList();
            SetTenKhongDau(_lstCate);
        }
        public async void NavProductItem(Guid Id)
        {
            _navigationManager.NavigateTo($"https://localhost:7075/product-detail/{Id}", true);
        }
        public async void NavCategory(string Id)
        {
            _navigationManager.NavigateTo($"https://localhost:7075/all-product/{Id}", true);
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
        private void SetTenKhongDau(List<Categories_VM> lst)
        {
            // Dictionary để theo dõi số lần xuất hiện của mỗi chuỗi TenKhongDau
            Dictionary<string, int> countDictionary = new Dictionary<string, int>();

            foreach (var x in lst)
            {
                string tenKhongDau = XoaDau(x.Name).Replace(" ", "-");

                // Kiểm tra xem chuỗi đã xuất hiện trước đó chưa
                if (countDictionary.ContainsKey(tenKhongDau))
                {
                    // Nếu đã xuất hiện, thì thêm số thứ tự vào cuối chuỗi
                    countDictionary[tenKhongDau]++;
                    x.TenKhongDau = $"{tenKhongDau}-{countDictionary[tenKhongDau]}";
                }
                else
                {
                    // Nếu chưa xuất hiện, thêm vào từ điển với số thứ tự là 1
                    countDictionary.Add(tenKhongDau, 1);
                    x.TenKhongDau = tenKhongDau;
                }
            }
        }
    }
}
