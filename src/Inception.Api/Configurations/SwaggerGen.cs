using Inception.Api.Features.Account;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Inception.Api.Configurations;

public static class SwaggerGen
{
    public static IServiceCollection AddSwaggerGeneration(this IServiceCollection services)
    {
        services.AddSwaggerExamplesFromAssemblyOf(typeof(LoginAccountCommandExample));
        services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();
            c.ExampleFilters();
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
                "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                "Example: \"Bearer 1safsfsdfdfd\"",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                new OpenApiSecurityScheme {
                    Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                    }
                    },
                    new string[] {}
                }
            });
        });
        return services;
    }
}
