using Inception.Api.Features.ContasBancarias;
using Microsoft.AspNetCore.Mvc;

namespace Inception.Api.Features.Usuarios;
public record CriacaoUsuarioCommand
{
    public string Description { get; set; }
}

public interface IUsuarioService
{
    Task Handle(CriacaoUsuarioCommand command, CancellationToken cancellationToken = default);
}

public class UsuarioService : IUsuarioService
{
    public Task Handle(CriacaoUsuarioCommand command, CancellationToken cancellationToken = default)
    {
        //throw new NotImplementedException();
        return Task.CompletedTask;
    }
}

public static class Dependencies
{
    public static IServiceCollection AddUsuarioInjection(this IServiceCollection services)
    {
        services.AddScoped<IUsuarioService, UsuarioService>();
        return services;
    }
}

[Route("api/[controller]")]
public class UsuariosController : DefaulController
{
    public UsuariosController(ILogger<UsuariosController> logger)
        : base(logger)
    {
    }

    //POST
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CriacaoUsuarioCommand request,
    [FromServices] IUsuarioService service,
    //[FromServices] IValidator<AberturaContaCommand> validator,
    CancellationToken cancellationToken = default)
    {
        //var validationResult = await validator.ValidateAsync(request, cancellationToken);
        //if (!validationResult.IsValid)
        //    return UnprocessableEntity(validationResult.ToModelState());

        //if (!ModelState.IsValid)
        //    return BadRequest(ModelState);

        await service.Handle(request, cancellationToken);

        //return Unauthorized();
        return Created();
    }
}