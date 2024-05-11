using Microsoft.AspNetCore.Mvc;

namespace Inception.Api.Features;

[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public class DefaultController(ILogger logger) : ControllerBase
{
    public readonly ILogger _logger = logger;
}
