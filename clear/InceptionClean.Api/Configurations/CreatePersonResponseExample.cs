using Swashbuckle.AspNetCore.Filters;

namespace InceptionClean.Api.Configurations;
public record CreatePersonResponse(int Id, string Name, string Fuction, string Token);
public class CreatePersonResponseExample : IMultipleExamplesProvider<CreatePersonResponse>
{
    public IEnumerable<SwaggerExample<CreatePersonResponse>> GetExamples()
    {
        yield return SwaggerExample.Create("Manager",
            new CreatePersonResponse(1, "batman", "manager", "Token"));
        yield return SwaggerExample.Create("Employee",
            new CreatePersonResponse(2, "robin", "employee", "Token"));
    }
}