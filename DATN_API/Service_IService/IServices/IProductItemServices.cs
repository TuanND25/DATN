using DATN_Shared.Models;
using DATN_Shared.ViewModel;

namespace DATN_API.Service_IService.IServices
{
    public interface IProductItemServices
    {
        public Task<ProductItems> AddProductItem(ProductItems item);
        public Task<ProductItems> UpdateProductItem(ProductItems item);
        public Task<bool> DeleteProductItem(Guid Id);
        public Task<List<ProductItems>> GetAllProductItems();
        public Task<List<ProductItem_Show_VM>> GetAllProductItems_Show();
        public Task<ProductItems> GetAllProductItemById(Guid Id);
        public Task<ProductItems> GetAllProductItemByProduct(Guid ProductId);
        public Task<List<ProductItem_Show_VM>> GetAllProductItemPromotionItem_Show(Guid Id);
    }
}
