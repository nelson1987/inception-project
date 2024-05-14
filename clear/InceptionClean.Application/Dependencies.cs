using FluentValidation;
using InceptionClean.Application.Behaviors;
using InceptionClean.Application.Features.Persons.Create;
using InceptionClean.Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InceptionClean.Application;
public static class Dependencies
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //var assembly = typeof(DependencyInjection).Assembly;

        //services.AddMediatR(configuration =>
        //    configuration.RegisterServicesFromAssembly(assembly));

        //services.AddValidatorsFromAssembly(assembly);
        //services.AddMediatR(x => typeof(CreatePerson));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped<IRequest<Person>, CreatePerson>();
        services.AddScoped<IValidator<CreatePerson>, CreatePersonValidator>();
        services.AddScoped<IRequestHandler<CreatePerson, Person>, CreatePersonHandler>();
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(MyPipelineBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
