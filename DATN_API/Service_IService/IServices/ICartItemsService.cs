using DATN_Shared.Models;

namespace DATN_API.Service_IService.IServices
{
	public interface ICartItemsService
	{
		public Task<CartItems> AddCartItems(CartItems CartItems);
		public Task<CartItems> UpdateCartItems(CartItems CartItems);
		public Task<bool> DeleteCartItems(Guid Id);
		public Task<List<CartItems>> GetAllCartItems();
		public Task<List<CartItems>> GetAllCartItemsByUserId(Guid UserID);
	}
}
