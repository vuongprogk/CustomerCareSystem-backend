using CustomerCareSystem.DataAccess;
using CustomerCareSystem.Interface;
using Action = CustomerCareSystem.Model.Action;

namespace CustomerCareSystem.Repository;

public class ActionRepository(ApplicationDbContext db) : GenericRepository<Action>(db), IActionRepository
{
    
}