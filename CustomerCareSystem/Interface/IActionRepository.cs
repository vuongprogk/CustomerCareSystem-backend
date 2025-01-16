using Action = CustomerCareSystem.Model.Action;

namespace CustomerCareSystem.Interface;

public interface IActionRepository: IGenericRepository<Action>
{
    public Task<IEnumerable<Action?>> GetAllByEmployeeIdAsync(string query, string employeeId, string redisKey);
    
}