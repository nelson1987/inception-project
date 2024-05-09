using Inception.Api.Features.Account.Authentication;
using Inception.Api.Features.Account.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Inception.Api.Features.Account;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginAccountResponse>> Authenticate([FromServices] IUserRepository userRepository, [FromBody] LoginAccountCommand model, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.Get(model.Username, model.Password, cancellationToken);

        if (user == null)
            return NotFound(new { message = "Usuário ou senha inválidos" });

        return new LoginAccountResponse(user.Id, user.Username, user.Role, TokenService.GenerateToken(user));
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