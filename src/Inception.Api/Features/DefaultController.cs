﻿using Microsoft.AspNetCore.Mvc;

namespace Inception.Api.Features;

[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public class DefaultController : ControllerBase
{
    private readonly ILogger _logger;

    public DefaultController(ILogger logger)
    {
        _logger = logger;
    }
}