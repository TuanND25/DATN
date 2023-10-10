using DATN_Shared.Models;

namespace DATN_API.Service_IService.IServices
{
    public interface IProductItemServices
    {
        public Task<ProductItems> AddProductItem(ProductItems item);
        public Task<ProductItems> UpdateProductItem(ProductItems item);
        public Task<bool> DeleteProductItem(Guid Id);
        public Task<List<ProductItems>> GetAllProductItems();
        public Task<ProductItems> GetAllProductItemById(Guid Id);
        public Task<ProductItems> GetAllProductItemByProduct(Guid ProductId);
    }
}
