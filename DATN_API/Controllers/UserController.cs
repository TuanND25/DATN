using DATN_API.Data;
using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

    


		[Route("get-user")]	
		[HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var users = await _userManager.Users.ToListAsync();
     
            return Ok(users);
        }



        [Route("update-user")]
      
        [HttpPut]
        public async Task<IActionResult> UpdateUser(User_VM user)
        {
            await _userService.UpdateUser(user);
            return Ok(user);
        }


        [Route("get-user-byusername")]
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IActionResult> GetUserByUserName(string username)
        {
            var ListUserForUserName = await _userService.GetUserByUserName(username);
            return Ok(ListUserForUserName);
        } 
    }
}
