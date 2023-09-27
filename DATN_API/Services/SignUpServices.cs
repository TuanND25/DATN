
using DATN.IServices;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace DATN.Services
{
    public class SignUpServices : ISignUpServices
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public SignUpServices(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<Response> SignUpAsync(SignUpUser user, string role)
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
            User newUser = new User
            {
                UserName = user.UserName,
                Email = user.Email,
                Name= user.Name,
            };
            if (await _roleManager.RoleExistsAsync(role))
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
                await _userManager.AddToRoleAsync(newUser, role);
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
