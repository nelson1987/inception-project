using Microsoft.AspNetCore.Diagnostics;

namespace InceptionClean.Api.Configurations
{
    public static class ExceptionHandler
    {
        public static IApplicationBuilder AddExceptionHandler(this IApplicationBuilder app) 
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature is not null)
                    {
                        await context.Response.WriteAsJsonAsync(new
                        {
                            context.Response.StatusCode,
                            Message = "Internal Server Error",
                            Error = contextFeature.Error.Message
                        });
                    }
                });
            });
            return app;
        }
    }
}
