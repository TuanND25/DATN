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
		private readonly UserManager<User> _userManager;
		public UserService(ApplicationDbContext context, UserManager<User> userManager)
		{
			_context = context;
			_userManager = userManager;
		}


		public async Task<List<User>> GetUserByUserName(string username)
		{
			return await _userManager.Users.Where(u => u.UserName.Contains(username)).ToListAsync();
		}

		public async Task<User> UpdateStatusUser(User_VM user)
		{
			var userupdate = _context.Users.Where(p => p.Id == user.Id).FirstOrDefault();
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


		public async Task<Response> ChangePassword(ChangePassword_VM changePassword)
		{
			var user = await _context.Users.FindAsync(changePassword.UserId);
			if (user != null)
			{
				var result = await _userManager.ChangePasswordAsync(user, changePassword.OldPassword, changePassword.NewPassword);
				if (result.Succeeded)
				{
					return new Response
					{
						IsSuccess = true,
						Message = "Change Password Success",
						StatusCode = 200,
						Token = null,

					};
				}
				else
				{
					return new Response
					{
						IsSuccess = true,
						Message = "Change Password Fail",
						StatusCode = 400,
						Token = null,

					};
				}
			}
			else
			{
				return new Response
				{
					IsSuccess = true,
					Message = "Khong duoc de trong",
					StatusCode = 400,
					Token = null,

				};
			}
		}

		public async Task<Response> UpdateUser(UpdateUser_VM updateUser)
		{
			var user = await _context.Users.FindAsync(updateUser.Id);
			if (user != null)
			{
				user.Name = updateUser.Name;
				user.Email = updateUser.Email;
				user.PhoneNumber = updateUser.PhoneNumber;
				user.Sex = updateUser.Sex;
				_context.Update(user);
				await _context.SaveChangesAsync();
				return new Response
				{
					IsSuccess = true,
					StatusCode = 200,
					Message = "update user success",
					Token = null
				};
			}
			else
			{
				return new Response
				{
					IsSuccess = false,
					StatusCode = 400,
					Message = "update user fail",
					Token = null
				};
			}

		}
	}
}
