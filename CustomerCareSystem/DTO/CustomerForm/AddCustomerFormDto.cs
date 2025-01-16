using System.ComponentModel.DataAnnotations;

namespace CustomerCareSystem.DTO.CustomerForm;

public class AddCustomerFormDto
{
    [Required] public Guid CustomerId { get; set; }
    [Required] public string Title { get; set; }
}