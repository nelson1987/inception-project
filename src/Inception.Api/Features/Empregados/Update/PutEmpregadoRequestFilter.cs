using Inception.Api.Contracts;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Inception.Api.Features.Empregados.Update;

public class PutEmpregadoRequestFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        PutEmpregadoRequest exemplo = new PutEmpregadoRequest();
        schema.Example = new OpenApiObject
        {
            [nameof(exemplo.Nome)] = new OpenApiString("An awesome product"),
            [nameof(exemplo.Nascimento)] = new OpenApiDateTime(DateTime.Now),
            [nameof(exemplo.Inscricao)] = new OpenApiInteger(1),
        };
    }
}
