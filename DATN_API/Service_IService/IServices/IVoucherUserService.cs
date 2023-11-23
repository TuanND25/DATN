using DATN_Shared.Models;

namespace DATN_API.Service_IService.IServices
{
	public interface IVoucherUserService
	{
		public Task<VoucherUser> PostVoucherUser(VoucherUser voucher);
		public Task<VoucherUser> PutVoucherUser(VoucherUser voucher);
		public Task<VoucherUser> DeleteVoucherUser(Guid Id);
		public Task<VoucherUser> GetVoucherUserById(Guid Id);
		public Task<IEnumerable<VoucherUser>> GetVoucherUserByUserId(Guid UserId);
		public Task<IEnumerable<VoucherUser>> GetAllVoucherUser();
	}
}
