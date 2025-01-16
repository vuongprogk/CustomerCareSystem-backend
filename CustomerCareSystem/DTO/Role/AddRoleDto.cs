using System.ComponentModel.DataAnnotations;

namespace CustomerCareSystem.DTO.Role;

public class AddRoleDto
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
}