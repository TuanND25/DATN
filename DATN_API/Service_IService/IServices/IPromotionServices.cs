using DATN_API.Models;

namespace DATN_API.Service_IService.IServices
{
	public interface IPromotionServices
    {
        public Task<Promotions> AddPromotions(Promotions item);
        public Task<Promotions> UpdatePromotions(Promotions item);
        public Task<bool> DeletePromotions(Guid Id);
        public Task<List<Promotions>> GetAllPromotions();
        public Task<Promotions> GetAllPromotionsById(Guid Id);
        public Task<Promotions> UpdateQuantityPromotion(Guid producitemId);

    }
}
