using DATN_API.Data;
using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Twilio.Types;

namespace DATN_API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly UserManager<User> _userManager;
        public readonly RoleManager<Role> _roleManager;
        public readonly ApplicationDbContext _context;
        public readonly IUserService _userService;
        public UserController(ApplicationDbContext context, IUserService userService,UserManager<User> userManager, RoleManager<Role> role)
        {
            _context = context;
            _userService = userService;
			_userManager = userManager;
            _roleManager = role;
		}

		[Route("get-user")]	
		[HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var listuser = from users in _userManager.Users
                           join roleusers in _context.UserRoles on users.Id equals roleusers.UserId
                           join roles in _roleManager.Roles on roleusers.RoleId equals roles.Id
                           select new
                           {
                               id = users.Id,
                               name = users.Name,
                               username = users.UserName,
                               phonenumber = users.PhoneNumber,
                               email = users.Email,
                               sex = users.Sex,
                               status = users.Status,
                               role = roles.Name,
                           };
                      
     
            return Ok(listuser);
        }

        [HttpGet("get_user_by_id/{Id}")]
        public async Task<IActionResult> GetUserById(Guid Id)
        {
            var a = await _userService.GetUserById(Id);
            return Ok(a);
        }


        [Route("update-status-user")]
      
        [HttpPut]
        public async Task<IActionResult> UpdateStatusUser(User_VM user)
        {
        
            var response = await _userService.UpdateStatusUser(user);
            if (response.IsSuccess)
            {
                return StatusCode(response.StatusCode, response.Message);

            }
            else
            {
				return StatusCode(response.StatusCode, response.Message);

			}
        }

        [Route("update-user")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(AddUserByAdmin updateUser)
        {
           var result = await _userService.UpdateUser(updateUser);
            if (result.IsSuccess)
            {
                return StatusCode(result.StatusCode,result.Message);
            }
            else
            {
                return StatusCode(result.StatusCode, result.Message);    
            }
        }


        [Route("get-user-byusername")]
        //[Authorize(Roles ="Admin")]
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
        public async Task<IActionResult> AddEmployeeOrAdmin(AddUserByAdmin user)
        {
            var result = await _userService.AddEmployeeOrAdmin(user);
            if (result.IsSuccess)
            {
                return StatusCode(result.StatusCode,result.Message);
            }
            else
            {
                return StatusCode(result.StatusCode,result.Message);
            }
        }

    }
}
