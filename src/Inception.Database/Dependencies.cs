using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inception.Database;

/*
 
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=Macoratti;Initial Catalog=InventarioDB;Integrated Security=True"
  }
}
 */
public static class Dependencies 
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        return services;
    }
}
