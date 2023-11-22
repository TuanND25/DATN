using DATN_API.Data;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Twilio;
using Twilio.Http;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML.Messaging;

namespace DATN_API.Controllers
{
    [Route("api/forget-password")]
    [ApiController]
    public class ForgetPasswordController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly TwilioSettings _twilioSettings = new TwilioSettings();
        private readonly ApplicationDbContext _context;

        public ForgetPasswordController(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;


        }
        [HttpPost("request")]
        public async Task<IActionResult> CheckUserForget(ForgetPasswordRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Email == request.Email && p.PhoneNumber == request.PhoneNumber && p.UserName == request.Username);
            if (user != null)
            {
				request.PhoneNumber = "+84" + request.PhoneNumber.Substring(1);
				user.OTP = GenerateRandomOTP();
				_context.Users.UpdateRange(user);
				_context.SaveChanges();
				var responseOTP = await SendSmsAsync(request.PhoneNumber, "OTP reset password :" + user.OTP);
				return StatusCode(200);
                //request.PhoneNumber = "+84" + request.PhoneNumber.Substring(1);
                //user.OTP = GenerateRandomOTP();
                //_context.Users.UpdateRange(user);
                //_context.SaveChanges();
                //var responseOTP = await SendSmsAsync(request.PhoneNumber, "OTP reset password :" + user.OTP);

                //if (user.OTP == request.OTP)
                //{
                //    var passwordRandom = GenerateRandomAlphanumericString();
                //    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                //    await _userManager.ResetPasswordAsync(user, code, passwordRandom);
                //    string message = "your new password : " + passwordRandom;

                //    var responsePassword = await SendSmsAsync(request.PhoneNumber, message);
                //}
                //else
                //{
                //    return BadRequest("OTP không chính xác");
                //}




            }
            else
            {
                return StatusCode(404,"Không tìm thấy tài khoản");
            }
            
        }
        [HttpPost("otp")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordRequest request)
        {
			var user = await _context.Users.FirstOrDefaultAsync(p => p.Email == request.Email && p.PhoneNumber == request.PhoneNumber && p.UserName == request.Username);
            if (user != null)
            {
              

                if (user.OTP == request.OTP)
                {
					request.PhoneNumber = "+84" + request.PhoneNumber.Substring(1);
					var passwordRandom = GenerateRandomAlphanumericString();
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    await _userManager.ResetPasswordAsync(user, code, passwordRandom);
                    string message = "your new password : " + passwordRandom;

                    var responsePassword = await SendSmsAsync(request.PhoneNumber, message);
                    return StatusCode(200, "reset passwrod success");
                }
                else
                {
                    return BadRequest("OTP không chính xác");
                }
            }
            else
            {
                return BadRequest();
            }
		}
		private async Task<MessageResource> SendSmsAsync(string toPhoneNumber, string message)
        {
            TwilioClient.Init(_twilioSettings.AccountSid, _twilioSettings.AuthToken);

            var Notification = MessageResource.Create(
                body: message,
                from: new Twilio.Types.PhoneNumber(_twilioSettings.PhoneNumber),
                to: new Twilio.Types.PhoneNumber(toPhoneNumber)
            );

            return Notification;
        }
        public static string GenerateRandomAlphanumericString()
        {
            const string characters = "abcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            char[] result = new char[7];

            for (int i = 0; i < 7; i++)
            {
                result[i] = characters[random.Next(characters.Length)];
            }

            return new string(result);
        }
        public static string GenerateRandomOTP()
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

        //[HttpPost("request")]
        //public async Task<IActionResult> RequestPasswordReset([FromBody] ForgotPasswordRequestModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByEmailAsync(model.Email);

        //        if (user != null)
        //        {
        //            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //            var resetLink = Url.Action("ResetPassword", "Account", new { userId = user.Id, token }, protocol: HttpContext.Request.Scheme);

        //            var message = $"Please reset your password by clicking this link: {resetLink}";

        //            var response = await SendSmsAsync(model.PhoneNumber, message);

        //            if (response != null && response.Status == MessageResource.StatusEnum.Sent)
        //            {
        //                return Ok(new { message = "Password reset link sent via SMS." });
        //            }
        //            else
        //            {
        //                return BadRequest(new { error = "Failed to send SMS." });
        //            }
        //        }
        //        return NotFound(new { error = "User not found." });
        //    }
        //    return BadRequest(ModelState);
        //}

        //private async Task<MessageResource> SendSmsAsync(string toPhoneNumber, string message)
        //{
        //    TwilioClient.Init(_twilioSettings.AccountSid, _twilioSettings.AuthToken);

        //    var message = MessageResource.Create(
        //        body: message,
        //        from: new Twilio.Types.PhoneNumber(_twilioSettings.PhoneNumber),
        //        to: new Twilio.Types.PhoneNumber(toPhoneNumber)
        //    );

        //    return message;
        //}
    }

}
