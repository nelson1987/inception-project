using Inception.Application.Features.ContaBancarias.Abertura;
using Swashbuckle.AspNetCore.Filters;

namespace Inception.Api.Features.ContasBancarias.Abertura;

public class AberturaContaCommandExample : IMultipleExamplesProvider<AberturaContaCommand>
{
    public IEnumerable<SwaggerExample<AberturaContaCommand>> GetExamples()
    {
        yield return SwaggerExample.Create("Maior de idade", new AberturaContaCommand()
        {
            Id = 1,
            Name = "Nome",
            Email = "maiordeidade@email.com",
            Age = 18
        });
        yield return SwaggerExample.Create("Menor de idade", new AberturaContaCommand()
        {
            Id = 1,
            Name = "Nome",
            Email = "maiordeidade@email.com",
            Age = 17
        });
    }
}
