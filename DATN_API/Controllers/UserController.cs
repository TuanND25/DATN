using DATN_Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
    [Route("api/getuser")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly UserManager<User> _userManager;
        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var users = _userManager.Users.ToList();
            return Ok(users);   
        }

    }
}
