using System.ComponentModel.DataAnnotations;
using CustomerCareSystem.Util.SD;

namespace CustomerCareSystem.DTO.User;

public class RegisterUserDTO
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    [DataType(DataType.EmailAddress,ErrorMessage ="Email is required")]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
    public string Password { get; set; }
    public string Address { get; set; }
    public string Role { get; set; } = RoleValue.Customer;
}