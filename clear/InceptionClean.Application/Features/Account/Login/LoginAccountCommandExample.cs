using Swashbuckle.AspNetCore.Filters;

namespace InceptionClean.Application.Features.Account.Login;

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