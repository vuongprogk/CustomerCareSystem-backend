using System.ComponentModel.DataAnnotations;

namespace CustomerCareSystem.DTO.Department;

public class UpdateDepartmentDto
{
    [Required] [MaxLength(50)] public string Name { get; set; }
    public string Description { get; set; }
}