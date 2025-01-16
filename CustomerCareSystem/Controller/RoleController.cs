using CustomerCareSystem.DTO.Role;
using CustomerCareSystem.Interface;
using CustomerCareSystem.Model;
using CustomerCareSystem.Util.SD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerCareSystem.Controller;

[ApiController]
[Route("api/role")]
[Authorize(Roles = RoleValue.Admin)]
public class RoleController(IRoleRepository role) : ControllerBase
{
    [HttpGet("roles")]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await role.GetAllAsync(QueryRole.GetRoles, RedisKey.Roles);
        return StatusCode(StatusCodes.Status200OK, new ResponseObjects()
        {
            Message = MessageResponse.FetchSuccess,
            Result = roles,
            Status = StatusResponse.Success,
        });
    }

    [HttpGet("role/{id}")]
    public async Task<IActionResult> GetRoleById(string id)
    {
        var roleObj = await role.GetByIdAsync(QueryRole.GetRoleById, id, RedisKey.RoleKey(id));
        if (roleObj == null)
        {
            return StatusCode(StatusCodes.Status404NotFound, new ResponseObjects()
            {
                Message = MessageResponse.NotFound,
                Status = StatusResponse.FetchError,
            });
        }

        return StatusCode(StatusCodes.Status200OK, new ResponseObject()
        {
            Message = MessageResponse.FetchSuccess,
            Result = roleObj,
            Status = StatusResponse.Success,
        });
    }

    [HttpPost("role")]
    public async Task<IActionResult> AddRole([FromBody] AddRoleDto roleSent)
    {
        var user = HttpContext.User.Claims.FirstOrDefault(c => c.Type == RoleValue.RoleName);
        var obj = new Role()
        {
            Name = roleSent.Name,
            Description = roleSent.Description,
        };
        var result = await role.AddAsync(QueryRole.AddRole, obj, RedisKey.Roles);
        if (result is null)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseObject()
            {
                Status = StatusResponse.Error,
                Message = MessageResponse.AddRoleFailed
            });
        }

        return StatusCode(StatusCodes.Status201Created, new ResponseObject()
        {
            Status = StatusResponse.AddSuccess,
            Message = MessageResponse.AddRoleSuccess,
            Result = result
        });
    }

    [HttpPut("role")]
    public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleDto roleSent)
    {
        var obj = new Role()
        {
            Name = roleSent.Name,
            Description = roleSent.Description,
        };
        var result = await role.UpdateAsync(QueryRole.UpdateRole, obj, RedisKey.Roles,
            RedisKey.RoleKey(obj.Id.ToString()));
        if (result is null)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseObject()
            {
                Status = StatusResponse.Error,
                Message = MessageResponse.UpdateRoleFailed
            });
        }

        return StatusCode(StatusCodes.Status200OK, new ResponseObject()
        {
            Status = StatusResponse.Success,
            Message = MessageResponse.UpdateRoleSuccess
        });
    }

    [HttpDelete("role/{id}")]
    public async Task<IActionResult> DeleteRole([FromBody] string id)
    {
        var result = await role.DeleteAsync(QueryRole.DeleteRole, id, RedisKey.Roles, RedisKey.RoleKey(id));
        if (result is null)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseObject()
            {
                Status = StatusResponse.Error,
                Message = MessageResponse.DeleteRoleFailed
            });
        }

        return StatusCode(StatusCodes.Status200OK, new ResponseObject()
        {
            Status = StatusResponse.Success,
            Message = MessageResponse.DeleteRoleSuccess
        });
    }
}