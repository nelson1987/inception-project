using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace Inception.Api.Configurations;

public static class RateLimit
{
    public static IServiceCollection AddRateLimit(this IServiceCollection services)
    {
        services.AddRateLimiter(rateLimiterOptions =>
        {
            rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            rateLimiterOptions.AddTokenBucketLimiter("token", options =>
            {
                options.TokenLimit = 1000;
                options.ReplenishmentPeriod = TimeSpan.FromHours(1);
                options.TokensPerPeriod = 700;
                options.AutoReplenishment = true;
            });

            rateLimiterOptions.AddPolicy("fixed-by-ip", httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
                    //partitionKey: httpContext.User.Identity?.Name?.ToString(),
                    //httpContext.Request.Headers["X-Forwarded-For"].ToString(),
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 10,
                        Window = TimeSpan.FromMinutes(1)
                    }));
        });
        return services;
    }
}
