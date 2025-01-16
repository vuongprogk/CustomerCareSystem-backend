using CustomerCareSystem.Model;

namespace CustomerCareSystem.Interface;

public interface ICustomerFormRepository: IGenericRepository<CustomerForm>
{
    // TODO: define method for Customer
    public Task<IEnumerable<CustomerForm?>> GetAllCustomerFormByCustomerIdAsync(string query, string customerId, string redisKey);
    public Task<CustomerForm?> GetAllCustomerFormByFormIdAsync(string query, string customerId, string id, string redisKey);
}