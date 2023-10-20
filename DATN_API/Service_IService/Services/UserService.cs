using DATN_API.Data;
using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
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
		

		public async Task<User> UpdateUser(User user)
		{
			var userupdate = await _context.Users.FirstOrDefaultAsync(p => p.Id == user.Id);
			if (userupdate != null)
			{
				return null;
			}
			else
			{
			    await userManager.UpdateAsync(user);
				return user;
			}
		}
	}
}
