using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DATN_API.Data;
using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DATN_API.Service_IService.Services
{
    public class LoginServices : ILoginServices
    {
        private readonly UserManager<User> _userManager;

        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public LoginServices(UserManager<User> userManager, IConfiguration configuration, ApplicationDbContext context)
        {
            _userManager = userManager;

            _configuration = configuration;
            _context = context;
        }

        public async Task<ResponseMess> LoginAsync(LoginUser userLogin)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p =>p.UserName==userLogin.UserName);
			if (user == null)
			{
				return new ResponseMess
				{
					IsSuccess = false,
					Message = "Tên đăng nhập không tồn tại",
					StatusCode = 400
				};
			}
			if (user.Status==3)
            {
				return new ResponseMess
				{
					IsSuccess = false,
					Message = "Bạn chưa xác thực OTP, hãy đăng ký lại",
					StatusCode = 400
				};
			}
       
            else if (!await _userManager.CheckPasswordAsync(user, userLogin.Password))
            {
                return new ResponseMess
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Message = "Tên đăng nhập hoặc mật khẩu sai"
                };
            }
            else
            {
                //tạo danh sách claim của jwt
                var roles = await _userManager.GetRolesAsync(user);
                var claims = new List<Claim>()
                {
                    new Claim("Id",user.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier,user.UserName.ToString()),
                    new Claim(ClaimTypes.Email,user.Email.ToString()),
                    new Claim(ClaimTypes.Role,roles.FirstOrDefault())

                };
                // tạo jwt token
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var token = new JwtSecurityToken(

                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                    );
      
                return new ResponseMess
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "dang nhap thanh cong",
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                };
            }



        }
    }
}
