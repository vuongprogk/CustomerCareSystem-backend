using CustomerCareSystem.DataAccess;
using CustomerCareSystem.Interface;
using CustomerCareSystem.Model;

namespace CustomerCareSystem.Repository;

public class DepartmentRepository(ApplicationDbContext db) : GenericRepository<Department>(db), IDepartmentRepository;