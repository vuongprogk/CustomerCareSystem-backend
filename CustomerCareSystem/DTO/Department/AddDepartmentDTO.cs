using System.ComponentModel.DataAnnotations;

namespace CustomerCareSystem.DTO.Department;

public class AddDepartmentDto
{
    [Required]
    [MaxLength(50)]
    public string Id { get; set; }
    [Required] [MaxLength(50)] public string Name { get; set; }
    public string Description { get; set; }
}