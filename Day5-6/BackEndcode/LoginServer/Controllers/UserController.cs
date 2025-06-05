using LoginServer.Dto;
using LoginServer.Models;
using LoginServer.Helpers;
using LoginServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity.Data;
namespace LoginServer.Controllers
{
    [EnableCors("AllowAll")]

    [ApiController]
    [Route("api/[controller]")]
    public class UserController:Controller
    {
        private readonly UserService _userService;
        private readonly JwtHelper _jwtHelper;

        public UserController(UserService userService, JwtHelper jwtHelper)
        {
            _userService = userService;
            _jwtHelper = jwtHelper;
        }

        
        [HttpPost]
        [Route("Add")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddUser([FromBody] User user)
        {
            // Check if current user is admin, here just an example
            // 
            await _userService.AddUser(user);
            return Ok("User Added!");
        }
        [HttpPut]
        [Route("Update")]
        [Authorize(Roles ="user")]
        public async Task<ActionResult> UpdateUser([FromBody] User user)
        {
            

            return Ok("User updated successfully.");
        }

       
       
        [HttpPost]
        
        [Route("Login")]
        public ActionResult Login([FromBody] LoginReqDto dto)
        {
            var user = _userService.Login(dto.Email, dto.Password);

            if (user == null)
            {
                return NotFound("Please check your email & password");
            }

            var token = _jwtHelper.GetJwtToken(user);

            return Ok(new LoginResDto() { Email = user.Email, Name = user.Email, Role = user.Role, Token = token });
        }
    }
}
