using CustomerCareSystem.DataAccess;
using CustomerCareSystem.Interface;
using CustomerCareSystem.Model;
using CustomerCareSystem.Util.SD;
using Dapper;

namespace CustomerCareSystem.Repository;

public class RoleRepository(ApplicationDbContext db) : GenericRepository<Role>(db), IRoleRepository
{

}