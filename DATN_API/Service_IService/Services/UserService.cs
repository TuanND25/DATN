using DATN_API.Data;
using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DATN_API.Service_IService.Services
{
	public class UserService : IUserService
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<User> userManager;
		public UserService(ApplicationDbContext context)
		{
			_context= context;
		}

	
		public async Task<List<User>> GetUserByUserName(string username)
		{
			return await userManager.Users.Where(u=>u.UserName.Contains(username)).ToListAsync();
		}

		public async Task<User> UpdateUser(User_VM user)
		{
			var userupdate =  _context.Users.Where(p => p.Id == user.Id).FirstOrDefault();
			if (userupdate == null)
			{
				return null;
			}
			else
			{
                userupdate.Status = user.Status;
				 _context.Users.Update(userupdate);
                await _context.SaveChangesAsync();
				return userupdate;
			}
		}
	}
}
