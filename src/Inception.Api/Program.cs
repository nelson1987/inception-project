using Inception.Api.Configurations;
using Inception.Core.Features.ContaBancarias;
using Inception.Database;
using Inception.Infrastructure.Persistence;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfrastructure(builder.Configuration)
                //.AddApplication()
                .AddDatabase(builder.Configuration)
                .AddContaBancaria()
                .AddAuthentication();
builder.Services.AddCors();
builder.Services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGeneration()
                .AddRateLimit();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
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
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error",
                Error = contextFeature.Error.Message
            });
        }
    });
});
app.UseRateLimiter();
app.Run();

public partial class Program() { }