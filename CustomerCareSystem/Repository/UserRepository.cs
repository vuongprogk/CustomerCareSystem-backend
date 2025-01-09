using CustomerCareSystem.DataAccess;
using CustomerCareSystem.Interface;
using CustomerCareSystem.Model;
using CustomerCareSystem.Util;
using CustomerCareSystem.Util.SD;
using Dapper;
using Microsoft.IdentityModel.Tokens;

namespace CustomerCareSystem.Repository;

public class UserRepository(ApplicationDbContext db, IRoleRepository roleRepository)
    : GenericRepository<User>(db), IUserRepository
{
    private readonly ApplicationDbContext _dbContext = db;

    public async Task<User?> RegisterNewUser(User user, string role)
    {
        var isExistCustomer = await roleRepository.GetByNameAsync(QueryRole.GetRoleByName, role);
        if (isExistCustomer is null)
        {
            var createdSuccess =
                await roleRepository.AddAsync(QueryRole.AddRole, new Role() { Name = role, Description = role });
            if (createdSuccess is null)
            {
                return null;
            }

            user.RoleId = createdSuccess.Id;
        }
        else
        {
            user.RoleId = isExistCustomer.Id;
        }

        using var connection = _dbContext.GetConnection();
        var isUserExist =
            await connection.QueryFirstOrDefaultAsync<User?>(QueryUser.GetUserByEmail, new { Email = user.Email });
        if (isUserExist is null)
        {
            var createdSuccess = await connection.ExecuteAsync(QueryUser.RegisterNewUser, user);
            return createdSuccess > 0 ? user : null;
        }
        return null;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        using var connection = _dbContext.GetConnection();
        return await connection.QueryFirstOrDefaultAsync<User>(QueryUser.GetUserByEmail, new { Email = email });
    }
}