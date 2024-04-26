using Microsoft.AspNetCore.Mvc;

namespace Inception.Api.Features.Empregados
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class EmpregadosController : ControllerBase
    {
        private readonly ILogger<EmpregadosController> _logger;

        public EmpregadosController(ILogger<EmpregadosController> logger)
        {
            _logger = logger;
        }

        //GETALL
        [HttpGet(Name = "Get All Empregado")]
        public async Task<ActionResult> GetAll() 
        {
            return Unauthorized();
            return Ok();
        }

        //GETBYID
        [HttpGet("{id:int}", Name = "Get Empregado By Id")]
        public async Task<ActionResult> GetById(int id)
        {
            return Unauthorized();
            return NotFound();
            return Ok();
        }

        //POST
        [HttpPost(Name = "Create Empregado")]
        public async Task<ActionResult> Post(Empregado empregado)
        {
            return Unauthorized();
            return NotFound();
            return Ok();
        }

        //PUT
        [HttpPut(Name = "Update Empregado")]
        public async Task<ActionResult> Update(int id, Empregado empregado)
        {
            return Unauthorized();
            return NotFound();
            return Ok();
        }

        //DELETE
        [HttpDelete(Name = "Delete Empregado")]
        public async Task<ActionResult> Delete(int id)
        {
            return Unauthorized();
            return NotFound();
            return Ok();
        }
    }
}
