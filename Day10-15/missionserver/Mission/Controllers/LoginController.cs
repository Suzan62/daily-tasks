using Mission.Entities;
using Mission.Entities.Models;
using Mission.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mission.Services;
using Microsoft.EntityFrameworkCore;
using Mission.Entities.Context;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Mission.Repositories.Helpers;

namespace Mission.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(ILoginService loginService, IWebHostEnvironment hostingEnvironment,MissionDbContext cIDbContext) : ControllerBase
    {
    

        private readonly ILoginService _loginService = loginService;
        private readonly IWebHostEnvironment _hostingEnvironment = hostingEnvironment;
        private readonly MissionDbContext _cIDbContext = cIDbContext;
        ResponseResult result = new ResponseResult();

        [HttpPost]
        [Route("LoginUser")]
        public ResponseResult LoginUser(LoginUserRequestModel model)
        {
            try
            {
                result.Data = _loginService.LoginUser(model);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserRequestModel registerUserRequest)
        {
            try
            {
                var res = await _loginService.RegisterUser(registerUserRequest);
                return Ok(new ResponseResult() { Data = "User registered", Result = ResponseStatus.Success, Message = "" });
            }
            catch
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "Failed to add user." });
            }
        }
        [HttpGet("LoginUserDetailById/{id}")]
        public IActionResult LoginUserDetailById(int id)
        {
            var user = _loginService.GetUserDetailById(id);
            if (user == null)
                return NotFound("User not found");

            return Ok(new ResponseResult() { Data = user, Result = ResponseStatus.Success, Message = "User found successfully" });
        }
        [HttpPut("UpdateUser")]
        public ActionResult UpdateUser([FromForm] UserDetails u)
        {
            try
            {
                var res = _loginService.UserUpdate(u);
                return Ok(new ResponseResult() { Data = res, Result = ResponseStatus.Success, Message = "User updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = ex.Message });
            }
        }





        [HttpGet("GetUserProfileDetailById/{id}")]
        public IActionResult GetUserProfileDetailById(int id)
        {
            var user = _loginService.GetUserProfileDetailById(id);
            if (user == null)
                return NotFound("User not found");

            return Ok(new ResponseResult() { Data = user, Result = ResponseStatus.Success, Message = "User Details Found successfully" });
        }
        

         [HttpPost]
        [Route("LoginUserProfileUpdate")]
        public async Task<ActionResult> LoginUserProfileUpdate([FromBody] AddUserDetailsRequestModel requestModel)
        {
            try
            {
                var res = await _loginService.LoginUserProfileUpdate(requestModel);
                return Ok(new ResponseResult() { Data = "Data Updated!", Result = ResponseStatus.Success, Message = "" });
            }
            catch
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "Failed to add user." });
            }
        }
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest model)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
         
            if (!int.TryParse(userIdStr, out int userId))
            {
                return Unauthorized(new { message = "Invalid user ID." });
            }

          

            var user = await _cIDbContext.User.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null || user.Password != model.OldPassword)
            {
                return BadRequest(new { message = "Old password is incorrect." });
            }

            if (model.NewPassword != model.ConfirmPassword)
            {
                return BadRequest(new { message = "New password and confirm password do not match." });
            }

            user.Password = model.NewPassword; 
            await _cIDbContext.SaveChangesAsync();

            return Ok(new { result=1, message = "Password changed successfully." });
        }


        //public async Task<IActionResult> ChnagePassword(ChangePasswordRequest dto)
        //{
        //    User user = await _dbContext.User.FirstOrDefaultAsync(u => u.Id == dto.UserId && u.Password == dto.OldPassword);

        //    if (user == null)
        //        return NotFound("No such user available");

        //    user.Password = dto.NewPassword;

        //    await _dbContext.SaveChangesAsync();

        //    return Ok(new { result = 1, data = "Password changed successfully" });
        //}





    }
}
