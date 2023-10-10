using DATN_Shared.Models;

namespace DATN_API.Service_IService.IServices
{
    public interface IPromotionProductItemServices
    {
        public Task<PromotionsProduct> AddPromotionsProduct(PromotionsProduct item);
        public Task<PromotionsProduct> UpdatePromotionsProduct(PromotionsProduct item);
        public Task<bool> DeletePromotionsProduct(Guid Id);
        public Task<List<PromotionsProduct>> GetAllPromotionProduct();
        public Task<PromotionsProduct> GetAllPromotionsProductById(Guid Id);
        public Task<PromotionsProduct> GetAllPromotionsProductByPromotin(Guid PromotionId);
    }
}
