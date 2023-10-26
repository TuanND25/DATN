using DATN_Shared.Models;

namespace DATN_API.Service_IService.IServices
{
    public interface IPromotionItemServices
    {
        public Task<PromotionsItem> AddPromotionsItem(PromotionsItem item);
        public Task<PromotionsItem> UpdatePromotionsItem(PromotionsItem item);
        public Task<bool> DeletePromotionsItem(Guid Id);
        public Task<List<PromotionsItem>> GetAllPromotionsItemt();
        public Task<List<PromotionsItem>> GetAllPromotionItemById(Guid Id);
        public Task<PromotionsItem> GetPromotionItemById(Guid Id);

        public Task<bool> DeletePromotionItemByProductItemId(Guid Id);
        public Task<bool> DeletePromotionItemByPomotionId(Guid Id);
    }
}
