using DATN_API.Service_IService.IServices;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private ISignUpServices _signUpServices;
        public SignUpController(ISignUpServices signUpServices)
        {
            _signUpServices= signUpServices;
        }


        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpUser user)
        {
            if (user == null)
            {
                return StatusCode(401,"Phải điền đầy đủ thông tin");
            }
            var result =await _signUpServices.SignUpAsync(user);
            if (result.IsSuccess)
            {
                return StatusCode(result.StatusCode,result.Message);

            }
            else
            {
                return StatusCode(result.StatusCode,result.Message);
            }
        }
		[Route("signup-otp")]
		[HttpPost]
		public async Task<IActionResult> SignUpOTP(SignUpUser user)
		{
			var reusult = await _signUpServices.SignUpOTPsync(user);
            if (reusult.IsSuccess)
            {
                return StatusCode(reusult.StatusCode, reusult.Message);
            }
            else
            {

				return StatusCode(reusult.StatusCode, reusult.Message);
			}
		}
	}
}
