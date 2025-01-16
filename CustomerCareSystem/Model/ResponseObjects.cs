using CustomerCareSystem.Util.SD;

namespace CustomerCareSystem.Model;

public class ResponseObjects
{
    public StatusResponse Status { get; set; }
    public string Message { get; set; } = string.Empty;
    public IEnumerable<Object>? Result { get; set; } = null;
}