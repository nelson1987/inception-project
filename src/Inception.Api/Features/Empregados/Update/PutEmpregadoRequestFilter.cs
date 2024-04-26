using Inception.Api.Contracts;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Inception.Api.Features.Empregados.Update;

public class PutEmpregadoRequestFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        PutEmpregadoRequest exemplo = new PutEmpregadoRequest()
        {
            Nome = "An awesome product",
            Nascimento = DateTime.UtcNow,
            Inscricao = 1
        };
        schema.Example = new OpenApiObject
        {
            [nameof(exemplo.Nome)] = new OpenApiString(exemplo.Nome),
            [nameof(exemplo.Nascimento)] = new OpenApiDateTime(exemplo.Nascimento),
            [nameof(exemplo.Inscricao)] = new OpenApiInteger(exemplo.Inscricao)
        };
    }
}
