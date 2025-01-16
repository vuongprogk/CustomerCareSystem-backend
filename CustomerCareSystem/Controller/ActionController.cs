using System.Security.Claims;
using CustomerCareSystem.DTO.Action;
using CustomerCareSystem.Interface;
using CustomerCareSystem.Model;
using CustomerCareSystem.Util.SD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Action = CustomerCareSystem.Model.Action;

namespace CustomerCareSystem.Controller;

[ApiController]
[Route("api/action")]
[Authorize]
public class ActionController(IActionRepository actionRepository) : ControllerBase
{
    [HttpGet("actions")]
    public async Task<IActionResult> GetActions()
    {
        var user = HttpContext.User;
        var role = user.Claims.FirstOrDefault(c => c.Type == RoleValue.RoleName)?.Value;
        if (role is null)
        {
            return StatusCode(StatusCodes.Status401Unauthorized, new ResponseObject()
            {
                Message = MessageResponse.Unauthorized,
                Status = StatusResponse.Error,
            });
        }

        var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (role.Equals(RoleValue.Customer))
            return StatusCode(StatusCodes.Status401Unauthorized, new ResponseObject()
            {
                Message = MessageResponse.Unauthorized,
                Status = StatusResponse.Error,
            });
        ;
        switch (role)
        {
            case RoleValue.Employee:
            {
                if (userId is null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, new ResponseObject()
                    {
                        Message = MessageResponse.Unauthorized,
                        Status = StatusResponse.Error,
                    });
                }

                var actions =
                    await actionRepository.GetAllByEmployeeIdAsync(QueryAction.GetActionByPerformBy, userId,
                        RedisKey.ActionsByPerformBy(userId));
                return StatusCode(StatusCodes.Status200OK, new ResponseObjects()
                {
                    Message = MessageResponse.FetchSuccess,
                    Status = StatusResponse.Success,
                    Result = actions!
                });
            }
            case RoleValue.Admin:
            {
                if (userId is null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, new ResponseObject()
                    {
                        Message = MessageResponse.Unauthorized,
                        Status = StatusResponse.Error,
                    });
                }

                var actions = await actionRepository.GetAllAsync(QueryAction.GetActions, RedisKey.Actions);
                return StatusCode(StatusCodes.Status200OK, new ResponseObjects()
                {
                    Message = MessageResponse.FetchSuccess,
                    Status = StatusResponse.Success,
                    Result = actions!
                });
            }

            default:
                return StatusCode(StatusCodes.Status401Unauthorized, new ResponseObject()
                {
                    Message = MessageResponse.Unauthorized,
                    Status = StatusResponse.Error,
                });
        }
    }

    [HttpGet("actionbyid/{id}")]
    public async Task<IActionResult> GetActionById(string id)
    {
        var user = HttpContext.User;
        var role = user.Claims.FirstOrDefault(c => c.Type == RoleValue.RoleName)?.Value;
        if (role is null)
        {
            return StatusCode(StatusCodes.Status401Unauthorized, new ResponseObject()
            {
                Message = MessageResponse.Unauthorized,
                Status = StatusResponse.Error,
            });
        }

        var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId is null)
        {
            return StatusCode(StatusCodes.Status401Unauthorized, new ResponseObject()
            {
                Message = MessageResponse.Unauthorized,
                Status = StatusResponse.Error,
            });
        }

        var action = await actionRepository.GetByIdAsync(QueryAction.GetActionById, id, RedisKey.ActionKey(id));
        return StatusCode(StatusCodes.Status200OK, new ResponseObject()
        {
            Message = MessageResponse.FetchSuccess,
            Status = StatusResponse.Success,
            Result = action!
        });
    }

    // NOTE: not implement now
    // [HttpGet("GetActionByCustomerId/{id}")]
    // public IActionResult GetActionByCustomerId(string id)
    // {
    //     return Ok();
    // }

    [HttpPost("action")]
    public async Task<IActionResult> AddAction([FromBody] AddActionDto action)
    {
        var user = HttpContext.User;
        var role = user.Claims.FirstOrDefault(c => c.Type == RoleValue.RoleName)?.Value;
        if (role is null)
        {
            return StatusCode(StatusCodes.Status401Unauthorized, new ResponseObject()
            {
                Message = MessageResponse.Unauthorized,
                Status = StatusResponse.Error,
            });
        }

        var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (role.Equals(RoleValue.Customer)) return Forbid();
        if (userId is null)
        {
            return StatusCode(StatusCodes.Status401Unauthorized, new ResponseObject()
            {
                Message = MessageResponse.Unauthorized,
                Status = StatusResponse.Error,
            });
        }

        var actionObj = new Action
        {
            FormId = action.FormId,
            PerformBy = Guid.Parse(userId),
            Description = action.Description,
        };
        var result = await actionRepository.AddAsync(QueryAction.AddAction, actionObj, RedisKey.Actions);
        if (result is null)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseObject()
            {
                Status = StatusResponse.AddError,
                Message = MessageResponse.AddActionFailed
            });
        }

        return StatusCode(StatusCodes.Status200OK, new ResponseObject()
        {
            Message = MessageResponse.FetchSuccess,
            Status = StatusResponse.Success,
            Result = action!
        });
    }
}