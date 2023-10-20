using DATN_API.Data;
using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly UserManager<User> _userManager;
        public readonly ApplicationDbContext _context;
        public readonly IUserService _userService;
        public UserController(ApplicationDbContext context, IUserService userService,UserManager<User> userManager)
        {
            _context = context;
            _userService = userService;
			_userManager = userManager;
		}

    


		[Authorize(Roles ="Admin")]
		[Route("get-user")]	
		[HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var users = _userManager.Users.ToList();
            var address = _context.AddressShips.ToList();
            var list_User_Address = from u in users
                                    join a in address on u.Id equals a.UserId
                                    select new
                                    {
                                        u.Id,
                                        u.UserName,
                                        u.PhoneNumberConfirmed,
                                        u.EmailConfirmed,
                                        u.AddressShips

                                    };
            return Ok(users);
        }



        [Route("update-user")]
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(User user)
        {
            await _userService.UpdateUser(user);
            return Ok(user);
        }

    }
}
