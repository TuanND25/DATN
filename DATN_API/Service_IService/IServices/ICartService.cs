using DATN_Shared.Models;

namespace DATN_API.Service_IService.IServices
{
	public interface ICartService
	{
		public Task<Cart> AddCart(Cart Cart);
		public Task<Cart> UpdateCart(Cart Cart);
		public Task<bool> DeleteCart(Guid UserId);
		public Task<List<Cart>> GetAllCart();
		public Task<List<Cart>> GetAllCartByUserId(Guid UserId);
		public Task<Cart> GetCartByUserId(Guid UserId);
	}
}
