using CustomerCareSystem.DataAccess;
using CustomerCareSystem.Interface;
using CustomerCareSystem.Util.Redis;
using CustomerCareSystem.Util.SD;
using Dapper;
using Microsoft.Extensions.Caching.Distributed;
using Action = CustomerCareSystem.Model.Action;

namespace CustomerCareSystem.Repository;

public class ActionRepository(
    ApplicationDbContext db,
    IDistributedCache cache,
    ILogger<GenericRepository<Action>> logger) : GenericRepository<Action>(db, cache, logger), IActionRepository
{
    private readonly ILogger<GenericRepository<Action>> _logger = logger;
    private readonly ApplicationDbContext _db = db;
    private readonly IDistributedCache _cache = cache;

    public async Task<IEnumerable<Action?>> GetAllByEmployeeIdAsync(string query, string employeeId, string redisKey)
    {
        _logger.LogInformation($"Getting all actions for {employeeId}");
        var actions = await _cache.GetOrSetAsync(redisKey,
            async () =>
            {
                _logger.LogInformation($"Cache miss, Getting all actions for {employeeId}");
                using var connection = _db.GetConnection();
                var actions = await connection.QueryAsync<Action>(QueryAction.GetActionByPerformBy);
                return actions;
            });
        return actions!;
    }
}