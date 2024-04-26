using Inception.Api.Features.Enderecos;

namespace Inception.Api.Features.Empregados;

public record Empregado
{
    public int Id { get; init; }
    public string Nome { get; init; }
    public DateTime Nascimento { get; init; }
    public int Inscricao { get; init; }
    public decimal Salario { get; init; }
    //public Funcao MyProperty { get; init; }
    public Endereco Endereco { get; init; }
}