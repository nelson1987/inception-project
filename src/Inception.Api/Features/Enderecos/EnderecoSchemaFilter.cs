//using Microsoft.OpenApi.Any;
//using Microsoft.OpenApi.Models;
//using Swashbuckle.AspNetCore.SwaggerGen;

//namespace Inception.Api.Features.Enderecos;

//public class EnderecoSchemaFilter : ISchemaFilter
//{
//    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
//    {
//        Endereco exemplo = new Endereco();
//        exemplo.Rua = "Rua Endereço";
//        exemplo.Numero = 100;
//        schema.Example = new OpenApiObject
//        {
//            [nameof(exemplo.Rua)] = new OpenApiString(exemplo.Rua),
//            [nameof(exemplo.Numero)] = new OpenApiInteger(exemplo.Numero),
//        };
//    }
//}