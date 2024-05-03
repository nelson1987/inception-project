using FluentValidation;
using Inception.Api.Contracts;
using Inception.Api.Extensions;
using Inception.Api.Features.Empregados.Create;
using Inception.Api.Features.Empregados.Delete;
using Inception.Api.Features.Empregados.GetById;
using Inception.Api.Features.Empregados.Update;
using Inception.Api.ResponseHandlers;
using Inception.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.RateLimiting;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace Inception.Api.Features.Empregados;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
[EnableRateLimiting("fixed-by-ip")]
[SwaggerTag("Create, read, update and delete Empregados")]
public class EmpregadosController : ControllerBase
{
    private readonly ILogger<EmpregadosController> _logger;

    public EmpregadosController(ILogger<EmpregadosController> logger)
    {
        _logger = logger;
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
    public async Task<ActionResult> GetAll(//[FromServices] IEmpregadoGetAllIdHandler handler,
    CancellationToken cancellationToken = default)
    {
        string input = "codemaze is awesome";
        var reverse = new StringBuilder(input.Length);
        for (int i = input.Length - 1; i >= 0; i--)
        {
            reverse.Append(input[i]);
        }
        Thread.Sleep(500);
        //return Unauthorized();
        return Ok(reverse.ToString());// await _context.Produtos.ToListAsync(cancellationToken));
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
    public async Task<ActionResult> GetById([FromRoute] int id,
    [FromServices] IEmpregadoGetByIdHandler handler,
    CancellationToken cancellationToken = default)
    {
        //return Unauthorized();
        //return NotFound();
        return Ok();//await _context.Produtos.FirstOrDefaultAsync(x => x.Id == id, cancellationToken));
    }

    //POST
    [HttpPost(Name = "Create Empregado")]
    [SwaggerOperation("Creates a new empregado", "Requires admin privileges")]
    [ProducesResponseType(201)]
    [SwaggerResponse(201, "The product was created")]
    [ProducesResponseType(typeof(IDictionary<string, string>), 422)]
    [SwaggerResponse(422, "The product was created", typeof(IDictionary<string, string>))]
    [SwaggerResponseExample(200, typeof(WeatherForecastResponseExample))]
    [SwaggerRequestExample(typeof(Empregado), typeof(WeatherForecastRequestExample))]
    public async Task<ActionResult> Create([FromBody, BindRequired] CreateEmpregadoRequest request,
    [FromServices] IEmpregadoCreateHandler handler,
    [FromServices] IValidator<CreateEmpregadoRequest> validator,
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

    //PUT
    [HttpPut("{id:int}", Name = "Update Empregado")]
    [SwaggerOperation("Update a Empregado", "Requires admin privileges")]
    [ProducesResponseType(typeof(PutEmpregadoResponse), 201)]
    [SwaggerResponse(201, "The product was created", typeof(PutEmpregadoResponse))]
    public async Task<ActionResult> Update([FromRoute] int id,
    [FromBody, BindRequired] PutEmpregadoRequest empregado,
    [FromServices] IEmpregadoUpdateHandler handler,
    CancellationToken cancellationToken = default)
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
    public async Task<ActionResult> Delete([FromRoute] int id,
    [FromServices] IEmpregadoDeleteHandler handler,
    CancellationToken cancellationToken = default)
    {
        //return Unauthorized();
        Response response = await handler.Handle(cancellationToken);
        if (response is NotFoundResponse) return NotFound();
        return NoContent();
    }
}

public class WeatherForecastResponseExample : IMultipleExamplesProvider<Empregado>
{
    public IEnumerable<SwaggerExample<Empregado>> GetExamples()
    {
        yield return SwaggerExample.Create("Com Id", new Empregado()
        {
            Nome = "Nome",
            Salario = 0.01M
        });
        yield return SwaggerExample.Create("Sem Id", new Empregado()
        {
            Id = 1,
            Nome = "Nome",
            Salario = 0.01M
        });
    }
}

public class WeatherForecastRequestExample : IMultipleExamplesProvider<Empregado>
{
    public IEnumerable<SwaggerExample<Empregado>> GetExamples()
    {
        yield return SwaggerExample.Create("Com Id", new Empregado()
        {
            Nome = "Nome",
            Salario = 0.01M
        });
        yield return SwaggerExample.Create("Sem Id", new Empregado()
        {
            Id = 1,
            Nome = "Nome",
            Salario = 0.01M
        });
    }
}