using FluentValidation;
using Inception.Api.Features.ContasBancarias.Abertura;
using Swashbuckle.AspNetCore.Filters;

namespace Inception.Api.Features.ContasBancarias;

public static class Dependencies
{
    public static IServiceCollection AddContaBancaria(this IServiceCollection services)
    {
        services.AddScoped<IValidator<AberturaContaCommand>, AberturaContaCommandValidator>();
        services.AddScoped<IMultipleExamplesProvider<AberturaContaCommand>, AberturaContaCommandExample>();
        services.AddScoped<IAberturaContaHandler, AberturaContaHandler>();
        return services;
    }
}
