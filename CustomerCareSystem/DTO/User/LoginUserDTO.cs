using System.ComponentModel.DataAnnotations;

namespace CustomerCareSystem.DTO.User;

public class LoginUserDTO
{
    [Required]
    [DataType(DataType.EmailAddress,ErrorMessage ="Email is required")]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
    public string Password { get; set; }
}