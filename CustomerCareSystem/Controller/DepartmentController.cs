using CustomerCareSystem.DTO.Department;
using CustomerCareSystem.Interface;
using CustomerCareSystem.Model;
using CustomerCareSystem.Util.SD;
using Microsoft.AspNetCore.Mvc;

namespace CustomerCareSystem.Controller;

[ApiController]
[Route("api/department")]
public class DepartmentController(IDepartmentRepository department) : ControllerBase
{
    private readonly IDepartmentRepository _department = department;

    [HttpGet("departments")]
    public async Task<IActionResult> GetDepartments()
    {
        var departments = await _department.GetAllAsync(QueryDepartment.GetDepartments);

        return Ok(departments);
    }

    [HttpGet("department/{id}")]
    public async Task<IActionResult> GetDepartment(string id)
    {
        var department = await _department.GetByIdAsync(QueryDepartment.GetDepartmentById, id);
        if (department == null)
        {
            return NotFound();
        }

        return Ok(department);
    }

    [HttpPost("department")]
    public async Task<IActionResult> AddDepartment([FromBody] AddDepartmentDto department)
    {
        var deparmentObj = new Department()
        {
            Id = department.Id.ToUpper(),
            Name = department.Name,
            Description = department.Description,
        };
        var result = await _department.AddAsync(QueryDepartment.AddDepartment, deparmentObj);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok("Department added successfully");
    }

    [HttpPut("department/{id}")]
    public async Task<IActionResult> UpdateDepartment([FromBody] UpdateDepartmentDto department, string id)
    {
        var departmentObj = new Department()
        {
            Id = id.ToUpper(),
            Name = department.Name,
            Description = department.Description,
        };
        var result = await _department.UpdateAsync(QueryDepartment.UpdateDepartment, departmentObj);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok("Department updated successfully");
    }

    [HttpDelete("department/{id}")]
    public async Task<IActionResult> DeleteDepartment(string id)
    {
        var result = await _department.DeleteAsync(QueryDepartment.DeleteDepartmentById, id);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok("Department deleted successfully");
    }
}