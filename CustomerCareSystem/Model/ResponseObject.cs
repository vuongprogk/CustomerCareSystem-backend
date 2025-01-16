using CustomerCareSystem.Util.SD;

namespace CustomerCareSystem.Model;

public class ResponseObject
{
    public StatusResponse Status { get; set; }
    public string Message { get; set; } = string.Empty;
    public Object? Result { get; set; } = null;
}