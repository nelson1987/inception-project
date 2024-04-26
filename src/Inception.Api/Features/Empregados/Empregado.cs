using Inception.Api.Features.Enderecos;
using Swashbuckle.AspNetCore.Annotations;

namespace Inception.Api.Features.Empregados;

[SwaggerSchemaFilter(typeof(EmpregadoSchemaFilter))]
public record Empregado
{
    public string Nome { get; init; }
    public DateTime Nascimento { get; init; }
    public int Inscricao { get; init; }
    public decimal Salario { get; init; }
    public Funcao MyProperty { get; init; }
    public Endereco Endereco { get; init; }
}

public enum Funcao
{
    Operario = 1,
    Supervisor = 2,
    Gerente = 3
}