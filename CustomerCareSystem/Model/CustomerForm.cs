namespace CustomerCareSystem.Model;

public class CustomerForm
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CustomerId { get; set; }
    public string Title { get; set; }
    public bool Status { get; set; } = false;
    public DateTime CreatedDate { get; set; }
    public DateTime ResolvedDate { get; set; }
}