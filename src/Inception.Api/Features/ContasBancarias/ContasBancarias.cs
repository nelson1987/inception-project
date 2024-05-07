using FluentValidation;
using Inception.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Inception.Api.Features.ContasBancarias;

[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public class DefaultController : ControllerBase
{
    private readonly ILogger _logger;

    public DefaultController(ILogger logger)
    {
        _logger = logger;
    }
}

[Route("api/[controller]")]
[SwaggerTag("Create, read, update and delete Empregados")]
public class ContasBancarias : DefaultController
{
    private readonly ILogger<ContasBancarias> _logger;

    public ContasBancarias(ILogger<ContasBancarias> logger) : base(logger)
    {
        _logger = logger;
    }

    //POST
    [HttpPost(Name = "Abertura de conta Bancária [controller]")]
    [SwaggerOperation("Abra uma nova conta", "Requires admin privileges")]
    [SwaggerRequestExample(typeof(AberturaContaCommand), typeof(AberturaContaCommandExample))]
    [ProducesResponseType(201)]
    [SwaggerResponse(201, "A conta foi criada com sucesso.")]
    [SwaggerResponseExample(201, typeof(CreatedResult))]
    public async Task<ActionResult> Create([FromBody, BindRequired] AberturaContaCommand request,
    [FromServices] IAberturaContaHandler handler,
    [FromServices] IValidator<AberturaContaCommand> validator,
    CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return UnprocessableEntity(validationResult.ToModelState());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await handler.Handle(request, cancellationToken);

        //return Unauthorized();
        return Created();
    }
}

public static class Dependencies
{
    public static IServiceCollection AddContaBancaria(this IServiceCollection services)
    {
        //services.AddFluentValidationAutoValidation();
        services.AddScoped<IValidator<AberturaContaCommand>, AberturaContaCommandValidator>();
        services.AddScoped<IMultipleExamplesProvider<AberturaContaCommand>, AberturaContaCommandExample>();
        services.AddScoped<IAberturaContaHandler, AberturaContaHandler>();
        return services;
    }
}

public record AberturaContaCommand
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public int Age { get; init; }
};

public class AberturaContaCommandValidator : AbstractValidator<AberturaContaCommand>
{
    public AberturaContaCommandValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Name).Length(0, 10);
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Age).InclusiveBetween(18, 60);
    }
};

public class AberturaContaCommandExample : IMultipleExamplesProvider<AberturaContaCommand>
{
    public IEnumerable<SwaggerExample<AberturaContaCommand>> GetExamples()
    {
        yield return SwaggerExample.Create("Maior de idade", new AberturaContaCommand()
        {
            Id = 1,
            Name = "Nome",
            Email = "maiordeidade@email.com",
            Age = 18
        });
        yield return SwaggerExample.Create("Menor de idade", new AberturaContaCommand()
        {
            Id = 1,
            Name = "Nome",
            Email = "maiordeidade@email.com",
            Age = 17
        });
    }
}

public interface IAberturaContaHandler
{
    Task Handle(AberturaContaCommand command, CancellationToken cancellationToken = default);
}

public class AberturaContaHandler : IAberturaContaHandler
{
    public Task Handle(AberturaContaCommand command, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}