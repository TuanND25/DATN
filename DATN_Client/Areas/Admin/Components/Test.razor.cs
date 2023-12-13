//using DATN_Shared.ViewModel;
//using Microsoft.AspNetCore.Components;

//namespace DATN_Client.Areas.Admin.Components
//{
//    public partial class Test
//    {
//        HttpClient _httpClient = new HttpClient();
//        [Inject] NavigationManager _navigationManager { get; set; }
//        List<Products_VM> _lstProduct = new List<Products_VM>();
//        List<ProductItem_Show_VM> _lstProductItem = new List<ProductItem_Show_VM>();
//        List<Image_VM> _lstImg = new List<Image_VM>();
//        Promotions_VM _promotion = new Promotions_VM();
//        ProductItem_VM _PI_VM = new ProductItem_VM();
//        Categories_VM _Cate_VM = new Categories_VM();
//        Color_VM _C_VM = new Color_VM();
//        Size_VM _S_VM = new Size_VM();
//        List<Color_VM> _lstC = new List<Color_VM>();
//        List<Size_VM> _lstS = new List<Size_VM>();
//        List<Categories_VM> _lstCate = new List<Categories_VM>();
//        public DateTime Date { get; set; }
//        public TimeSpan Time { get; set; }
//        //public DateTime StartDateView = DateTime + Time;

//        List<PromotionItem_VM> _lstPromotionItem = new List<PromotionItem_VM>();
//        List<Products_VM> _lstProductReturn = new List<Products_VM>();
//        List<ProductItem_Show_VM> _lstPrI_show_VM = new List<ProductItem_Show_VM>();
//        ProductItem_Show_VM _PM_S_VM = new ProductItem_Show_VM();
//        List<ProductItem_Show_VM> SelectedProductItems { get; set; } = new List<ProductItem_Show_VM>();
//        public Products_VM SelectedProduct =new Products_VM();

//        protected override async Task OnInitializedAsync()
//        {
//            _lstProduct = await _httpClient.GetFromJsonAsync<List<Products_VM>>("https://localhost:7141/api/product/get_allProduct");
//            _lstProductItem = await _httpClient.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");


//            _lstImg = await _httpClient.GetFromJsonAsync<List<Image_VM>>("https://localhost:7141/api/Image");
//            _lstPromotionItem = await _httpClient.GetFromJsonAsync<List<PromotionItem_VM>>($"https://localhost:7141/api/PromotionItem/PromotionItem_By_Promotion/{_promotion.Id}");
//            _lstPrI_show_VM = await _httpClient.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");
//            _lstCate = await _httpClient.GetFromJsonAsync<List<Categories_VM>>("https://localhost:7141/api/Categories");
//            _lstC = await _httpClient.GetFromJsonAsync<List<Color_VM>>("https://localhost:7141/api/Color/get_color");
//            _lstS = await _httpClient.GetFromJsonAsync<List<Size_VM>>("https://localhost:7141/api/Size/get_size");
//        }

//       public async void ToggleAllProducts()
//        {
//            bool allProductsSelected = _lstProduct.All(p => p.IsSelected);
//            foreach (var product in _lstProduct)
//            {
//                product.IsSelected = !allProductsSelected;
//            }

//            if (SelectedProduct != null)
//            {
//                SelectedProduct = null;
//            }
//        }

//        public async void ToggleProduct(Products_VM product)
//        {
//            product.IsSelected = !product.IsSelected;

//            if (product.IsSelected)
//            {
//                SelectedProduct = product;
//            }
//            else if (SelectedProduct == product)
//            {
//                SelectedProduct = null;
//            }
//        }

//        public async void ToggleAllProductItems()
//        {
//            bool allProductItemsSelected = SelectedProductItems.Count == _lstProductItem.Count;
//            foreach (var productItem in _lstProductItem)
//            {
//                productItem.IsSelected = !allProductItemsSelected;
//            }
//        }

//        public async void ToggleProductItem(ProductItem_Show_VM productItem)
//        {
//            productItem.IsSelected = !productItem.IsSelected;
//        }

//        public async void UpdateSelectedProductItems()
//        {
//            SelectedProductItems = _lstProductItem.Where(p => _lstProduct.Any(pr => pr.IsSelected && pr.Id == p.ProductId)).ToList();
//        }


//       public async Task<List<ProductItem_Show_VM>> GetRelatedProductItems(Guid productId)
//        {
//            // Lấy danh sách các mục sản phẩm liên quan đến sản phẩm có productId tương ứng từ API hoặc nguồn dữ liệu khác
//            List<ProductItem_Show_VM> relatedProductItems = _lstProductItem.Where(x=>x.ProductId==productId).ToList();

//            return relatedProductItems;
//        }
//    }
//}
