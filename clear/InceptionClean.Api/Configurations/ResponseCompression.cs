using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

namespace InceptionClean.Api.Configurations;

public static class ResponseCompression
{
    public static IServiceCollection AddCompression(this IServiceCollection services)
    {
        services.Configure<GzipCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Optimal;
        });

        services.AddResponseCompression(options =>
        {
            options.Providers.Add<GzipCompressionProvider>();
        });
        return services;
    }
}