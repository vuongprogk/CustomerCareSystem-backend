using CustomerCareSystem.DTO.Department;
using CustomerCareSystem.Interface;
using CustomerCareSystem.Model;
using CustomerCareSystem.Util.SD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerCareSystem.Controller;

[ApiController]
[Route("api/department")]
[Authorize(Roles = RoleValue.Admin)]
public class DepartmentController(IDepartmentRepository department) : ControllerBase
{
    [HttpGet("departments")]
    public async Task<IActionResult> GetDepartments()
    {
        var departments = await department.GetAllAsync(QueryDepartment.GetDepartments, RedisKey.Departments);

        return StatusCode(StatusCodes.Status200OK, new ResponseObjects()
        {
            Status = StatusResponse.FetchSuccess,
            Message = MessageResponse.UpdateRoleSuccess,
            Result = departments
        });
    }

    [HttpGet("department/{id}")]
    public async Task<IActionResult> GetDepartment(string id)
    {
        var departmentObj =
            await department.GetByIdAsync(QueryDepartment.GetDepartmentById, id, RedisKey.DepartmentKey(id));
        if (departmentObj == null)
        {
            return StatusCode(StatusCodes.Status404NotFound, new ResponseObject()
            {
                Status = StatusResponse.FetchError,
                Message = MessageResponse.NotFound,
            });
        }

        return StatusCode(StatusCodes.Status200OK, new ResponseObject()
        {
            Status = StatusResponse.FetchSuccess,
            Message = MessageResponse.FetchSuccess,
            Result = departmentObj
        });
    }

    [HttpPost("department")]
    public async Task<IActionResult> AddDepartment([FromBody] AddDepartmentDto departmentSent)
    {
        var departmentObj = new Department()
        {
            Id = departmentSent.Id.ToUpper(),
            Name = departmentSent.Name,
            Description = departmentSent.Description,
        };
        var isDuplicate = await department.GetByIdAsync(QueryDepartment.GetDepartmentById, departmentSent.Id,
            RedisKey.DepartmentKey(departmentSent.Id));
        if (isDuplicate != null)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseObject()
            {
                Status = StatusResponse.AddError,
                Message = MessageResponse.AddDepartmentFailed
            });
        }

        var result = await department.AddAsync(QueryDepartment.AddDepartment, departmentObj, RedisKey.Departments);
        if (result is null)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseObject()
            {
                Status = StatusResponse.AddError,
                Message = MessageResponse.AddDepartmentFailed
            });
        }
        return StatusCode(StatusCodes.Status201Created, new ResponseObject()
        {
            Status = StatusResponse.AddSuccess,
            Message = MessageResponse.AddDepartmentSuccess,
            Result = result
        });

    }

    [HttpPut("department/{id}")]
    public async Task<IActionResult> UpdateDepartment([FromBody] UpdateDepartmentDto departmentSent, string id)
    {
        var departmentObj = new Department()
        {
            Id = id.ToUpper(),
            Name = departmentSent.Name,
            Description = departmentSent.Description,
        };
        var result = await department.UpdateAsync(QueryDepartment.UpdateDepartment, departmentObj,
            RedisKey.Departments, RedisKey.DepartmentKey(id));
        if (result is null)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseObject()
            {
                Status = StatusResponse.UpdateError,
                Message = MessageResponse.UpdateDepartmentFailed
            });
        }

        return StatusCode(StatusCodes.Status200OK, new ResponseObject()
        {
            Status = StatusResponse.Success,
            Message = MessageResponse.UpdateDepartmentSuccess
        });
    }

    [HttpDelete("department/{id}")]
    public async Task<IActionResult> DeleteDepartment(string id)
    {
        var isExit = await department.GetByIdAsync(QueryDepartment.GetDepartmentById, id, RedisKey.DepartmentKey(id));
        if (isExit is null)
        {
            return StatusCode(StatusCodes.Status404NotFound, new ResponseObject()
            {
                Status = StatusResponse.FetchError,
                Message = MessageResponse.NotFound
            });
        }

        var result = await department.DeleteAsync(QueryDepartment.DeleteDepartmentById, id, RedisKey.Departments,
            RedisKey.DepartmentKey(id));
        if (result is null)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseObject()
            {
                Status = StatusResponse.Error,
                Message = MessageResponse.DeleteDepartmentFailed
            });
        }

        return StatusCode(StatusCodes.Status200OK, new ResponseObject()
        {
            Status = StatusResponse.Success,
            Message = MessageResponse.DeleteDepartmentSuccess
        });
    }
}