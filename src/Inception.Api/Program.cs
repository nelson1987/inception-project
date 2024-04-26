using FluentValidation;
using FluentValidation.AspNetCore;
using Inception.Api.Contracts;
using Inception.Api.Features.Empregados.Create;

var builder = WebApplication.CreateBuilder(args);

// https://github.com/domaindrivendev/Swashbuckle.AspNetCore

builder.Services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<CreateEmpregadoRequest>, CreateEmpregadoValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();