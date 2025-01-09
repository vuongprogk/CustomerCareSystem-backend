using CustomerCareSystem.DataAccess;
using CustomerCareSystem.Interface;
using CustomerCareSystem.Model;

namespace CustomerCareSystem.Repository;

public class CustomerFormRepository(ApplicationDbContext db) : GenericRepository<CustomerForm>(db), ICustomerFormRepository
{
}