using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;

namespace DATN_Client.Areas.Admin.Components
{
    public partial class UpdatePromotion
    {
        HttpClient _httpClient = new HttpClient();
        [Inject] NavigationManager _navigationManager { get; set; }
        List<Products_VM> _lstProduct = new List<Products_VM>();
        List<ProductItem_Show_VM> _lstProductItem = new List<ProductItem_Show_VM>();
        List<Image_VM> _lstImg = new List<Image_VM>();
        Promotions_VM _promotion = new Promotions_VM();
        List<ProductItem_Show_VM> _productItemByPromotion = new List<ProductItem_Show_VM>();
        PromotionItem_VM _promotionItem = new PromotionItem_VM();
        List<Guid> _lstProductSelect = new List<Guid>();
        List<Guid> _lstProductItemSelect = new List<Guid>();
        ProductItem_VM _PI_VM = new ProductItem_VM();
        Categories_VM _Cate_VM = new Categories_VM();
        Color_VM _C_VM = new Color_VM();
        Size_VM _S_VM = new Size_VM();
        List<Color_VM> _lstC = new List<Color_VM>();
        List<Size_VM> _lstS = new List<Size_VM>();
        List<Categories_VM> _lstCate = new List<Categories_VM>();
        ProductItem_Show_VM _PM_S_VM = new ProductItem_Show_VM();

        List<PromotionItem_VM> _lstPromotionItem = new List<PromotionItem_VM>();
        List<Products_VM> _lstProductReturn = new List<Products_VM>();
        List<ProductItem_Show_VM> _lstPrI_show_VM = new List<ProductItem_Show_VM>();

        List<Guid> _lstProductItemSelect_Them = new List<Guid>();
        List<Guid> _lstProductItemSelect_Xoa = new List<Guid>();

        public bool SelectAllCheckboxProductItem { get; set; } = false;
        public bool SelectAllCheckboxProduct = false;

        protected override async Task OnInitializedAsync()
        {
            _lstProduct = await _httpClient.GetFromJsonAsync<List<Products_VM>>("https://localhost:7141/api/product/get_allProduct");
            _lstImg = await _httpClient.GetFromJsonAsync<List<Image_VM>>("https://localhost:7141/api/Image");
            _promotion = Promotions._promotion_VM;

            _lstPromotionItem = await _httpClient.GetFromJsonAsync<List<PromotionItem_VM>>($"https://localhost:7141/api/PromotionItem/PromotionItem_By_Promotion/{_promotion.Id}");
            _lstPrI_show_VM = await _httpClient.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");
            _lstProductItem = await _httpClient.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");


            
            _lstCate = await _httpClient.GetFromJsonAsync<List<Categories_VM>>("https://localhost:7141/api/Categories");
            _lstC = await _httpClient.GetFromJsonAsync<List<Color_VM>>("https://localhost:7141/api/Color");
            _lstS = await _httpClient.GetFromJsonAsync<List<Size_VM>>("https://localhost:7141/api/Size");
            foreach (var a in _lstPromotionItem)
            {
                _lstProductItemSelect.Add(a.ProductItemsId);

            }
            foreach (var a in _lstPrI_show_VM)
            {
                foreach (var b in _lstProductItemSelect)
                {
                    if (a.Id == b)
                    {
                        _lstProductSelect.Add(a.ProductId);
                    }
                }
            }
            _lstProductSelect = _lstProductSelect.Distinct().ToList();
            _lstProductItem = _lstProductItem.Where(p => _lstProductSelect.Contains(p.ProductId)).ToList();

            if (_lstProductItemSelect.Count == _lstProductItem.Select(x => x.Id).ToList().Count && _lstProductItem.Select(x => x.Id).ToList().Count != 0)
            {
                SelectAllCheckboxProductItem = true;
            }
            else
            {
                SelectAllCheckboxProductItem = false;
            }
        }

