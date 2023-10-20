using DATN_Shared.Models;

namespace DATN_API.Service_IService.IServices
{
	public interface IUserService
	{
		public Task<User> UpdateUser(User user);

	}
}
