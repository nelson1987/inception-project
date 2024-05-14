using InceptionClean.Application.Abstractions;
using InceptionClean.Infrastructure.DbContexts;
using InceptionClean.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InceptionClean.Infrastructure;
public static class Dependencies
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //var assembly = typeof(DependencyInjection).Assembly;

        //services.AddMediatR(configuration =>
        //    configuration.RegisterServicesFromAssembly(assembly));

        //services.AddValidatorsFromAssembly(assembly);
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        var cs = configuration.GetConnectionString("Default");
        services.AddDbContext<PersonDbContext>(opt => opt.UseSqlServer(cs));

        //var cs = configuration.GetConnectionString("Default");
        services.AddDbContext<UserDbContext>(opt => opt.UseSqlServer(cs));
        return services;
    }
}
