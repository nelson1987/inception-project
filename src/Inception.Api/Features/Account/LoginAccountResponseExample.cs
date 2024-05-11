using Inception.Api.Features.Account.Login;
using Swashbuckle.AspNetCore.Filters;

namespace Inception.Api.Features.Account;

public class LoginAccountResponseExample : IMultipleExamplesProvider<LoginAccountResponse>
{
    public IEnumerable<SwaggerExample<LoginAccountResponse>> GetExamples()
    {
        yield return SwaggerExample.Create("Manager", 
            new LoginAccountResponse(1, "batman","manager","Token"));
        yield return SwaggerExample.Create("Employee", 
            new LoginAccountResponse(2, "robin", "employee", "Token"));
    }
}