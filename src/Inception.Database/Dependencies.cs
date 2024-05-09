﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Inception.Database;

public static class Dependencies
{
    public static IServiceCollection ConfigureContexts(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoClient = new MongoClient(configuration.GetConnectionString("DefaultConnection"));

        var dbContextOptions =
            new DbContextOptionsBuilder<InceptionDbContext>().UseMongoDB(mongoClient, "sales");

        //var db = new InceptionDbContext(dbContextOptions.Options);

        //services.AddDbContext<InceptionDbContext>(options =>
        //{
        //    options.UseMongoDB(configuration.GetConnectionString("DefaultConnection"), "sales");
        //});

        services.AddSingleton<IInceptionDbContext>(new InceptionDbContext(dbContextOptions.Options));

        return services;
    }
}