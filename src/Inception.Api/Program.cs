using Inception.Api.Configurations;
using Inception.Api.Features.Account;
using Inception.Infrastructure.Persistence;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddCors();
builder.Services.AddUserAuthentication();
builder.Services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddEndpointsApiExplorer();

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

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Optimal;
});

builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<GzipCompressionProvider>();
});
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
public partial class Program { }