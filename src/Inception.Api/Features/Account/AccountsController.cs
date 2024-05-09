using Inception.Api.Features.Account.Authentication;
using Inception.Api.Features.Account.Login;
using Inception.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Security.Claims;

namespace Inception.Api.Features.Account;

[ApiController]
[Route("api/[controller]")]
[EnableRateLimiting("fixed-by-ip")]
[Produces("application/json")]
[Consumes("application/json")]
[SwaggerTag("Login Accounts")]
public class AccountsController : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("login", Name = "Abertura de conta Bancária [controller]")]
    [SwaggerOperation("Realizar Login", "Non-requires any privileges")]
    [SwaggerRequestExample(typeof(LoginAccountCommand), typeof(LoginAccountCommandExample))] 
    [ProducesResponseType(typeof(LoginAccountCommand), 200)]
    [SwaggerResponse(200, "O usuário foi logado com sucesso.")]
    [SwaggerResponseExample(200, typeof(LoginAccountResponse))]
    public async Task<ActionResult<LoginAccountResponse>> Authenticate([FromServices] IUserRepository userRepository, [FromBody] LoginAccountCommand model, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.Get(model.Username, model.Password, cancellationToken);

        if (user == null)
            return NotFound(new { message = "Usuário ou senha inválidos" });

        return Ok(new LoginAccountResponse(user.Id, user.Username, user.Role, TokenService.GenerateToken(user)));
    }

    [HttpGet]
    [Route("anonymous")]
    [AllowAnonymous]
    public string Anonymous()
    {
        return "Anônimo";
    }

    [HttpGet]
    [Route("authenticated")]
    [Authorize]
    public AuthenticationQueryResponse Authenticated()
    {
        var identity = (ClaimsIdentity?)User.Identity!;
        var roles = identity.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value);

        return new AuthenticationQueryResponse(identity.Name!, roles.ToArray());
    }

    [HttpGet]
    [Route("employee")]
    [Authorize(Roles = "employee,manager")]
    public string Employee()
    {
        var identity = (ClaimsIdentity?)User.Identity!;
        return $"Funcionário: {identity.Name}";
    }

    [HttpGet]
    [Route("manager")]
    [Authorize(Roles = "manager")]
    public string Manager()
    {
        var identity = (ClaimsIdentity?)User.Identity!;
        return $"Gerente: {identity.Name}";
    }
}