        public async Task Update()
        {
            var a = await _httpClient.PutAsJsonAsync<Promotions_VM>("https://localhost:7141/api/promotion/Update", _promotion);
            var c = _promotion.Id;
            if (_lstProductItemSelect_Them.Count > 0)
            {
                foreach (var item in _lstProductItemSelect_Them)
                {
                    if (!_lstPromotionItem.Any(x => x.ProductItemsId == item) || !_lstPromotionItem.Any(x => x.PromotionsId == c))
                    {
                        _promotionItem.Id = Guid.NewGuid();
                        _promotionItem.PromotionsId = c;
                        _promotionItem.ProductItemsId = item;
                        _promotionItem.Status = 1;
                        var b = await _httpClient.PostAsJsonAsync("https://localhost:7141/api/PromotionItem/Add", _promotionItem);
                    }
                }
            }
            if (_lstProductItemSelect_Xoa.Count > 0)
            {
                foreach (var item in _lstProductItemSelect_Xoa)
                {
                    var b = await _httpClient.DeleteAsync($"https://localhost:7141/api/PromotionItem/PromotionItemByProductItem/{item}");
                }
            }
            _navigationManager.NavigateTo("https://localhost:7075/Admin/Promotion", true);
        }

        private async Task LoadPromotionItem(Guid Id)
        {
            _productItemByPromotion = await _httpClient.GetFromJsonAsync<List<ProductItem_Show_VM>>($"https://localhost:7141/api/PromotionItem/PromotionItem_By_Promotion/{Id}");
        }

        private async Task ToggleProductSelection(Guid productId)
        {
            _lstProductItem = await _httpClient.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");
            //_lstProductItemSelect.Clear();
            if (_lstProductSelect.Contains(productId))
            {
                _lstProductSelect.Remove(productId);
                _lstProductItemSelect_Xoa.Clear();
                _lstProductItemSelect_Xoa = _lstProductItem.Select(x => x.Id).ToList();
                //_lstProductItemSelect_Them.Clear();
                SelectAllCheckboxProductItem = false;
            }
            else
            {
                foreach (var a in _lstPromotionItem)
                {
                    _lstProductItemSelect.Add(a.ProductItemsId);
                }
                _lstProductItemSelect_Xoa.Clear();
                _lstProductSelect.Add(productId);
            }
            _lstProductItem = _lstProductItem.Where(p => _lstProductSelect.Contains(p.ProductId)).ToList();
            if (_lstProductItemSelect.Count == _lstProductItem.Select(x => x.Id).ToList().Count && _lstProductItem.Select(x => x.Id).ToList().Count != 0)
            {
                SelectAllCheckboxProductItem = true;
            }
            else
            {
                SelectAllCheckboxProductItem = false;
            }

        }

        private async Task ToggleProductItemSelection(Guid productItemId)
        {
            if (_lstProductItemSelect.Contains(productItemId))
            {
                _lstProductItemSelect.Remove(productItemId);
                _lstProductItemSelect_Them.Remove(productItemId);
                _lstProductItemSelect_Xoa.Add(productItemId);
            }
            else
            {
                _lstProductItemSelect.Add(productItemId);
                _lstProductItemSelect_Them.Add(productItemId);
                _lstProductItemSelect_Xoa.Remove(productItemId);
            }
        }

        private void ToggleAllItems(ChangeEventArgs e)
        {

            if ((bool)e.Value == true)
            {
                _lstProductItemSelect = _lstProductItem.Select(x => x.Id).ToList();
                _lstProductItemSelect_Them.Clear();
                _lstProductItemSelect_Them = _lstProductItemSelect;
                _lstProductItemSelect_Xoa.Clear();

            }
            else
            {
                _lstProductItemSelect_Xoa.Clear();
                _lstProductItemSelect_Xoa = _lstProductItem.Select(x => x.Id).ToList();
                _lstProductItemSelect_Them.Clear();
                _lstProductItemSelect.Clear();
            }
        }

        private bool SelectAllChangedProductItem
        {
            get => SelectAllCheckboxProductItem;
            set
            {
                SelectAllCheckboxProductItem = value;
                if (SelectAllCheckboxProductItem)
                {
                    _lstProductItemSelect = _lstProductItem.Select(x => x.Id).Distinct().ToList();
                }
            }
        }
        public async Task LocHangLoat()
        {
            _lstProductItem = await _httpClient.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");

            _lstProductItem = _lstPrI_show_VM.Where(c =>
                                (_PM_S_VM.CategoryName == null ||
                                _PM_S_VM.CategoryName == "0" ||
                                c.CategoryName == _PM_S_VM.CategoryName) &&
                                (_PM_S_VM.SizeName == null ||
                                _PM_S_VM.SizeName == "0" ||
                                c.SizeName == _PM_S_VM.SizeName) &&
                                (_PM_S_VM.ColorName == null ||
                                _PM_S_VM.ColorName == "0" ||
                                c.ColorName == _PM_S_VM.ColorName)).ToList().ToList();
        }
    }
}
