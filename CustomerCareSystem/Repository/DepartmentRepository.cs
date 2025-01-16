using CustomerCareSystem.DataAccess;
using CustomerCareSystem.Interface;
using CustomerCareSystem.Model;
using Microsoft.Extensions.Caching.Distributed;

namespace CustomerCareSystem.Repository;

public class DepartmentRepository(
    ApplicationDbContext db,
    IDistributedCache cache,
    ILogger<GenericRepository<Department>> logger)
    : GenericRepository<Department>(db, cache, logger), IDepartmentRepository;