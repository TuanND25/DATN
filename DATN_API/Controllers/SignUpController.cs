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
        public async Task<IActionResult> SignUp(SignUpUser user,string role)
        {
            var result =await _signUpServices.SignUpAsync(user,role);
            if (result.IsSuccess)
            {
                return Ok(result);

            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
