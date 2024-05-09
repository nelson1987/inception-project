using Inception.Core.Features.ContaBancarias.Abertura;

namespace Inception.Core.Features.ContaBancarias;

public static class Dependencies
{
    public static IServiceCollection AddContaBancaria(this IServiceCollection services)
    {
        services.AddScoped<IValidator<AberturaContaCommand>, AberturaContaCommandValidator>();
        services.AddScoped<IAberturaContaHandler, AberturaContaHandler>();
        return services;
    }
}