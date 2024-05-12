

namespace Inception.Tests;

public class ContaFactoryUnitTests
{
    [Fact]
    public void Dado_os_Dados_Necessario_Abrir_Conta()
    {
        //Arrange
        var documentoValido = "1234567890";
        //Act
        Conta contaGerada = ContaFactory.Generate(documentoValido);
        //Assert
        Assert.Equal(documentoValido, contaGerada.Documento);
        Assert.NotNull(contaGerada.Numero);
        Assert.NotNull(contaGerada.Agencia);
        Assert.NotNull(contaGerada.CodigoCompe);
    }
}
public class ContaApplicationServiceUnitTests
{
    [Fact]
    public void Dado_os_Dados_Necessario_Abrir_Conta()
    {
        //Arrange
        var documentoValido = "1234567890";
        //Act
        var servico = new ContaApplicationService();
        Conta contaGerada = servico.AbrirConta(documentoValido);
        //Assert
        Assert.Equal(documentoValido, contaGerada.Documento);
        Assert.NotNull(contaGerada.Numero);
        Assert.NotNull(contaGerada.Agencia);
        Assert.NotNull(contaGerada.CodigoCompe);
    }
    [Fact]
    public void Dado_que_nao_foi_possivel_gerar_numero_conta_retorna_erro()
    {
    }
    [Fact]
    public void Dado_que_o_numero_conta_ja_existe_retorna_erro()
    {
    }
    [Fact]
    public void Dado_que_ja_existe_conta_com_esse_documento_retorna_erro()
    {
    }
}
public class ContaApplicationService
{
    public Conta AbrirConta(string documento)
    {
        //Gerar NumeroConta
        var fabricante = ContaFactory.Generate(documento);
        return fabricante;
        //Validar se já há conta para esse documento
        //Salvar Conta em Repositorio
    }
}
public static class ContaFactory
{
    static internal Conta Generate(string documentoValido)
    {
        return new Conta
        {
            Documento = documentoValido,
            Numero = "123",
            Agencia = "Alfandega",
            CodigoCompe = "391"
        };
    }
}
public class Conta
{
    public required string Documento { get; set; }
    public required string Numero { get; set; }
    public required string Agencia { get; set; }
    public required string CodigoCompe { get; set; }
}