using FluentValidation;
using Inception.Api.Contracts;
using Inception.Api.Features.ContasBancarias;
using Inception.Api.Features.Empregados;
using Inception.Api.Features.Empregados.Create;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.RateLimiting;
using Swashbuckle.AspNetCore.Filters;
using System.Threading.RateLimiting;
using Inception.Api.Configurations;
using Inception.Infrastructure.Persistence;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging();
builder.Services.AddInfrastructure(builder.Configuration);
//.AddApplication()
//.AddContaBancaria()
//.AddAuthentication();
builder.Services.AddCors();
builder.Services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGeneration()
                .AddRateLimit();
//builder.Services.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
//              .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
//              .MinimumLevel.Override("System", LogEventLevel.Warning)
//              .ReadFrom.Configuration(hostingContext.Configuration)
//              .Enrich.FromLogContext());
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
                context.Response.StatusCode,
                Message = "Internal Server Error",
                Error = contextFeature.Error.Message
            });
        }
    });
});
app.UseRateLimiter();
app.Run();

public partial class Program() { }