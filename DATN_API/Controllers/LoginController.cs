﻿using DATN_API.Models.ViewModel;
using DATN_API.Service_IService.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginServices _loginServices;
        public LoginController(ILoginServices loginServices)
        {
            _loginServices= loginServices;
        }
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginUser user)
        {
            var result = await _loginServices.LoginAsync(user);
            if (result.IsSuccess)
            {
                return Ok(result.Token);
            }
            else
            {
				return StatusCode(result.StatusCode, result.Message);
			}
        }
    }
}
