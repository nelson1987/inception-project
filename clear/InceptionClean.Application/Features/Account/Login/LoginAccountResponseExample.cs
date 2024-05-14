using Swashbuckle.AspNetCore.Filters;

namespace InceptionClean.Application.Features.Account.Login;

public class LoginAccountResponseExample : IMultipleExamplesProvider<LoginAccountResponse>
{
    public IEnumerable<SwaggerExample<LoginAccountResponse>> GetExamples()
    {
        yield return SwaggerExample.Create("Manager",
            new LoginAccountResponse(1, "batman", "manager", "Token"));
        yield return SwaggerExample.Create("Employee",
            new LoginAccountResponse(2, "robin", "employee", "Token"));
    }
}