using DATN_Shared.Models;
using DATN_Shared.ViewModel;

namespace DATN_API.Service_IService.IServices
{
	public interface IUserService
	{
		public Task<User> UpdateStatusUser(User_VM user);
		public Task<Response> UpdateUser(UpdateUser_VM updateUser);

		public Task<Response> AddEmployeeOrAdmin(SignUpUser user,string role);
		public Task<Response> ChangePassword(ChangePassword_VM changePassword);
		public Task<List<User>> GetUserByUserName(string username);
	}
}
