using Inception.Core;
using Inception.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Inception.Api.Features.Account;

public interface IUserRepository
{
    Task SeedAsync(CancellationToken cancellationToken = default);
    User? Get(string username, string password);
}

public class UserRepository : IUserRepository
{
    private readonly AppDbContext context;
    public UserRepository()
    {
        context = new AppDbContext();
    }

    public User? Get(string username, string password)
    {
        return context.Usuarios.FirstOrDefault(x => x.Username.ToLower() == username.ToLower() && x.Password == password);
    }
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (context.Usuarios.FirstOrDefault(x => x.Id == 1) == null)
            await context.Usuarios.AddAsync(new() { Id = 1, Username = "batman", Password = "batman", Role = "manager" }, cancellationToken);
        if (context.Usuarios.FirstOrDefault(x => x.Id == 2) == null)
            await context.Usuarios.AddAsync(new() { Id = 2, Username = "robin", Password = "robin", Role = "employee" }, cancellationToken);
        if ((context.Usuarios.FirstOrDefault(x => x.Id == 2) == null) || (context.Usuarios.FirstOrDefault(x => x.Id == 1) == null))
            await context.SaveChangesAsync(cancellationToken);
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

public static class Dependencies
{
    public static IServiceCollection AddUserAuthentication(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        var key = Encoding.ASCII.GetBytes(Settings.Secret);
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
        return services;
    }
}