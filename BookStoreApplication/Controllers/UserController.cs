using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Threading.Tasks;
using System;
using BookStoreCommon.Model;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;
using BookStoreBussiness.IBussiness;
using NLogImplementation;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        Nlog nlog  = new Nlog();
        public readonly IUserBussiness userBussiness;
        public UserController(IUserBussiness userBussiness)
        {
            this.userBussiness = userBussiness;
        }
        [HttpPost]
        [Route("Register")]
        public Task<ActionResult> UserRegister(User register)
        {
            try
            {
                var result = this.userBussiness.RegisterUser(register);
                if (result == true)
                {
                    return Task.FromResult<ActionResult>(this.Ok(new { Status = true, Message = "User Registration Successful", Data = register }));
                }
                return Task.FromResult<ActionResult>(this.BadRequest(new { Status = false, Message = "User Registration Unsuccessful" }));
            }
            catch (Exception ex)
            {
                return Task.FromResult<ActionResult>(this.NotFound(new { Status = false, Message = ex.Message }));
            }
        }
        [HttpPost]
        [Route("Login")]
        public ActionResult UserLogin(string email,string password)
        {
            try
            {
                var result = this.userBussiness.LoginUser(email,password);
                if (result != null)
                {
                    var tokenhandler = new JwtSecurityTokenHandler();
                    var jwtToken = tokenhandler.ReadJwtToken(result);
                    var id = jwtToken.Claims.FirstOrDefault(c => c.Type == "Id");
                    string Id = id.Value;
                    return this.Ok(new { Status = true, Message = "User Login Successful", Data = result, id = Id });
                }
                return this.BadRequest(new { Status = false, Message = "User Login Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut]
        [Route("ResetPassword")]
        public ActionResult UserResetPassword(string newpassword,string confirmpassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = this.userBussiness.ResetPassword(email, newpassword, confirmpassword);
                if (result != null)
                {
                    nlog.LogInfo("password reset Successfully");
                    return this.Ok(new { Status = true, Message = "User Password Reset Successful", Data = result });
                }
                return this.BadRequest(new { Status = false, Message = "User Password Reset Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("ForgetPassword")]

        public ActionResult ForgetPassword(string email)
        {
            try
            {
                var resultLog = this.userBussiness.ForgetPassword(email);

                if (resultLog != null)
                {
                    return Ok(new { success = true, message = "Reset Email Send" });
                }
                else
                {
                    return BadRequest(new { success = false, message = " UnSuccessful" });
                }

            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
