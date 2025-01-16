using CustomerCareSystem.DataAccess;
using CustomerCareSystem.Interface;
using CustomerCareSystem.Model;
using CustomerCareSystem.Util.Redis;
using Dapper;
using Microsoft.Extensions.Caching.Distributed;

namespace CustomerCareSystem.Repository;

public class CustomerFormRepository(
    ApplicationDbContext db,
    IDistributedCache cache,
    ILogger<GenericRepository<CustomerForm>> logger)
    : GenericRepository<CustomerForm>(db, cache, logger), ICustomerFormRepository
{
    private readonly ApplicationDbContext _db = db;
    private readonly IDistributedCache _cache = cache;
    private readonly ILogger<GenericRepository<CustomerForm>> _logger = logger;

    public async Task<IEnumerable<CustomerForm?>> GetAllCustomerFormByCustomerIdAsync(string query, string customerId,
        string redisKey)
    {
        _logger.LogInformation("Fetching data from cache for {key}", redisKey);
        var item = await _cache.GetOrSetAsync(
            redisKey, async () =>
            {
                _logger.LogInformation("Cache miss. Fetching data from cache for {key}", redisKey);
                using var connection = _db.GetConnection();
                return await connection.QueryAsync<CustomerForm?>(query, new { CustomerId = customerId });
            });
        return item!;
    }

    public async Task<CustomerForm?> GetAllCustomerFormByFormIdAsync(string query, string customerId, string id, string redisKey)
    {
        _logger.LogInformation("Fetching data from cache for {key}", redisKey);
        var item = await _cache.GetOrSetAsync(
            redisKey, async () =>
            {
                _logger.LogInformation("Cache miss. Fetching data from cache for {key}", redisKey);
                using var connection = _db.GetConnection();
                return await connection.QueryFirstOrDefaultAsync<CustomerForm?>(query, new { CustomerId = customerId, Id = id });
            });
        return item!;
    }
}