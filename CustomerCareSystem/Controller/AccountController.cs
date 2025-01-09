using CustomerCareSystem.DTO.User;
using CustomerCareSystem.Interface;
using CustomerCareSystem.Model;
using CustomerCareSystem.Util;
using CustomerCareSystem.Util.SD;
using Microsoft.AspNetCore.Mvc;

namespace CustomerCareSystem.Controller;

[ApiController]
[Route("api/account")]
public class AccountController: ControllerBase
{
    private readonly IUserRepository _user;
    private readonly IRoleRepository _role;
    private readonly ITokenService _token;

    public AccountController(IUserRepository user, IRoleRepository role, ITokenService token)
    {
        _user = user;
        _role = role;
        _token = token;
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDTO user)
    {
        var userDb = await _user.GetUserByEmail(user.Email);
        if (userDb == null)
        {
            return Unauthorized("Invalid username or password");
        }

        var isValidPassword = BCrypt.Net.BCrypt.Verify(user.Password, userDb.HashedPassword);
        if (!isValidPassword)
        {
            return Unauthorized("Invalid username or password");
        }
        var token = await _token.GenerateToken(userDb);
        return Ok(new {Token = token});
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDTO user)
    {
        var userObj = new User
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Address = user.Address,
            PhoneNumber = user.PhoneNumber,
            HashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password),
        };
        
        var result = await _user.RegisterNewUser(userObj, RoleValue.Customer);
        if (result is not null)
        {
            var token = await _token.GenerateToken(userObj);
            return Ok(new { Token = token });
        }
        return Ok("Register failed");
    }
    
}