using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inception.Database;

public static class Dependencies
{
    public static IServiceCollection ConfigureContexts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<InceptionDbContext>(options =>
        {
            options.UseMongoDB(configuration.GetConnectionString("DefaultConnection"), "sales");
        });
        return services;
    }
}