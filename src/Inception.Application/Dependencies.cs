using FluentValidation;
using Inception.Application.Features.LoginAccount;
using Microsoft.Extensions.DependencyInjection;

namespace Inception.Application;

public static class Dependencies
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.ConfigureLoginAccount();
        return services;
    }
    private static IServiceCollection ConfigureLoginAccount(this IServiceCollection services)
    {
        services.AddScoped<IValidator<LoginAccountCommand>, LoginAccountCommandValidator>()
                .AddScoped<ILoginAccountHandler, LoginAccountHandler>();
        return services;
    }
}