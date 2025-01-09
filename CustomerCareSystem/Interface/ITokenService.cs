using CustomerCareSystem.Model;

namespace CustomerCareSystem.Interface;

public interface ITokenService
{
    // TODO: define method for generate token
    public Task<string> GenerateToken(User user);
}