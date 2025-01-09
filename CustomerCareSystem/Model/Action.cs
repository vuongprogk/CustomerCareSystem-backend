namespace CustomerCareSystem.Model;

public class Action
{
    public Guid Id { get; set; }
    public Guid FormId { get; set; }
    public Guid PerformBy { get; set; }
    public DateTime ActionDate { get; set; }
    public string Description { get; set; }
}