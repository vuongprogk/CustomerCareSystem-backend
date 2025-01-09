using CustomerCareSystem.DataAccess;
using CustomerCareSystem.Interface;
using Dapper;

namespace CustomerCareSystem.Repository;

public class GenericRepository<T>(ApplicationDbContext db) : IGenericRepository<T>
    where T : class
{
    public async Task<IEnumerable<T>> GetAllAsync(string query)
    {
        using var connection = db.GetConnection();
        return await connection.QueryAsync<T>(query);
    }

    public async Task<T?> GetByIdAsync(string query, string id)
    {
        using var connection = db.GetConnection();
        return await connection.QuerySingleOrDefaultAsync<T>(query, new { Id = id });
    }

    public async Task<T?> GetByNameAsync(string query, string name)
    {
        using var connection = db.GetConnection();
        return await connection.QueryFirstOrDefaultAsync<T>(query, new { Name = name });
    }

    public async Task<T?> AddAsync(string query, T entity)
    {
        using var connection = db.GetConnection();
        return await connection.QueryFirstAsync<T>(query, entity);
    }

    public async Task<T?> UpdateAsync(string query, T entity)
    {
        using var connection = db.GetConnection();
        return await connection.QueryFirstAsync(query, entity);
    }

    public async Task<T?> DeleteAsync(string query, string id)
    {
        using var connection = db.GetConnection();
        return await connection.QueryFirstAsync(query, new { Id = id });
    }
}