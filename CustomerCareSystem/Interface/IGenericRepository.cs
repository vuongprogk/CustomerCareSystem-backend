namespace CustomerCareSystem.Interface;

public interface IGenericRepository<T> where T : class
{
    public Task<IEnumerable<T>> GetAllAsync(string query);
    public Task<T?> GetByIdAsync(string query, string id);
    public Task<T?> GetByNameAsync(string query, string name);
    public Task<T?> AddAsync(string query, T entity);
    public Task<T?> UpdateAsync(string query, T entity);
    public Task<T?> DeleteAsync(string query, string id);
}