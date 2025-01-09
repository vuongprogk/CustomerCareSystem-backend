using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CustomerCareSystem.Interface;
using CustomerCareSystem.Model;
using CustomerCareSystem.Util.SD;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace CustomerCareSystem.Repository;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly string _key;
    private readonly IRoleRepository _role;

    public TokenService(IConfiguration configuration, IRoleRepository role)
    {
        _configuration = configuration;
        _key = _configuration["Jwt:Key"]!;
        _role = role;
    }

    public async Task<string> GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Name, user.LastName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };
        if (user.RoleId != null)
        {
            var role = await _role.GetByIdAsync(QueryRole.GetRoleById, user.RoleId.ToString());
            if (role != null)
            {
                var roleClaims = new Claim(RoleValue.RoleName, role.Name);
                claims = [.. claims, roleClaims];
            }
        }

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credential
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}