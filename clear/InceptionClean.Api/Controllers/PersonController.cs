using InceptionClean.Api.Configurations;
using InceptionClean.Application.Extensions;
using InceptionClean.Application.Features.Persons.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace InceptionClean.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    [EnableRateLimiting("fixed-by-ip")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [SwaggerTag("Person Controllers")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "manager")]
        [HttpPost(Name = "Abertura de conta Bancária [controller]")]
        [SwaggerOperation("Abra uma nova conta", "Requires admin privileges")]
        //[SwaggerRequestExample(typeof(CreatePersonRequest), typeof(CreatePersonRequestExample))]
        [ProducesResponseType(201)]
        [SwaggerResponse(201, "A conta foi criada com sucesso.")]
        [SwaggerResponseExample(201, typeof(CreatedResult))]
        public async Task<IResult> Post([FromServices] IMediator mediator, 
            [FromBody] CreatePersonRequest request, 
            CancellationToken cancellationToken = default)
        {
            var _request = request with { Name = "Nome", Email = "nome@email.com" };
            _logger.LogInformation($"Started command executed Json:\n{_request.ToJson()}");
            var createPerson = new CreatePerson() { Name = _request.Name, Email = _request.Email };
            var person = await mediator.Send(createPerson, cancellationToken);
            return TypedResults.Created($"/{person.Id}");
        }
    }
}
