using DATN_Shared.Models;
using DATN_Shared.ViewModel;

namespace DATN_API.Service_IService.IServices
{
	public interface IUserService
	{
		public Task<User> UpdateUser(User_VM user);
		

		public Task<List<User>> GetUserByUserName(string username);
	}
}
