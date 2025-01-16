namespace CustomerCareSystem.Model;

public class Action
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid FormId { get; set; }
    public Guid PerformBy { get; set; }
    public DateTime ActionDate { get; set; } = DateTime.Now;
    public string Description { get; set; }
}