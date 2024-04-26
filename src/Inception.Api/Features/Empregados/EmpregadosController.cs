using FluentValidation;
using Inception.Api.Contracts;
using Inception.Api.Features.Empregados.Create;
using Inception.Api.Features.Empregados.EmpregadosDelete;
using Inception.Api.Features.Empregados.GetAll;
using Inception.Api.Features.Empregados.GetById;
using Inception.Api.Features.Empregados.Update;
using Inception.Api.ResponseHandlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace Inception.Api.Features.Empregados;
public class CreateEmpregadoValidator : AbstractValidator<CreateEmpregadoRequest> 
{
    public CreateEmpregadoValidator()
    {
        RuleFor(x => x.Inscricao).GreaterThan(0);
        RuleFor(x => x.Nome).NotEmpty();
    }
}

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[SwaggerTag("Create, read, update and delete Empregados")]
public class EmpregadosController : ControllerBase
{
    private readonly ILogger<EmpregadosController> _logger;
    private readonly IAppDbContext _context;

    public EmpregadosController(ILogger<EmpregadosController> logger, IAppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    //GETALL
    [HttpGet(Name = "Get All Empregado")]
    [SwaggerOperation("Get all empregado", "Requires admin privileges")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(200, "The product was created")]
    [SwaggerResponse(400, "The product was created")]
    [SwaggerResponse(500, "The product was created")]
    public async Task<ActionResult> GetAll([FromServices] IEmpregadoGetAllIdHandler handler, CancellationToken cancellationToken = default)
    {
        //return Unauthorized();
        return Ok(await _context.Produtos.ToListAsync(cancellationToken));
    }

    //GETBYID
    [HttpGet("{id:int}", Name = "Get Empregado By Id")]
    [SwaggerOperation("Get empregado by id", "Requires admin privileges")]
    [ProducesResponseType(typeof(Empregado), 200)]
    [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [SwaggerResponse(200, "The product was created", typeof(Empregado))]
    [SwaggerResponse(400, "The product was created", typeof(IDictionary<string, string>))]
    [SwaggerResponse(404, "The product was created")]
    [SwaggerResponse(500, "The product was created")]
    public async Task<ActionResult> GetById([FromRoute] int id, [FromServices] IEmpregadoGetByIdHandler handler, CancellationToken cancellationToken = default)
    {
        //return Unauthorized();
        //return NotFound();
        return Ok(await _context.Produtos.FirstOrDefaultAsync(x => x.Id == id, cancellationToken));
    }

    //POST
    [HttpPost(Name = "Create Empregado")]
    [SwaggerOperation("Creates a new empregado", "Requires admin privileges")]
    [ProducesResponseType(201)]
    [SwaggerResponse(201, "The product was created")]
    [ProducesResponseType(typeof(IDictionary<string, string>), 422)]
    [SwaggerResponse(422, "The product was created", typeof(IDictionary<string, string>))]
    public async Task<ActionResult> Create([FromBody, BindRequired] CreateEmpregadoRequest empregado,
        [FromServices] IEmpregadoCreateHandler handler,
        [FromServices] IValidator<CreateEmpregadoRequest> validator,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(empregado, cancellationToken);
        if (!validationResult.IsValid)
            return UnprocessableEntity(validationResult.ToDictionary());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        //return Unauthorized();
        return Created();
    }

    //PUT
    [HttpPut("{id:int}", Name = "Update Empregado")]
    [SwaggerOperation("Update a Empregado", "Requires admin privileges")]
    [ProducesResponseType(typeof(PutEmpregadoResponse), 201)]
    [SwaggerResponse(201, "The product was created", typeof(PutEmpregadoResponse))]
    public async Task<ActionResult> Update([FromRoute] int id, [FromBody, BindRequired] PutEmpregadoRequest empregado, [FromServices] IEmpregadoUpdateHandler handler, CancellationToken cancellationToken = default)
    {
        //return Unauthorized();
        //return NotFound();
        return NoContent();
    }

    //DELETE
    [HttpDelete(Name = "Delete Empregado")]
    [SwaggerOperation("Delete a Empregado", "Requires admin privileges")]
    [ProducesResponseType(204)]
    [SwaggerResponse(204, "The empregado was deleted")]
    public async Task<ActionResult> Delete([FromRoute] int id, [FromServices] IEmpregadoDeleteHandler handler, CancellationToken cancellationToken = default)
    {
        //return Unauthorized();
        Response response = await handler.Handle(cancellationToken);
        if (response is NotFoundResponse) return NotFound();
        return NoContent();
    }
}