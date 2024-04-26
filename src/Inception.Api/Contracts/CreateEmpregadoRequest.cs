using Inception.Api.Features.Empregados.Create;
using Swashbuckle.AspNetCore.Annotations;

namespace Inception.Api.Contracts;

[SwaggerSchemaFilter(typeof(CreateEmpregadoRequestFilter))]
public record CreateEmpregadoRequest
{
    public string Nome { get; init; }
    public DateTime Nascimento { get; init; }
    public int Inscricao { get; init; }
}
