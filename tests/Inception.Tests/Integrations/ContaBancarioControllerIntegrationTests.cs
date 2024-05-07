using AutoFixture;
using AutoFixture.AutoMoq;
using Inception.Api.Features.ContasBancarias;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using System.Text.Json;

namespace Inception.Tests.Integrations;

public class ApiFixture : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
    }
}

public class ContasBancariasControllerIntegrationTests
{
    private readonly IFixture _fixture = new Fixture().Customize(new AutoMoqCustomization());
    private readonly ApiFixture _server = new();
    private readonly AberturaContaCommand _command;
    private HttpClient Client => _server.CreateClient();
    const string _url = "api/ContasBancarias";

    //private IWriteRepository<Movement> _creditNotesWriter =>
    //    _server.Services.GetRequiredService<IWriteRepository<Movement>>();
    //private IReadRepository<Movement> _creditNotesReader =>
    //    _server.Services.GetRequiredService<IReadRepository<Movement>>();
    public ContasBancariasControllerIntegrationTests()
    {
        _command = _fixture.Build<AberturaContaCommand>()
            .Create();
    }

    [Fact]
    public async Task Menor_De_Idade()
    {
        var command = _command with { Age = 17 };
        // Arrange
        var content = new StringContent(JsonSerializer.Serialize(command),
            Encoding.UTF8, "application/json");
        // Act
        var result = await Client.PostAsync(_url, content);
        // Assert
        Assert.Equal(422, (int)result.StatusCode);
    }

    [Fact]
    public async Task Sem_Nome()
    {
        var command = _command with { Name = string.Empty };
        // Arrange
        var content = new StringContent(JsonSerializer.Serialize(command),
            Encoding.UTF8, "application/json");
        // Act
        var result = await Client.PostAsync(_url, content);
        // Assert
        Assert.Equal(422, (int)result.StatusCode);
    }

    [Fact]
    public async Task Sem_Email()
    {
        var command = _command with { Email = string.Empty };
        // Arrange
        var content = new StringContent(JsonSerializer.Serialize(command),
            Encoding.UTF8, "application/json");
        // Act
        var result = await Client.PostAsync(_url, content);
        // Assert
        Assert.Equal(422, (int)result.StatusCode);
    }

    [Fact]
    public async Task Com_Id()
    {
        var command = _command with { Id = 0 };
        // Arrange
        var content = new StringContent(JsonSerializer.Serialize(command),
            Encoding.UTF8, "application/json");
        // Act
        var result = await Client.PostAsync(_url, content);
        // Assert
        Assert.Equal(422, (int)result.StatusCode);
    }
}