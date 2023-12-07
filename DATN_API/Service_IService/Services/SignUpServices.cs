using System.Security.Cryptography;
using DATN_API.Data;
using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Twilio;
using Twilio.Http;
using Twilio.Rest.Api.V2010.Account;

namespace DATN_API.Service_IService.Services
{
    public class SignUpServices : ISignUpServices
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ApplicationDbContext _context;
		private readonly TwilioSettings _twilioSettings = new TwilioSettings();
		public SignUpServices(UserManager<User> userManager, RoleManager<Role> roleManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

		public string RandomOTP()
		{

			const string characters = "0123456789";
			Random random = new Random();
			char[] result = new char[6];

			for (int i = 0; i < 6; i++)
			{
				result[i] = characters[random.Next(characters.Length)];
			}

			return new string(result);
		}

	

		public async Task<ResponseMess> SignUpAsync(SignUpUser user)
        {
			if (user.Password != user.ConfirmPassword)
			{
				return new ResponseMess
				{
					IsSuccess = false,
					StatusCode = 403,
					Message = "Xác nhận mật khẩu sai"

				};
			}
			if (await  _userManager.Users.FirstOrDefaultAsync(p=>p.PhoneNumber == user.PhoneNumber && p.Status == 3 )!=null)
            {
                var usertemp = await _context.Users.FirstOrDefaultAsync(p => p.PhoneNumber == user.PhoneNumber && p.Status == 3);
			 	var code = await _userManager.GeneratePasswordResetTokenAsync(usertemp);
                await _userManager.ResetPasswordAsync(usertemp, code, user.Password);
                usertemp.UserName = user.UserName;
                usertemp.Name = user.Name;
                usertemp.Sex = user.Sex;
                usertemp.Email = user.Email;
                usertemp.OTP = RandomOTP();
                usertemp.TokenExpires= DateTime.Now;
                _context.Users.Update(usertemp);
                 await _context.SaveChangesAsync();
				usertemp.PhoneNumber = "+84" + usertemp.PhoneNumber.Substring(1);
				await SendSmsOTP(usertemp.PhoneNumber, "Hạn xác thực đăng kí là 5 phút,OTP xác thực đăng kí :" + usertemp.OTP);
                return new ResponseMess
                {
                    IsSuccess = true,
                    StatusCode= 200,
                    Message= "success"
                };


			}
            if(await _userManager.Users.FirstOrDefaultAsync(p=>p.PhoneNumber== user.PhoneNumber && p.Status==1) != null )
            {
                return new ResponseMess
                {
                    IsSuccess = false,
                    StatusCode = 403,
                    Message = "PhoneNumber đã tồn tại"

                };
            }
            if (await _userManager.Users.FirstOrDefaultAsync(p=>p.Email==user.Email && p.Status==1) != null)
            {
                return new ResponseMess
                {
                    IsSuccess = false,
                    StatusCode = 403,
                    Message = "Email đã tồn tại"

                };
            }
            else if (await _userManager.Users.FirstOrDefaultAsync(p=>p.UserName == user.UserName && p.Status == 1 ) != null)
            {
                return new ResponseMess
                {
                    IsSuccess = false,
                    StatusCode = 403,
                    Message = "UserName đã tồn tại"

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
                OTP=  RandomOTP(),
                TokenExpires= DateTime.Now,
                Sex= user.Sex,               
                Status = 3
            };
            newUser.PhoneNumber = "+84" + newUser.PhoneNumber.Substring(1);

		 	 await SendSmsOTP(newUser.PhoneNumber, "OTP xác thực đăng kí :" + newUser.OTP);
            newUser.PhoneNumber = newUser.PhoneNumber.Replace("+84","0");

            if (await _roleManager.RoleExistsAsync("user"))
            {
                var result = await _userManager.CreateAsync(newUser, user.Password);
                
                if (!result.Succeeded)
                {
                    return new ResponseMess
                    {
                        IsSuccess = false,
                        StatusCode = 403,
                        Message = "Mật khẩu phải từ 6 kí tự"

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

                ConsumerPoint consumerPoint = new ConsumerPoint()
                { 
                    UserID= id,
                    Point =string.Empty,
                    Status= 1,

                };
				await _context.ConsumerPoints.AddAsync(consumerPoint);
				await _context.SaveChangesAsync();

				return new ResponseMess
                {
                    IsSuccess = true,
                    StatusCode = 201,
                    Message = "Vui lòng xác nhận OTP"
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

		public async Task<ResponseMess> SignUpOTPsync(SignUpUser user)
		{

            var check = await _context.Users.FirstOrDefaultAsync(p => p.UserName == user.UserName && p.Email == user.Email && p.PhoneNumber == user.PhoneNumber && p.Name == user.Name && p.Status==3);
			if ( check!= null)
            {   
                if (check.OTP == user.OTP)
                {
                    if (DateTime.Now.Minute - check.TokenExpires.Minute < 2)
                    {
						check.Status = 1;

						_context.Users.UpdateRange(check);
						await _context.SaveChangesAsync();
						return new ResponseMess
						{
							IsSuccess = true,
							StatusCode = 200,
							Message = "Xác thực thành công",

						};

                    }
                    else
                    {
						return new ResponseMess
						{
							IsSuccess = false,
							StatusCode = 400,
							Message = "OTP có hạn là 2 phút,OTP đã hết hạn hãy ấn vào gửi lại",

						};
					}
				

				}
				return new ResponseMess
				{
					IsSuccess = true,
					StatusCode = 400,
					Message = "Xác thực thất bại1",

				};

			}
			else
			{
				return new ResponseMess
				{
					IsSuccess = false,
					StatusCode = 400,
					Message = "Xác thực thất bại2",
					
				};

			}
		}

		public async Task<ResponseMess> SendSmsOTP(string toPhoneNumber, string message)
		{
			TwilioClient.Init(_twilioSettings.AccountSid, _twilioSettings.AuthToken);

			var Notification = MessageResource.Create(
				body: message,
				from: new Twilio.Types.PhoneNumber(_twilioSettings.PhoneNumber),
				to: new Twilio.Types.PhoneNumber(toPhoneNumber)
			);
            return new ResponseMess {
                IsSuccess = false,
                StatusCode = 400,
                Message = "Gửi thất bại",
                Token = null
            };
		}
	}
}
