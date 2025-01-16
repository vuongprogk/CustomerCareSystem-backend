using System.ComponentModel.DataAnnotations;

namespace CustomerCareSystem.DTO.Role;

public class UpdateRoleDto
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
}