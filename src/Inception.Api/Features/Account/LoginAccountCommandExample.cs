using Inception.Api.Features.Account.Login;
using Swashbuckle.AspNetCore.Filters;

namespace Inception.Api.Features.Account;

public class LoginAccountCommandExample : IMultipleExamplesProvider<LoginAccountCommand>
{
    public IEnumerable<SwaggerExample<LoginAccountCommand>> GetExamples()
    {
        yield return SwaggerExample.Create("Manager", new LoginAccountCommand()
        {
            Username = "batman",
            Password = "batman"
        });
        yield return SwaggerExample.Create("Employee", new LoginAccountCommand()
        {
            Username = "robin",
            Password = "robin"
        });
    }
}