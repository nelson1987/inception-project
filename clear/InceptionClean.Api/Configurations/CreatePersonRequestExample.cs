using InceptionClean.Application.Features.Persons.Create;
using Swashbuckle.AspNetCore.Filters;

namespace InceptionClean.Api.Configurations;

public class CreatePersonRequestExample : IMultipleExamplesProvider<CreatePersonRequest>
{
    public IEnumerable<SwaggerExample<CreatePersonRequest>> GetExamples()
    {
        yield return SwaggerExample.Create("Manager", new CreatePersonRequest()
        {
            Name = "batman",
            Email = "batman@email.com"
        });
        yield return SwaggerExample.Create("Employee", new CreatePersonRequest()
        {
            Name = "robin",
            Email = "robin@email.com"
        });
    }
}