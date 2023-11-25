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
        private readonly RoleManager<Role> _roleManager;
        public UserService(ApplicationDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
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


		public async Task<ResponseMess> ChangePassword(ChangePassword_VM changePassword)
		{
			var user = await _context.Users.FindAsync(changePassword.UserId);
			if (user != null)
			{
				var result = await _userManager.ChangePasswordAsync(user, changePassword.OldPassword, changePassword.NewPassword);
				if (result.Succeeded)
				{
					return new ResponseMess
					{
						IsSuccess = true,
						Message = "Change Password Success",
						StatusCode = 200,
						Token = null,

					};
				}
				else
				{
					return new ResponseMess
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
				return new ResponseMess
				{
					IsSuccess = true,
					Message = "Khong duoc de trong",
					StatusCode = 400,
					Token = null,

				};
			}
		}

		public async Task<ResponseMess> UpdateUser(UpdateUser_VM updateUser)
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
				return new ResponseMess
				{
					IsSuccess = true,
					StatusCode = 200,
					Message = "update user success",
					Token = null
				};
			}
			else
			{
				return new ResponseMess
				{
					IsSuccess = false,
					StatusCode = 400,
					Message = "update user fail",
					Token = null
				};
			}

		}

        public async Task<ResponseMess> AddEmployeeOrAdmin(SignUpUser user, string role)
        {
            if (await _userManager.FindByEmailAsync(user.Email) != null)
            {
                return new ResponseMess
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Message = "Email da ton tai"

                };
            }
            else if (await _userManager.FindByNameAsync(user.UserName) != null)
            {
                return new ResponseMess
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Message = "UserName da ton tai"

                };
            }
            if (user.Password != user.ConfirmPassword)
            {
                return new ResponseMess
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Message = "xac nhan mat khau sai"

                };
            }
            User newUser = new User
            {
                UserName = user.UserName,
                Email = user.Email,
                Name = user.Name,
            };
            if (await _roleManager.RoleExistsAsync(role))
            {
                var result = await _userManager.CreateAsync(newUser, user.Password);

                if (!result.Succeeded)
                {
                    return new ResponseMess
                    {
                        IsSuccess = false,
                        StatusCode = 500,
                        Message = "mat khau khong du dai"

                    };

                }
                await _userManager.AddToRoleAsync(newUser, role);
                return new ResponseMess
                {
                    IsSuccess = true,
                    StatusCode = 201,
                    Message = "Register successfully!"
                };
            }
            else
            {
                return new ResponseMess
                {
                    IsSuccess = true,
                    StatusCode = 500,
                    Message = "co gi do sai"

                };
            }
        }

        public async Task<User> GetUserById(Guid Id)
        {
            var a= await _context.Users.FindAsync(Id);
			return a;
        }
    }
}
