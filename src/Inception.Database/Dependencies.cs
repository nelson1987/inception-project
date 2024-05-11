using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Inception.Database;

public static class Dependencies
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoClient = new MongoClient(configuration.GetConnectionString("DefaultConnection"));

        var dbContextOptions =
            new DbContextOptionsBuilder<InceptionDbContext>().UseMongoDB(mongoClient, "sales");

        services.AddSingleton<IInceptionDbContext>(new InceptionDbContext(dbContextOptions.Options));

        return services;
    }
}