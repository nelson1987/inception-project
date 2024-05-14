using AutoFixture;
using AutoFixture.AutoMoq;
using Inception.Application.Features.LoginAccount;
using Inception.Core.Entities;
using Inception.Infrastructure.Persistence.Repositories;
using Moq;

namespace Inception.Tests.UnitTests;
public class LoginAccountHandlerTests
{
    private readonly IFixture _fixture = new Fixture().Customize(new AutoMoqCustomization());
    private readonly CancellationToken _token = CancellationToken.None;
    private readonly User _usuario;
    private readonly LoginAccountHandler _handler;
    private readonly LoginAccountCommand _command;

    public LoginAccountHandlerTests()
    {
        _command = _fixture.Build<LoginAccountCommand>()
            .Create();
        _usuario = _fixture.Build<User>()
            .Create();
        _fixture.Freeze<Mock<IUserRepository>>()
            .Setup(x => x.Get(It.IsAny<string>(), It.IsAny<string>(), _token))
            .ReturnsAsync(_usuario);
        _handler = _fixture.Create<LoginAccountHandler>();
    }

    [Fact]
    public async Task Dado_Usuario_Existente_Retona_Sucesso()
    {
        var result = await _handler.Login(_command, _token);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Dado_Usuario_Inexistente_Retona_Falha()
    {
        _fixture.Freeze<Mock<IUserRepository>>()
            .Setup(x => x.Get(It.IsAny<string>(), It.IsAny<string>(), _token))
            .ReturnsAsync((User?)null);
        var result = await _handler.Login(_command, _token);
        Assert.True(result.IsFailed);
        Assert.Equal("Usuário ou senha inválidos", result.Errors[0].Message);
    }
}
