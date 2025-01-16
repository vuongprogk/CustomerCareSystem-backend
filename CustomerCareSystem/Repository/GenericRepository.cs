using CustomerCareSystem.DataAccess;
using CustomerCareSystem.Interface;
using CustomerCareSystem.Util.Redis;
using Dapper;
using Microsoft.Extensions.Caching.Distributed;

namespace CustomerCareSystem.Repository;

public class GenericRepository<T>(
    ApplicationDbContext db,
    IDistributedCache cache,
    ILogger<GenericRepository<T>> logger) : IGenericRepository<T>
    where T : class
{
    public async Task<IEnumerable<T>> GetAllAsync(string query, string key)
    {
        logger.LogInformation("Fetching data from cache for {key}", key);
        var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(20))
            .SetSlidingExpiration(TimeSpan.FromMinutes(2));
        var list = await cache.GetOrSetAsync(
            key, async () =>
            {
                logger.LogInformation("Cache miss. Fetching data from database for {key}", key);
                using var connection = db.GetConnection();
                return await connection.QueryAsync<T>(query);
            },
            options);
        return list!;
    }

    public async Task<T?> GetByIdAsync(string query, string id, string key)
    {
        logger.LogInformation("Fetching data from cache for {key}", key);
        var item = await cache.GetOrSetAsync(
            key,
            async () =>
            {
                logger.LogInformation("Cache miss. Fetching data from cache for {key}", key);
                using var connection = db.GetConnection();
                return await connection.QuerySingleOrDefaultAsync<T>(query, new { Id = id });
            });
        return item;
    }

    public async Task<T?> GetByNameAsync(string query, string name, string key)
    {
        logger.LogInformation("Fetching data from cache for {key}", key);
        var item = await cache.GetOrSetAsync(
            key, async () =>
            {
                logger.LogInformation("Cache miss. Fetching data from cache for {key}", key);

                using var connection = db.GetConnection();
                return await connection.QueryFirstOrDefaultAsync<T>(query, new { Name = name });
            });
        return item;
    }

    public async Task<T?> AddAsync(string query, T entity, string keyAll)
    {
        using var connection = db.GetConnection();
        var result = await connection.QueryFirstOrDefaultAsync<T>(query, entity);
        if (result is null) return result;
        logger.LogInformation("Invalid data from cache for {key}", keyAll);
        await cache.RemoveAsync(keyAll);
        return result;
    }

    public async Task<T?> UpdateAsync(string query, T entity, string keyAll, string keyNew)
    {
        using var connection = db.GetConnection();
        var result = await connection.QueryFirstOrDefaultAsync<T>(query, entity);
        if (result is null) return result;
        logger.LogInformation("Invalid data from cache for {key}", keyAll);
        await cache.RemoveAsync(keyAll);
        await cache.RemoveAsync(keyNew);
        return result;
    }

    public async Task<T?> DeleteAsync(string query, string id, string keyAll, string keyOld)
    {
        using var connection = db.GetConnection();
        var result = await connection.QueryFirstOrDefaultAsync(query, new { Id = id });
        if (result is null) return result;
        logger.LogInformation("Invalid data from cache for {key}", keyAll);
        await cache.RemoveAsync(keyAll);
        await cache.RemoveAsync(keyOld);

        return result;
    }
}