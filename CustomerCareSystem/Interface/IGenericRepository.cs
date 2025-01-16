namespace CustomerCareSystem.Interface;

public interface IGenericRepository<T> where T : class
{
    public Task<IEnumerable<T>> GetAllAsync(string query, string key);
    public Task<T?> GetByIdAsync(string query, string id, string key);
    public Task<T?> GetByNameAsync(string query, string name, string key);
    public Task<T?> AddAsync(string query, T entity,string keyAll);
    public Task<T?> UpdateAsync(string query, T entity,string keyAll, string keyNew);
    public Task<T?> DeleteAsync(string query, string id, string keyAll, string keyOld);
}