using Inception.Core.Entities;
using Inception.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Inception.Api.Features.Account;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<ActionResult<dynamic>> Authenticate([FromServices] IUserRepository userRepository, [FromBody] User model)
    {
        var user = await userRepository.Get(model.Username, model.Password);

        if (user == null)
            return NotFound(new { message = "Usuário ou senha inválidos" });

        var token = TokenService.GenerateToken(user);
        user.Password = "";
        return new
        {
            user,
            token
        };
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
    public string Authenticated()
    {
        var identity = (ClaimsIdentity?)User.Identity!;
        var roles = identity.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value);

        return $"Autenticado: {identity.Name}\nRole: {string.Join(",", roles.ToList())}";
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