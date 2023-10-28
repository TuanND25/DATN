using System.Security.Cryptography;
using DATN_API.Data;
using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace DATN_API.Service_IService.Services
{
    public class SignUpServices : ISignUpServices
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ApplicationDbContext _context;
        public SignUpServices(UserManager<User> userManager, RoleManager<Role> roleManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }
        public async Task<Response> SignUpAsync(SignUpUser user)
        {
            if (await _userManager.FindByEmailAsync(user.Email) != null)
            {
                return new Response
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Message = "Email da ton tai"

                };
            }
            else if (await _userManager.FindByNameAsync(user.UserName) != null)
            {
                return new Response
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Message = "UserName da ton tai"

                };
            }
            if (user.Password != user.ConfirmPassword)
            {
                return new Response
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Message = "xac nhan mat khau sai"

                };
            }
            var id = Guid.NewGuid();
            User newUser = new User
            {
                Id = id,
                UserName = user.UserName,
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Sex= user.Sex,               
                Status = 1
            };
            if (await _roleManager.RoleExistsAsync("user"))
            {
                var result = await _userManager.CreateAsync(newUser, user.Password);
                
                if (!result.Succeeded)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        StatusCode = 500,
                        Message = "mat khau khong du dai"

                    };

                }
                await _userManager.AddToRoleAsync(newUser, "user");
                Cart newCart = new Cart()
                {
                    UserId = id,
                    Description = null,
                    Status = 1
                };
                await _context.Carts.AddAsync(newCart);
                await _context.SaveChangesAsync();
                return new Response
                {
                    IsSuccess = true,
                    StatusCode = 201,
                    Message = "Register successfully!"
                };
            }
            else
            {
                return new Response
                {
                    IsSuccess = true,
                    StatusCode = 500,
                    Message = "co gi do sai"

                };
            }

        }


    }
}
