using Inception.Api.Features.ContasBancarias.Abertura;
using Inception.Application.Features.ContaBancarias.Abertura;
using Swashbuckle.AspNetCore.Filters;

namespace Inception.Api.Features.ContasBancarias;

public static class Dependencies
{
    public static IServiceCollection AddContaBancariaEnpoints(this IServiceCollection services)
    {
        services.AddScoped<IMultipleExamplesProvider<AberturaContaCommand>, AberturaContaCommandExample>();
        return services;
    }
}
