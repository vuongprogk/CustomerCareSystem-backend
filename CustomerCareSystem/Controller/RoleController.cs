using CustomerCareSystem.Model;
using Microsoft.AspNetCore.Mvc;

namespace CustomerCareSystem.Controller;

[ApiController]
[Route("api/roles")]
public class RoleController: ControllerBase
{
    [HttpGet("GetRoles")]
    public async Task<IActionResult> GetRoles()
    {
        return Ok();
    }

    [HttpGet("GetRoleById/{id}")]
    public async Task<IActionResult> GetRoleById(int id)
    {
        return Ok();
    }
    [HttpPost("AddRole")]
    public async Task<IActionResult> AddRole([FromBody] Role role)
    {
        return Ok();
    }

    [HttpPut("UpdateRole")]
    public async Task<IActionResult> UpdateRole([FromBody] Role role)
    {
        return Ok();
    }

    [HttpDelete("DeleteRole")]
    public async Task<IActionResult> DeleteRole([FromBody] Role role)
    {
        return Ok();
    }
}