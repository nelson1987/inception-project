using FluentValidation;
using Inception.Api.Extensions;
using Inception.Api.Features.ContasBancarias.Abertura;
using Inception.Core.Features.ContaBancarias.Abertura;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Inception.Api.Features.ContasBancarias;

[Route("api/[controller]")]
[SwaggerTag("Create, read, update and delete Empregados")]
public class ContasBancariasController : DefaultController
{
    private readonly ILogger<ContasBancariasController> _logger;

    public ContasBancariasController(ILogger<ContasBancariasController> logger) : base(logger)
    {
        _logger = logger;
    }

    //POST
    [HttpGet]
    [Authorize(Roles = "manager")]
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

        return Created();
    }
}