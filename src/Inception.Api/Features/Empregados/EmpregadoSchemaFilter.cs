using Inception.Database;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Inception.Api.Features.Empregados;

public class EmpregadoSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        Empregado exemplo = new Empregado();
        schema.Example = new OpenApiObject
        {
            [nameof(exemplo.Nome)] = new OpenApiString("An awesome product"),
            [nameof(exemplo.Nascimento)] = new OpenApiDateTime(DateTime.Now),
            [nameof(exemplo.Inscricao)] = new OpenApiInteger(1),
        };
    }
}