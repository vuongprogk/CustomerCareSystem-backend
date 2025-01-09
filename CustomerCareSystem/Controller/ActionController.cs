using Microsoft.AspNetCore.Mvc;
using Action = CustomerCareSystem.Model.Action;

namespace CustomerCareSystem.Controller;

[ApiController]
[Route("api/action")]
public class ActionController: ControllerBase
{
    [HttpGet("GetActions")]
    public IActionResult GetActions()
    {
        return Ok();
    }

    [HttpGet("GetActionById/{id}")]
    public IActionResult GetActionById(string id)
    {
        return Ok();
    }

    [HttpGet("GetActionByCustomerId/{id}")]
    public IActionResult GetActionByCustomerId(string id)
    {
        return Ok();
    }

    [HttpPost("AddAction")]
    public async Task<IActionResult> AddAction([FromBody] Action action)
    {
        return Ok();
    }
}