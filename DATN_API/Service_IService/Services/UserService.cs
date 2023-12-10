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

		public async Task<ResponseMess> UpdateStatusUser(User_VM user)
		{
			var userupdate = _context.Users.Where(p => p.Id == user.Id).FirstOrDefault();
			if (userupdate == null)
			{
				return new ResponseMess {
					IsSuccess = false,
					Message = "Không có người dùng nào để cập nhật thông tin",
					StatusCode = 400,
					Token = null
				};
			}
			else
			{
				userupdate.Status = user.Status;
				_context.Users.Update(userupdate);
				await _context.SaveChangesAsync();
				return new ResponseMess {
					IsSuccess = true,
					Message = "Thay đổi trạng thái thành công",
					StatusCode = 200,
					Token = null
				};
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
						Message = "Thay đổi mật khẩu thành công",
						StatusCode = 200,
						Token = null,

					};
				}
				else
				{
					return new ResponseMess
					{
						IsSuccess = true,
						Message = "Thay đổi mật khẩu thành công",
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
					Message = "Không được để trống",
					StatusCode = 400,
					Token = null,

				};
			}
		}

		public async Task<ResponseMess> UpdateUser(AddUserByAdmin updateUser)
		{
			var user = await _context.Users.FindAsync(updateUser.id);
			if (user != null)
			{
				user.Name = updateUser.name;
				user.Email = updateUser.email;
				user.PhoneNumber = updateUser.phonenumber;
				user.Sex = updateUser.sex;
				user.Status = updateUser.status;
				
				
				var code =await _userManager.GeneratePasswordResetTokenAsync(user);
				await _userManager.ResetPasswordAsync(user,code,updateUser.password);
				
				
				_context.Users.Update(user);
				await _context.SaveChangesAsync();
				return new ResponseMess
				{
					IsSuccess = true,
					StatusCode = 200,
					Message = "Cập nhận thông tin người dùng thành công",
					Token = null
				};
			}
			else
			{
				return new ResponseMess
				{
					IsSuccess = false,
					StatusCode = 400,
					Message = "Cập nhập thông tin người dùng thất bại",
					Token = null
				};
			}

		}

        public async Task<ResponseMess> AddEmployeeOrAdmin(AddUserByAdmin user)
        {
			if (await _userManager.Users.FirstOrDefaultAsync(p => p.PhoneNumber == user.phonenumber && p.Status == 1) != null)
			{
				return new ResponseMess
				{
					IsSuccess = false,
					StatusCode = 403,
					Message = "Số điện thoại đã tồn tại"

				};
			}
			if (await _userManager.FindByEmailAsync(user.email) != null)
            {
                return new ResponseMess
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Message = "Email đã tồn tại"

                };
            }
            else if (await _userManager.FindByNameAsync(user.username) != null)
            {
                return new ResponseMess
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Message = "Tên đăng nhập đã tồn tại"

                };
            }
			var id = Guid.NewGuid();
            User newUser = new User
            {
				Id = id,
                UserName = user.username,
                Email = user.email,
                Name = user.name,
				PhoneNumber= user.phonenumber,
				Sex= user.sex,
				Status =user.status
				
            };
            if (await _roleManager.RoleExistsAsync(user.role))
            {
                var result = await _userManager.CreateAsync(newUser, user.password);

                if (!result.Succeeded)
                {
                    return new ResponseMess
                    {
                        IsSuccess = false,
                        StatusCode = 500,
                        Message = "Mật khẩu không đủ dài"

                    };

                }
                await _userManager.AddToRoleAsync(newUser,user.role);
				Cart newCart = new Cart()
				{
					UserId = id,
					Description = null,
					Status = 1
				};
				await _context.Carts.AddAsync(newCart);
				await _context.SaveChangesAsync();

				ConsumerPoint consumerPoint = new ConsumerPoint()
				{
					UserID = id,
					Point = string.Empty,
					Status = 1,

				};
				await _context.ConsumerPoints.AddAsync(consumerPoint);
				await _context.SaveChangesAsync();
				return new ResponseMess
                {
                    IsSuccess = true,
                    StatusCode = 201,
                    Message = "Thêm người dùng thành công"
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
