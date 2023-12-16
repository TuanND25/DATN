using DATN_Shared.Models;
using DATN_Shared.ViewModel;

namespace DATN_API.Service_IService.IServices
{
	public interface IUserService
	{
		public Task<ResponseMess> UpdateStatusUser(User_VM user);
		public Task<ResponseMess> UpdateUser(AddUserByAdmin updateUser);
		public Task<ResponseMess> AddEmployeeOrAdmin(AddUserByAdmin user);
		public Task<ResponseMess> ChangePassword(ChangePassword_VM changePassword);
		public Task<User> GetUserByUserName(string username);
		public Task<User> GetUserById(Guid Id);
		public Task<ResponseMess> UpdateUserCustomer(User_VM user_VM);
	}
}
