using CustomerCareSystem.DataAccess;
using CustomerCareSystem.Interface;
using CustomerCareSystem.Model;
using CustomerCareSystem.Util.SD;
using Dapper;
using Microsoft.Extensions.Caching.Distributed;

namespace CustomerCareSystem.Repository;

public class RoleRepository(ApplicationDbContext db,    IDistributedCache cache,
    ILogger<GenericRepository<Role>> logger) : GenericRepository<Role>(db, cache, logger), IRoleRepository
{

}