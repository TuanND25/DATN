using DATN_Shared.Models;
using DATN_Shared.ViewModel;

namespace DATN_API.Service_IService.IServices
{
	public interface IUserService
	{
		public Task<User> UpdateStatusUser(User_VM user);
		public Task<ResponseMess> UpdateUser(UpdateUser_VM updateUser);
		public Task<ResponseMess> AddEmployeeOrAdmin(SignUpUser user,string role);
		public Task<ResponseMess> ChangePassword(ChangePassword_VM changePassword);
		public Task<List<User>> GetUserByUserName(string username);
		public Task<User> GetUserById(Guid Id);
	}
}
