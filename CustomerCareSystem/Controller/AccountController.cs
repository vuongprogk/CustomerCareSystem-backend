using CustomerCareSystem.DTO.User;
using CustomerCareSystem.Interface;
using CustomerCareSystem.Model;
using CustomerCareSystem.Util;
using CustomerCareSystem.Util.SD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CustomerCareSystem.Controller;

[ApiController]
[Route("api/account")]
public class AccountController(IUserRepository user, ITokenService token) : ControllerBase
{
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginUserDTO userSent)
    {
        var userDb = await user.GetUserByEmail(userSent.Email);
        if (userDb == null)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseObject()
            {
                Message = MessageResponse.InvalidUserOrPassword,
                Status = StatusResponse.Error
            });
        }

        var isValidPassword = BCrypt.Net.BCrypt.Verify(userSent.Password, userDb.HashedPassword);
        if (!isValidPassword)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseObject()
            {
                Message = MessageResponse.InvalidUserOrPassword,
                Status = StatusResponse.Error
            });
        }

        var generateToken = await token.GenerateToken(userDb);
        return StatusCode(StatusCodes.Status200OK, new ResponseObject()
        {
            Message = MessageResponse.LoginSuccess,
            Status = StatusResponse.Success,
            Result = generateToken
        });
    }

    [HttpPost("register")]
    [Authorize]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterUserDTO userSent)
    {
        var userObj = new User
        {
            Email = userSent.Email,
            FirstName = userSent.FirstName,
            LastName = userSent.LastName,
            Address = userSent.Address,
            PhoneNumber = userSent.PhoneNumber,
            HashedPassword = BCrypt.Net.BCrypt.HashPassword(userSent.Password),
        };
        var httpContextUser = HttpContext.User;
        var role = httpContextUser.Claims.FirstOrDefault(c => c.Type.Equals(RoleValue.RoleName));
        if (role is null)
        {
            var result = await user.RegisterNewUser(userObj, RoleValue.Customer);
            if (result is null)
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseObject()
                {
                    Status = StatusResponse.Error,
                    Message = MessageResponse.RegisterFailed,
                });
            var generateToken = await token.GenerateToken(userObj);
            return StatusCode(StatusCodes.Status201Created, new ResponseObject()
            {
                Message = MessageResponse.RegisterSuccess,
                Status = StatusResponse.Success,
                Result = generateToken
            });
        }

        if (!role.Value.Equals(RoleValue.Admin)) return BadRequest();
        {
            var result = await user.RegisterNewUser(userObj, userSent.Role);
            return result is null ? StatusCode(StatusCodes.Status400BadRequest, new ResponseObject()
            {
                Status = StatusResponse.Error,
                Message = MessageResponse.RegisterFailed,
            }) 
                : StatusCode(StatusCodes.Status201Created, new ResponseObject()
                {
                    Message = MessageResponse.RegisterSuccess,
                    Status = StatusResponse.Success,
                });
        }
    }

    // NOTE: not implement now
    // [HttpPost("logout")]
    // public async Task<IActionResult> Logout()
    // {
    //     
    // }
}