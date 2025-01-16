using System.Security.Claims;
using CustomerCareSystem.DTO.CustomerForm;
using CustomerCareSystem.Interface;
using CustomerCareSystem.Model;
using CustomerCareSystem.Util.SD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace CustomerCareSystem.Controller;

[ApiController]
[Route("api/customerform")]
[Authorize]
public class CustomerFormController(ICustomerFormRepository customerform) : ControllerBase
{
    [HttpGet("customerforms")]
    public async Task<IActionResult> GetCustomerForms()
    {
        var user = HttpContext.User;
        var role = user.Claims.FirstOrDefault(c => c.Type == RoleValue.RoleName)?.Value;
        if (string.IsNullOrEmpty(role))
        {
            return StatusCode(StatusCodes.Status401Unauthorized, new ResponseObject()
            {
                Message = MessageResponse.Unauthorized,
                Status = StatusResponse.Error,
            });
        }

        var userId = user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return StatusCode(StatusCodes.Status401Unauthorized, new ResponseObject()
            {
                Message = MessageResponse.Unauthorized,
                Status = StatusResponse.Error,
            });
        }

        switch (role)
        {
            case RoleValue.Customer:
            {
                var customerForms = await customerform.GetAllCustomerFormByCustomerIdAsync(
                    QueryCustomerForm.GetCustomerFormsByCustomerId, userId, RedisKey.CustomerFormCustomerKey(userId));
                return StatusCode(StatusCodes.Status200OK, new ResponseObjects()
                {
                    Message = MessageResponse.FetchSuccess,
                    Status = StatusResponse.Success,
                    Result = customerForms!,
                });
            }
            case RoleValue.Admin:
            {
                var customerForms =
                    await customerform.GetAllAsync(QueryCustomerForm.GetCustomerForms, RedisKey.CustomerForms);
                return StatusCode(StatusCodes.Status200OK, new ResponseObjects()
                {
                    Message = MessageResponse.FetchSuccess,
                    Status = StatusResponse.Success,
                    Result = customerForms!,
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

    [HttpGet("customerform/{id}")]
    public async Task<IActionResult> GetCustomerForm(string id)
    {
        var user = HttpContext.User;
        var role = user.Claims.FirstOrDefault(c => c.Type == RoleValue.RoleName)?.Value;
        if (string.IsNullOrEmpty(role))
        {
            return StatusCode(StatusCodes.Status401Unauthorized, new ResponseObject()
            {
                Message = MessageResponse.Unauthorized,
                Status = StatusResponse.Error,
            });
        }

        var userId = user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return StatusCode(StatusCodes.Status401Unauthorized, new ResponseObject()
            {
                Message = MessageResponse.Unauthorized,
                Status = StatusResponse.Error,
            });
        }

        switch (role)
        {
            case RoleValue.Customer:
            {
                var customerForm = await customerform.GetAllCustomerFormByFormIdAsync(
                    QueryCustomerForm.GetCustomerFormsById, userId, id, RedisKey.CustomerFormKey(id));
                if (customerForm is null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseObject()
                    {
                        Message = MessageResponse.NotFound,
                        Status = StatusResponse.Error,
                    });
                }

                return StatusCode(StatusCodes.Status200OK, new ResponseObject()
                {
                    Message = MessageResponse.FetchSuccess,
                    Status = StatusResponse.Success,
                    Result = customerForm!,
                });
            }
            case RoleValue.Admin:
            {
                var customerForm = await customerform.GetByIdAsync(QueryCustomerForm.GetCustomerFormsByIdAdmin, id,
                    RedisKey.CustomerFormKey(id));
                if (customerForm is null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseObject()
                    {
                        Message = MessageResponse.NotFound,
                        Status = StatusResponse.Error,
                    });
                }

                return StatusCode(StatusCodes.Status200OK, new ResponseObject()
                {
                    Message = MessageResponse.FetchSuccess,
                    Status = StatusResponse.Success,
                    Result = customerForm!,
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

    [HttpPost("customerform")]
    public async Task<IActionResult> AddCustomerForm([FromBody] AddCustomerFormDto customerForm)
    {
        var user = HttpContext.User;
        var role = user.Claims.FirstOrDefault(c => c.Type == RoleValue.RoleName)?.Value;
        if (string.IsNullOrEmpty(role))
        {
            return StatusCode(StatusCodes.Status401Unauthorized, new ResponseObject()
            {
                Message = MessageResponse.Unauthorized,
                Status = StatusResponse.Error,
            });
        }

        var userId = user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return StatusCode(StatusCodes.Status401Unauthorized, new ResponseObject()
            {
                Message = MessageResponse.Unauthorized,
                Status = StatusResponse.Error,
            });
        }

        if (role != RoleValue.Customer)
            return StatusCode(StatusCodes.Status401Unauthorized, new ResponseObject()
            {
                Message = MessageResponse.Unauthorized,
                Status = StatusResponse.Error,
            });
        var form = new CustomerForm()
        {
            CustomerId = Guid.Parse(userId),
            Title = customerForm.Title,
        };
        var result = await customerform.AddAsync(QueryCustomerForm.AddCustomerForm, form, RedisKey.CustomerForms);
        if (result is null)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseObject()
            {
                Status = StatusResponse.Error,
                Message = MessageResponse.AddCustomerFormFailed,
            });
        }

        return RedirectToAction(nameof(GetCustomerForms));
    }
}