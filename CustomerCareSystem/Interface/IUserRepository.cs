using CustomerCareSystem.Model;

namespace CustomerCareSystem.Interface;

public interface IUserRepository : IGenericRepository<User>
{
    // TODO: define method for all user
    public Task<User?> RegisterNewUser(User user, string role);
    public Task<User?> GetUserByEmail(string email);
}