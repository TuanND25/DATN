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

        [Authorize]         
		[Route("get-user")]	
		[HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var users = await _userManager.Users.ToListAsync();
     
            return Ok(users);
        }

        [HttpGet("get_user_by_id/{Id}")]
        public async Task<IActionResult> GetUser(Guid Id)
        {
            var a = await _userService.GetUserById(Id);
            return Ok(a);
        }


        [Route("update-status-user")]
      
        [HttpPut]
        public async Task<IActionResult> UpdateStatusUser(User_VM user)
        {
            await _userService.UpdateStatusUser(user);
            return Ok(user);
        }

        [Route("update-user")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUser_VM updateUser)
        {
           var result = await _userService.UpdateUser(updateUser);
            if (result.IsSuccess)
            {
                return Ok();
            }
            else
            {
                return BadRequest();    
            }
        }


        [Route("get-user-byusername")]
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IActionResult> GetUserByUserName(string username)
        {
            var ListUserForUserName = await _userService.GetUserByUserName(username);
            return Ok(ListUserForUserName);
        }


        [Route("change-password")]
       
        [HttpPut] 
        
        
        public async Task<IActionResult> ChangePassword(ChangePassword_VM changePassword)
        {
            var result = await _userService.ChangePassword(changePassword);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [Route("add-employee-admin")]
        [HttpPost]
        public async Task<IActionResult> AddEmployeeOrAdmin(SignUpUser user,string role)
        {
            var result = await _userService.AddEmployeeOrAdmin(user,role);
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
