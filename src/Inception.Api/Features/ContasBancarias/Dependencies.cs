using FluentValidation;
using Inception.Api.Features.ContasBancarias.Abertura;
using Inception.Core.Features.ContaBancarias.Abertura;
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
