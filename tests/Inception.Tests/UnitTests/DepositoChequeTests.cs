namespace Inception.Tests.UnitTests
{
    public class DepositoChequeTests
    {

        [Fact]
        public async Task Dado_Cheque_Valido_Depositar_Retona_Sucesso()
        {
            //var cliente = "Sra. Silva";
            //var valorCheque = 200.00M;

            //Assert.True(result.IsSuccess);
        }
    }
    public record Cliente
    {
    }
    public record Cheque(decimal valor, string bancoEmissor, string agencia, string conta, string numeroCheque)
    {
        public bool IsAuthenticated()
        {
            throw new NotImplementedException();
        }
    }
    public record Transacao(DateTime Data, decimal Valor);
    public class DepositarChequeHandler
    {
        public bool Init(Cliente cliente, Cheque depositado)
        {
            if (!depositado.IsAuthenticated()) throw new NotImplementedException();

            return true;
        }
    }
}
/*
 
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
 */