using Inception.Api.Features.Empregados.Create;
using Inception.Api.Features.Empregados.Update;
using Swashbuckle.AspNetCore.Annotations;

namespace Inception.Api.Contracts;
[SwaggerSchemaFilter(typeof(CreateEmpregadoRequestFilter))]
public record CreateEmpregadoRequest
{
    public string Nome { get; init; }
    public DateTime Nascimento { get; init; }
    public int Inscricao { get; init; }
}

[SwaggerSchemaFilter(typeof(PutEmpregadoRequestFilter))]
public record PutEmpregadoRequest
{
    public string Nome { get; init; }
    public DateTime Nascimento { get; init; }
    public int Inscricao { get; init; }
}

public class PutEmpregadoResponse
{
}
