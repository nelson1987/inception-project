using Inception.Api.Features.Empregados.Update;
using Swashbuckle.AspNetCore.Annotations;

namespace Inception.Api.Contracts;

[SwaggerSchemaFilter(typeof(PutEmpregadoRequestFilter))]
public record PutEmpregadoRequest
{
    public required string Nome { get; init; }
    public DateTime Nascimento { get; init; }
    public int Inscricao { get; init; }
}