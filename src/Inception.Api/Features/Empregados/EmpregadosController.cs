using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace Inception.Api.Features.Empregados
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
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
        public async Task<ActionResult> GetAll()
        {
            return Unauthorized();
            return Ok();
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
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            return Unauthorized();
            return NotFound();
            return Ok();
        }

        //POST
        [HttpPost(Name = "Create Empregado")]
        [SwaggerOperation("Creates a new empregado", "Requires admin privileges")]
        [ProducesResponseType(typeof(Empregado), 201)]
        [SwaggerResponse(201, "The product was created", typeof(Empregado))]
        public async Task<ActionResult> Post([FromBody, BindRequired] Empregado empregado)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Unauthorized();
            return Created();
        }

        //PUT
        [HttpPut("{id:int}", Name = "Update Empregado")]
        [SwaggerOperation("Update a Empregado", "Requires admin privileges")]
        [ProducesResponseType(typeof(Empregado), 201)]
        [SwaggerResponse(201, "The product was created", typeof(Empregado))]
        public async Task<ActionResult> Update(int id, Empregado empregado)
        {
            return Unauthorized();
            return NotFound();
            return Ok();
        }

        //DELETE
        [HttpDelete(Name = "Delete Empregado")]
        [SwaggerOperation("Delete a Empregado", "Requires admin privileges")]
        [ProducesResponseType(204)]
        [SwaggerResponse(204, "The empregado was deleted")]
        public async Task<ActionResult> Delete(int id)
        {
            return Unauthorized();
            return NotFound();
            return NoContent();
        }
    }
}