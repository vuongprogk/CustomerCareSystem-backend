using CustomerCareSystem.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerCareSystem.Controller;

[ApiController]
[Route("api/customerform")]
[Authorize]
public class CustomerFormController: ControllerBase
{
    [HttpGet("GetCustomerForms")]
    public IActionResult GetCustomerForms()
    {
        return Ok();
    }

    [HttpGet("GetCustomerForm/{id}")]
    public IActionResult GetCustomerForm(string id)
    {
        return Ok();
    }

    [HttpPost("AddCustomerForm")]
    public IActionResult AddCustomerForm(CustomerForm customerForm)
    {
        return Ok();
    }
}