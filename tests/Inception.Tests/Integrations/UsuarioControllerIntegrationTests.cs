using AutoFixture;
using AutoFixture.AutoMoq;
using Inception.Api.Features.Usuarios;
using System.Text;
using System.Text.Json;

namespace Inception.Tests.Integrations;

public class UsuarioControllerIntegrationTests
{
    private readonly IFixture _fixture = new Fixture().Customize(new AutoMoqCustomization());
    private readonly ApiFixture _server = new();
    private readonly CriacaoUsuarioCommand _command;
    private HttpClient Client => _server.CreateClient();

    public UsuarioControllerIntegrationTests()
    {
        _command = _fixture.Build<CriacaoUsuarioCommand>()
            .Create();
    }

    [Fact]
    public async Task Post_Created()
    {
        //var command = _command with { FirstName = string.Empty };
        // Arrange
        var content = new StringContent(JsonSerializer.Serialize(_command),
            Encoding.UTF8, "application/json");
        // Act
        var result = await Client.PostAsync("api/usuarios", content);
        // Assert
        Assert.Equal(204, (int)result.StatusCode);
    }
}
