using FluentResults;
using Inception.Core.Entities;
using Inception.Infrastructure.Persistence.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Inception.Application.Features.LoginAccount;
public interface ILoginAccountHandler
{
    Task<Result<LoginAccountResponse>> Login(LoginAccountCommand command, CancellationToken cancellationToken = default);
}
public class LoginAccountHandler(IUserRepository userRepository) : ILoginAccountHandler
{
    public async Task<Result<LoginAccountResponse>> Login(LoginAccountCommand command, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.Get(command.Username, command.Password, cancellationToken);
        return user == null
            ? Result.Fail("Usuário ou senha inválidos")
            : Result.Ok(new LoginAccountResponse(user.Id, user.Username, user.Role, TokenService.GenerateToken(user)));
    }
}
public static class Settings
{
    public static readonly string Secret = "fedaf7d8863b48e197b9287d492b708e";
}
public static class TokenService
{
    public static string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Settings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Name, user.Username),
                new(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}