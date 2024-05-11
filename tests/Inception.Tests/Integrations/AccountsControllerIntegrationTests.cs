using Inception.Api.Features.Account.Authentication;
using Inception.Api.Features.Account.Login;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Inception.Tests.Integrations;

public class ApiFixture : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing")
            .ConfigureTestServices(services =>
            {
                services.AddMockedApiAuthentication();
                services.AddAuthorization(options =>
                {
                    options.AddPolicy("manager", builder =>
                    {
                        builder.AuthenticationSchemes.Add("Testing");
                        builder.RequireAuthenticatedUser();
                    });
                    options.DefaultPolicy = options.GetPolicy("manager");
                });
            });
    }
}
public static class ServiceCollectionTestExtensions
{
    public static IServiceCollection AddMockedApiAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(defaultScheme: "Testing")
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Testing", options => { });

        return services;
    }
    private class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder)
            : base(options, logger, encoder)
        { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new[] {
                new Claim(ClaimTypes.Name, "Test user"),
                new Claim(ClaimTypes.Role, "Test role")
                };
            var identity = new ClaimsIdentity(claims, "Testing");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, "Testing");

            var result = AuthenticateResult.Success(ticket);

            return Task.FromResult(result);
        }
    }
}
public class AccountsControllerIntegrationTests
{
    //private readonly IFixture _fixture = new Fixture().Customize(new AutoMoqCustomization());
    private readonly ApiFixture _server = new();
    private readonly LoginAccountCommand _command;
    private HttpClient Client => _server.CreateClient();
    const string _url = "api/accounts";

    //private IWriteRepository<Movement> _creditNotesWriter =>
    //    _server.Services.GetRequiredService<IWriteRepository<Movement>>();
    //private IReadRepository<Movement> _creditNotesReader =>
    //    _server.Services.GetRequiredService<IReadRepository<Movement>>();
    public AccountsControllerIntegrationTests()
    {
        _command = new LoginAccountCommand()
        { Username = "batman", Password = "batman" };
    }

    [Fact]
    public async Task Logar_Como_Manager()
    {
        var command = _command with { Username = "batman", Password = "batman" };
        // Arrange
        var content = new StringContent(JsonSerializer.Serialize(command),
            Encoding.UTF8, "application/json");
        // Act
        var result = await Client.PostAsync($"{_url}/login", content);
        var jsonResponse = await result.Content.ReadAsStringAsync();
        var response = JsonSerializer.Deserialize<LoginAccountResponse>(jsonResponse);
        // Assert
        result.EnsureSuccessStatusCode();
        Assert.Equal(200, (int)result.StatusCode);
        Assert.Equal(command.Username, response.Username);
        Assert.Equal("manager", response.Role);
        Assert.NotNull(response.Token);
    }

    [Fact]
    public async Task Logar_Sem_Senha()
    {
        var command = _command with { Password = string.Empty };
        // Arrange
        var content = new StringContent(JsonSerializer.Serialize(command),
            Encoding.UTF8, "application/json");
        // Act
        var result = await Client.PostAsync($"{_url}/login", content);
        // Assert
        Assert.Equal(404, (int)result.StatusCode);
    }

    [Fact]
    public async Task Logar_Sem_Login()
    {
        var command = _command with { Username = string.Empty };
        // Arrange
        var content = new StringContent(JsonSerializer.Serialize(command),
            Encoding.UTF8, "application/json");
        // Act
        var result = await Client.PostAsync($"{_url}/login", content);
        // Assert
        Assert.Equal(404, (int)result.StatusCode);
    }

    [Fact]
    public async Task Validar_Autenticacao_Teste()
    {
        // Act
        var result = await Client.GetAsync($"{_url}/authenticated");
        var jsonResponse = await result.Content.ReadAsStringAsync();
        var response = JsonSerializer.Deserialize<AuthenticationQueryResponse>(jsonResponse);
        // Assert
        result.EnsureSuccessStatusCode();
        Assert.Equal(200, (int)result.StatusCode);
        Assert.Equal("Test user", response.Username);
        Assert.NotNull(response.Roles);
        Assert.Equal("Test role", response.Roles[0]);
    }
}