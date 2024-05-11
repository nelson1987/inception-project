using FluentValidation;
using Inception.Application.Features.ContaBancarias.Abertura;
using Microsoft.Extensions.DependencyInjection;

namespace Inception.Application.Features.ContaBancarias;

public static class Dependencies
{
    public static IServiceCollection AddContaBancaria(this IServiceCollection services)
    {
        services.AddScoped<IValidator<AberturaContaCommand>, AberturaContaCommandValidator>();
        services.AddScoped<IAberturaContaHandler, AberturaContaHandler>();
        return services;
    }
}