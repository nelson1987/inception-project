using FluentValidation;
using Inception.Api.Contracts;
using Inception.Api.Features.Account;
using Inception.Api.Features.Empregados.Create;
using Microsoft.OpenApi.Models;
using Inception.Api.Features.ContasBancarias;
using Inception.Api.Features.Empregados;
using Inception.Api.Features.Empregados.Create;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
builder.Services.AddContaBancaria();
// https://github.com/domaindrivendev/Swashbuckle.AspNetCore

builder.Services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { 
    c.EnableAnnotations();
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
//builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddSwaggerExamplesFromAssemblyOf(typeof(WeatherForecastRequestExample));
builder.Services.AddSwaggerGen(c =>
{
    c.ExampleFilters();
    c.EnableAnnotations();
});

builder.Services.AddScoped<IValidator<CreateEmpregadoRequest>, CreateEmpregadoValidator>();
builder.Services.AddUserAuthentication();

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
app.Run();