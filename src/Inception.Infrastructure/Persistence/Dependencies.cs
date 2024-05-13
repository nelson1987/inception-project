using Inception.Core.Repositories;
using Inception.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Inception.Infrastructure.Persistence;

public static class Dependencies
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureRepositories()
                .ConfigureContexts(configuration);
        return services;
    }

    private static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }

    private static IServiceCollection ConfigureContexts(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoClient = new MongoClient(configuration.GetConnectionString("DefaultConnection"));

        var dbContextOptions =
            new DbContextOptionsBuilder<InceptionDbContext>().UseMongoDB(mongoClient, "sales");

        services.AddSingleton<IInceptionDbContext>(new InceptionDbContext(dbContextOptions.Options));

        return services;
    }
}